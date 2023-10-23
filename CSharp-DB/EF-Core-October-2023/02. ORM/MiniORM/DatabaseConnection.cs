namespace MiniORM;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

/// <summary>
///     Used for accessing a database, inserting/updating/deleting entities
///     and mapping database columns to entity classes.
/// </summary>
internal class DatabaseConnection
{
    private readonly SqlConnection connection;

    private SqlTransaction transaction;

    /// <summary>
    /// Constructor for the DatabaseConnection class.
    /// Initializes a new instance with the provided connection string.
    /// </summary>
    /// <param name="connectionString">The connection string to the database.</param>
    public DatabaseConnection(string connectionString)
    {
        this.connection = new SqlConnection(connectionString);
    }

    private SqlCommand CreateCommand(string queryText, params SqlParameter[] parameters)
    {
        var command = new SqlCommand(queryText, this.connection, this.transaction);

        foreach (var param in parameters)
        {
            command.Parameters.Add(param);
        }

        return command;
    }

    /// <summary>
    /// Executes a non-query SQL command and returns the number of affected rows.
    /// </summary>
    /// <param name="queryText">The SQL command text to execute.</param>
    /// <param name="parameters">Optional parameters to include in the command.</param>
    /// <returns>The number of affected rows.</returns>
    public int ExecuteNonQuery(string queryText, params SqlParameter[] parameters)
    {
        using var query = this.CreateCommand(queryText, parameters);
        int result = query.ExecuteNonQuery();

        return result;
    }

    /// <summary>
    /// Fetches the column names of a specified table from the database.
    /// </summary>
    /// <param name="tableName">The name of the table to fetch column names for.</param>
    /// <returns>A collection of column names.</returns>
    public IEnumerable<string> FetchColumnNames(string tableName)
    {
        var rows = new List<string>();

        string queryText = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

        using var query = this.CreateCommand(queryText);
        using var reader = query.ExecuteReader();
        while (reader.Read())
        {
            string column = reader.GetString(0);

            rows.Add(column);
        }

        return rows;
    }

    /// <summary>
    /// Executes a query and returns a collection of results of type T.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="queryText">The SQL query text to execute.</param>
    /// <returns>A collection of query results of type T.</returns>
    public IEnumerable<T> ExecuteQuery<T>(string queryText)
    {
        var rows = new List<T>();

        using var query = this.CreateCommand(queryText);
        using var reader = query.ExecuteReader();
        while (reader.Read())
        {
            object[] columnValues = new object[reader.FieldCount];
            reader.GetValues(columnValues);

            var obj = reader.GetFieldValue<T>(0);
            rows.Add(obj);
        }

        return rows;
    }

    /// <summary>
    /// Fetches a result set from a table with specific column names.
    /// </summary>
    /// <typeparam name="T">The type of the result set.</typeparam>
    /// <param name="tableName">The name of the table to fetch data from.</param>
    /// <param name="columnNames">The names of the columns to include in the result set.</param>
    /// <returns>A collection of objects of type T.</returns>
    public IEnumerable<T> FetchResultSet<T>(string tableName, params string[] columnNames)
    {
        var rows = new List<T>();

        string escapedColumns = string.Join(", ", columnNames.Select(EscapeColumn));
        string queryText = $@"SELECT {escapedColumns} FROM {tableName}";

        using var query = this.CreateCommand(queryText);
        using var reader = query.ExecuteReader();
        while (reader.Read())
        {
            object[] columnValues = new object[reader.FieldCount];
            reader.GetValues(columnValues);

            var obj = MapColumnsToObject<T>(columnNames, columnValues);
            rows.Add(obj);
        }

        return rows;
    }

    /// <summary>
    /// Inserts a collection of entities into the specified table.
    /// </summary>
    /// <typeparam name="T">The type of entities to insert.</typeparam>
    /// <param name="entities">The collection of entities to insert.</param>
    /// <param name="tableName">The name of the table to insert the entities into.</param>
    /// <param name="columns">The columns to insert data into.</param>
    public void InsertEntities<T>(IEnumerable<T> entities, string tableName, string[] columns)
        where T : class
    {
        IEnumerable<string> identityColumns = this.GetIdentityColumns(tableName);

        string[] columnsToInsert = columns.Except(identityColumns).ToArray();

        string[] escapedColumns = columnsToInsert.Select(EscapeColumn).ToArray();

        object[][] rowValues = entities
            .Select(entity => columnsToInsert
                .Select(c => entity.GetType().GetProperty(c).GetValue(entity))
                .ToArray())
            .ToArray();

        string[][] rowParameterNames = Enumerable.Range(1, rowValues.Length)
            .Select(i => columnsToInsert.Select(c => c + i).ToArray())
            .ToArray();

        string sqlColumns = string.Join(", ", escapedColumns);

        string sqlRows = string.Join(", ",
            rowParameterNames.Select(p =>
                string.Format("({0})",
                    string.Join(", ", p.Select(a => $"@{a}")))));

        string query = string.Format(
            "INSERT INTO {0} ({1}) VALUES {2}",
            tableName,
            sqlColumns,
            sqlRows
        );

        SqlParameter[] parameters = rowParameterNames
            .Zip(rowValues, (@params, values) =>
                @params.Zip(values, (param, value) =>
                    new SqlParameter(param, value ?? DBNull.Value)))
            .SelectMany(p => p)
            .ToArray();

        int insertedRows = this.ExecuteNonQuery(query, parameters);

        if (insertedRows != entities.Count())
        {
            throw new InvalidOperationException($"Could not insert {entities.Count() - insertedRows} rows.");
        }
    }

    /// <summary>
    /// Updates a collection of entities in the specified table.
    /// </summary>
    /// <typeparam name="T">The type of entities to update.</typeparam>
    /// <param name="modifiedEntities">The collection of entities to update.</param>
    /// <param name="tableName">The name of the table to update entities in.</param>
    /// <param name="columns">The columns to update data in.</param>
    public void UpdateEntities<T>(IEnumerable<T> modifiedEntities, string tableName, string[] columns)
        where T : class
    {
        IEnumerable<string> identityColumns = this.GetIdentityColumns(tableName);

        string[] columnsToUpdate = columns.Except(identityColumns).ToArray();

        PropertyInfo[] primaryKeyProperties = typeof(T).GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray();

        foreach (var entity in modifiedEntities)
        {
            object[] primaryKeyValues = primaryKeyProperties
                .Select(c => c.GetValue(entity))
                .ToArray();

            SqlParameter[] primaryKeyParameters = primaryKeyProperties
                .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value))
                .ToArray();

            object[] rowValues = columnsToUpdate
                .Select(c => entity.GetType().GetProperty(c).GetValue(entity) ?? DBNull.Value)
                .ToArray();

            SqlParameter[] columnsParameters = columnsToUpdate.Zip(rowValues, (param, value) => new SqlParameter(param, value))
                .ToArray();

            string columnsSql = string.Join(", ",
                columnsToUpdate.Select(c => $"{c} = @{c}"));

            string primaryKeysSql = string.Join(" AND ",
                primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));

            string query = string.Format("UPDATE {0} SET {1} WHERE {2}",
                tableName,
                columnsSql,
                primaryKeysSql);

            int updatedRows = this.ExecuteNonQuery(query, columnsParameters.Concat(primaryKeyParameters).ToArray());

            if (updatedRows != 1)
            {
                throw new InvalidOperationException($"Update for table {tableName} failed.");
            }
        }
    }

    /// <summary>
    /// Deletes a collection of entities from the specified table.
    /// </summary>
    /// <typeparam name="T">The type of entities to delete.</typeparam>
    /// <param name="entitiesToDelete">The collection of entities to delete.</param>
    /// <param name="tableName">The name of the table to delete entities from.</param>
    /// <param name="columns">The columns to use for deletion criteria.</param>
    public void DeleteEntities<T>(IEnumerable<T> entitiesToDelete, string tableName, string[] columns)
        where T : class
    {
        PropertyInfo[] primaryKeyProperties = typeof(T).GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray();

        foreach (var entity in entitiesToDelete)
        {
            object[] primaryKeyValues = primaryKeyProperties
                .Select(c => c.GetValue(entity))
                .ToArray();

            SqlParameter[] primaryKeyParameters = primaryKeyProperties
                .Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value))
                .ToArray();

            string primaryKeysSql = string.Join(" AND ",
                primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));

            string query = string.Format("DELETE FROM {0} WHERE {1}",
                tableName,
                primaryKeysSql);

            int updatedRows = this.ExecuteNonQuery(query, primaryKeyParameters);

            if (updatedRows != 1)
            {
                throw new InvalidOperationException($"Delete for table {tableName} failed.");
            }
        }
    }

    /// <summary>
    /// Retrieves the identity columns of a specified table.
    /// </summary>
    /// <param name="tableName">The name of the table to retrieve identity columns for.</param>
    /// <returns>A collection of identity column names.</returns>
    private IEnumerable<string> GetIdentityColumns(string tableName)
    {
        const string identityColumnsSql =
            "SELECT COLUMN_NAME FROM (SELECT COLUMN_NAME, COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}') AS IdentitySpecs WHERE IsIdentity = 1";

        string parametrizedSql = string.Format(identityColumnsSql, tableName);

        IEnumerable<string> identityColumns = this.ExecuteQuery<string>(parametrizedSql);

        return identityColumns;
    }

    /// <summary>
    /// Starts a new database transaction.
    /// </summary>
    /// <returns>A <see cref="SqlTransaction"/> object representing the new transaction.</returns>
    public SqlTransaction StartTransaction()
    {
        this.transaction = this.connection.BeginTransaction();
        return this.transaction;
    }

    // Opens the database connection.
    public void Open() => this.connection.Open();

    // Closes the database connection.
    public void Close() => this.connection.Close();

    /// <summary>
    /// Escapes a column name by enclosing it in square brackets.
    /// </summary>
    /// <param name="c">The column name to be escaped.</param>
    /// <returns>The escaped column name.</returns>
    private static string EscapeColumn(string c)
    {
        string escapedColumn = $"[{c}]";
        return escapedColumn;
    }

    /// <summary>
    /// Maps column values to an object of type T based on the provided column names.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <param name="columnNames">The names of the columns to map.</param>
    /// <param name="columns">The values of the columns to map.</param>
    /// <returns>An object of type T with mapped column values.</returns>
    private static T MapColumnsToObject<T>(string[] columnNames, object[] columns)
    {
        var obj = Activator.CreateInstance<T>();

        for (int i = 0; i < columns.Length; i++)
        {
            string columnName = columnNames[i];
            object columnValue = columns[i];

            if (columnValue is DBNull)
            {
                columnValue = null;
            }

            var property = typeof(T).GetProperty(columnName);
            property.SetValue(obj, columnValue);
        }

        return obj;
    }
}