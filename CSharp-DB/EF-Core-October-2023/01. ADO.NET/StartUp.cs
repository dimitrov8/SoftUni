using Exercises_ADO.NET;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

await using var sqlConnection = new SqlConnection(Config.CONNECTION_STRING);
await sqlConnection.OpenAsync();

// string result = await ..;
// Console.WriteLine(result);

string result = await IncreaseMinionAgeUsingStoredProcedureAsync(sqlConnection);
Console.WriteLine(result);

// 02. Villain Names
static async Task<string> GetAllVillainNamesAsync(SqlConnection sqlConnection)
{
    var sb = new StringBuilder();

    var getVillainNamesCmd = new SqlCommand(SqlQueries.VILLAIN_NAMES, sqlConnection);
    await using var reader = await getVillainNamesCmd.ExecuteReaderAsync();
    while (reader.Read())
    {
        string villainName = (string)reader["Name"];
        int minionsCount = (int)reader["MinionsCount"];

        sb.AppendLine($"{villainName} - {minionsCount}");
    }

    return sb.ToString().TrimEnd();
}

// 03. Minion Names
static async Task<string> GetVillainsWithTheirMinionsAsync(SqlConnection sqlConnection, int villainId)
{
    var sb = new StringBuilder();

    var getVillainByIdCmd = new SqlCommand(SqlQueries.VILLAIN_ID, sqlConnection);
    getVillainByIdCmd.Parameters.AddWithValue("@Id", villainId);

    object? villainObj = await getVillainByIdCmd.ExecuteScalarAsync();
    if (villainObj == null)
        return $"No villain with ID {villainId} exists in the database.";

    string villainName = (string)villainObj;
    sb.AppendLine($"Villain: {villainName}");

    var getMinionsInfoCmd = new SqlCommand(SqlQueries.MINIONS_NAMES_AND_AGE, sqlConnection);
    getMinionsInfoCmd.Parameters.AddWithValue("@Id", villainId);

    await using var reader = await getMinionsInfoCmd.ExecuteReaderAsync();
    {
        if (!reader.HasRows)
        {
            sb.AppendLine("(no minions)");
        }
        else
        {
            while (reader.Read())
            {
                long rowNumber = (long)reader["RowNum"];
                string minionName = (string)reader["Name"];
                int age = (int)reader["Age"];

                sb.AppendLine($"{rowNumber}. {minionName} {age}");
            }
        }
    }

    return sb.ToString().TrimEnd();
}

// 04. Add Minion
static async Task<string> AddNewMinionAsync(SqlConnection sqlConnection)
{
    var sb = new StringBuilder();

    string[] minionArgs = Console.ReadLine().Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    string minionName = minionArgs[1];
    int minionAge = int.Parse(minionArgs[2]);
    string townName = minionArgs[3];

    string[] villainArgs = Console.ReadLine().Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    string villainName = villainArgs[1];

    var sqlTransaction = sqlConnection.BeginTransaction();
    try
    {
        int townId = await GetTownIdOrAddByNameAsync(sqlConnection, sqlTransaction, townName, sb);
        int villainId = await GetVillainIdOrAddByNameAsync(sqlConnection, sqlTransaction, villainName, sb);
        int minionId = await AddNewMinionAndReturnIdAsync(sqlConnection, sqlTransaction, minionName, minionAge, townId);

        await SetMinionToBeServantOfVillainAsync(sqlConnection, sqlTransaction, minionId, villainId);
        sb.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");

        await sqlTransaction.CommitAsync();
    }
    catch (Exception e)
    {
        await sqlTransaction.RollbackAsync();
        sb.AppendLine("Transaction Failed!");
    }

    return sb.ToString().Trim();
}

static async Task<int> GetTownIdOrAddByNameAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string townName, StringBuilder sb)
{
    var getTownIdCmd = new SqlCommand(SqlQueries.TOWN_NAME_BY_ID, sqlConnection, sqlTransaction);
    getTownIdCmd.Parameters.AddWithValue("@townName", townName);

    int? townId = (int?)await getTownIdCmd.ExecuteScalarAsync();
    if (!townId.HasValue)
    {
        var addNewTownCmd = new SqlCommand(SqlQueries.ADD_TOWN, sqlConnection, sqlTransaction);
        addNewTownCmd.Parameters.AddWithValue("@townName", townName);

        await addNewTownCmd.ExecuteNonQueryAsync();
        townId = (int?)await getTownIdCmd.ExecuteScalarAsync();
        sb.AppendLine($"Town {townName} was added to the database.");
    }

    return townId.Value;
}

static async Task<int> GetVillainIdOrAddByNameAsync(SqlConnection sqlConnection,
    SqlTransaction sqlTransaction,
    string villainName,
    StringBuilder sb)
{
    var getVillageIdCmd = new SqlCommand(SqlQueries.VILLAIN_BY_NAME, sqlConnection, sqlTransaction);
    getVillageIdCmd.Parameters.AddWithValue("@Name", villainName);

    int? villainId = (int?)await getVillageIdCmd.ExecuteScalarAsync();
    if (!villainId.HasValue)
    {
        var addVillainCmd = new SqlCommand(SqlQueries.ADD_VILLAIN_WITH_DEFAULT_EVILNESS_FACTOR, sqlConnection, sqlTransaction);
        addVillainCmd.Parameters.AddWithValue("@villainName", villainName);

        await addVillainCmd.ExecuteNonQueryAsync();

        villainId = (int?)await getVillageIdCmd.ExecuteScalarAsync();
        sb.AppendLine($"Villain {villainName} was added to the database.");
    }

    return villainId.Value;
}

static async Task<int> AddNewMinionAndReturnIdAsync(SqlConnection sqlConnection,
    SqlTransaction sqlTransaction,
    string minionName,
    int minionAge,
    int townId)
{
    var addNewMinionAsync = new SqlCommand(SqlQueries.ADD_MINION, sqlConnection, sqlTransaction);
    addNewMinionAsync.Parameters.AddWithValue("@name", minionName);
    addNewMinionAsync.Parameters.AddWithValue("@age", minionAge);
    addNewMinionAsync.Parameters.AddWithValue("@townId", townId);

    await addNewMinionAsync.ExecuteNonQueryAsync();

    var getMinionIdCmd = new SqlCommand(SqlQueries.MINION_ID, sqlConnection, sqlTransaction);
    getMinionIdCmd.Parameters.AddWithValue("@Name", minionName);

    int minionId = (int)await getMinionIdCmd.ExecuteScalarAsync();
    return minionId;
}

static async Task SetMinionToBeServantOfVillainAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int minionId, int villainId)
{
    var addMinionVillainCmd = new SqlCommand(SqlQueries.ADD_MINION_VILLAIN, sqlConnection, sqlTransaction);
    addMinionVillainCmd.Parameters.AddWithValue("@minionId", minionId);
    addMinionVillainCmd.Parameters.AddWithValue("@inputVillainId", villainId);

    await addMinionVillainCmd.ExecuteNonQueryAsync();
}

// 05. Change Town Names Casing
static async Task<string> UpdateTownNamesToUpperCaseAsync(SqlConnection sqlConnection)
{
    const string noTownNamesWereAffectedMsg = "No town names were affected.";

    string countryInput = Console.ReadLine();

    var getCountryIdCmd = new SqlCommand(SqlQueries.COUNTRY_ID, sqlConnection);
    getCountryIdCmd.Parameters.AddWithValue("@countryName", countryInput);
    int? countryId = (int?)await getCountryIdCmd.ExecuteScalarAsync();
    if (!countryId.HasValue)
        return noTownNamesWereAffectedMsg;

    var townsToUpperCaseCmd = new SqlCommand(SqlQueries.UPDATE_TOWN_NAMES_TO_UPPERCASE, sqlConnection);
    townsToUpperCaseCmd.Parameters.AddWithValue("@countryName", countryInput);

    int rowsAffected = await townsToUpperCaseCmd.ExecuteNonQueryAsync();
    if (rowsAffected == 0)
        return noTownNamesWereAffectedMsg;

    var listTownsCmd = new SqlCommand(SqlQueries.LIST_TOWNS, sqlConnection);
    listTownsCmd.Parameters.AddWithValue("@countryName", countryInput);

    var updatedTownNames = new HashSet<string>();
    await using var reader = await listTownsCmd.ExecuteReaderAsync();
    {
        if (!reader.HasRows)
            return "No town names were affected.";

        while (reader.Read())
        {
            string townName = (string)reader["Name"];
            updatedTownNames.Add(townName);
        }
    }

    return $"{rowsAffected} town names were affected. {Environment.NewLine}[{string.Join(", ", updatedTownNames)}]";
}

// 06. *Remove Villain
static async Task<string> RemoveVillain(SqlConnection sqlConnection)
{
    var sb = new StringBuilder();
    int inputVillainId = int.Parse(Console.ReadLine());

    var sqlTransaction = sqlConnection.BeginTransaction();
    var findVillainNameById = new SqlCommand(SqlQueries.VILLAIN_ID_TASK_06, sqlConnection, sqlTransaction);
    findVillainNameById.Parameters.AddWithValue("@villainId", inputVillainId);

    try
    {
        string? villainName = (string?)await findVillainNameById.ExecuteScalarAsync();
        if (villainName.IsNullOrEmpty())
            return "No such villain was found.";

        var deleteVillainMinionsCmd = new SqlCommand(SqlQueries.DELETE_VILLAIN_MINIONS, sqlConnection, sqlTransaction);
        deleteVillainMinionsCmd.Parameters.AddWithValue("@villainId", inputVillainId);

        int deletedMinionsCount = deleteVillainMinionsCmd.ExecuteNonQuery();

        sb.AppendLine($"{villainName} was deleted.")
            .AppendLine($"{deletedMinionsCount} minions were released.");
    }
    catch (Exception e)
    {
        await sqlTransaction.RollbackAsync();
        sb.AppendLine("Transaction Failed!");
    }

    return sb.ToString().TrimEnd();
}

// 07. Print All Minion Names
static async Task<string> PrintAllMinionsNames(SqlConnection sqlConnection)
{
    var sb = new StringBuilder();
    var getAllMinionNames = new SqlCommand(SqlQueries.ALL_MINION_NAMES, sqlConnection);

    var minionNames = new List<string>();

    await using var reader = await getAllMinionNames.ExecuteReaderAsync();
    while (reader.Read())
    {
        string minionName = (string)reader["Name"];
        minionNames.Add(minionName);
    }

    for (int first = 0, last = minionNames.Count - 1; first <= last; first++, last--)
    {
        sb.AppendLine(minionNames[first]);
        if (first != last)
        {
            sb.AppendLine(minionNames[last]);
        }
    }

    return sb.ToString();
}

// 08. Increase Minion Age
static async Task<string> IncreaseMinionAge(SqlConnection sqlConnection)
{
    var sb = new StringBuilder();

    int[] minionIdsInput = Console.ReadLine()
        .Split(' ')
        .Select(int.Parse)
        .ToArray();

    var updateMinionsAgeCmd = new SqlCommand(SqlQueries.UPDATE_MINIONS_AGE_BY_1, sqlConnection);
    var updateMinionsAgeParameter = new SqlParameter("@Id", SqlDbType.Int);
    updateMinionsAgeCmd.Parameters.Add(updateMinionsAgeParameter);

    foreach (int minionId in minionIdsInput)
    {
        updateMinionsAgeParameter.Value = minionId;
        await updateMinionsAgeCmd.ExecuteScalarAsync();
    }

    var printMinionNameAndAgeCmd = new SqlCommand(SqlQueries.GET_MINIONS_NAME_AGE, sqlConnection);

    await using var reader = await printMinionNameAndAgeCmd.ExecuteReaderAsync();
    while (reader.Read())
    {
        string minionName = (string)reader["Name"];
        int minionAge = (int)reader["Age"];
        sb.AppendLine($"{minionName} {minionAge}");
    }

    return sb.ToString().TrimEnd();
}

// 09. Increase Age Stored Procedure
static async Task<string> IncreaseMinionAgeUsingStoredProcedureAsync(SqlConnection sqlConnection)
{
    if (!int.TryParse(Console.ReadLine(), out int minionIdInput))
        return "Invalid input. Please input an integer value!";

    try
    {
        var increaseAgeStoredProcedureCmd = new SqlCommand("usp_GetOlder", sqlConnection);
        increaseAgeStoredProcedureCmd.CommandType = CommandType.StoredProcedure;
        increaseAgeStoredProcedureCmd.Parameters.AddWithValue("@minionId", minionIdInput);
        await increaseAgeStoredProcedureCmd.ExecuteScalarAsync();

        var printMinionNameAndAgeCmd = new SqlCommand(SqlQueries.GET_MINION_NAME_AND_AGE_BY_ID, sqlConnection);
        printMinionNameAndAgeCmd.Parameters.AddWithValue("@Id", minionIdInput);
        await using var reader = await printMinionNameAndAgeCmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            string minionName = (string)reader["Name"];
            int minionAge = (int)reader["Age"];

            return $"{minionName} – {minionAge} years old";
        }

        return $"MinionId: [{minionIdInput}] does not exist!";
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}