namespace Exercises_ADO.NET;

public static class SqlQueries
{
    public const string VILLAIN_NAMES = @" SELECT v.Name,COUNT(mv.VillainId) AS MinionsCount  
                                           FROM Villains AS v 
                                               JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                           GROUP BY v.Id, v.Name 
                                           HAVING COUNT(mv.VillainId) > 3 
                                           ORDER BY COUNT(mv.VillainId)";

    public const string VILLAIN_ID = @"SELECT Name FROM Villains WHERE Id = @Id";

    public const string MINIONS_NAMES_AND_AGE = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

    public const string TOWN_NAME_BY_ID = @"SELECT Id FROM Towns WHERE Name = @townName";

    public const string ADD_TOWN = @"INSERT INTO Towns (Name) VALUES (@townName)";

    public const string VILLAIN_BY_NAME = @"SELECT Id FROM Villains WHERE Name = @Name";

    public const string ADD_VILLAIN_WITH_DEFAULT_EVILNESS_FACTOR = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

    public const string ADD_MINION = @"INSERT INTO Minions (Name, Age, TownId) VALUES(@name, @age, @townId)";

    public const string MINION_ID = @"SELECT Id FROM Minions WHERE Name = @Name";

    public const string ADD_MINION_VILLAIN = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

    public const string UPDATE_TOWN_NAMES_TO_UPPERCASE = @"UPDATE Towns
     SET Name = UPPER(Name)
     WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)
";

    public const string LIST_TOWNS = @" SELECT t.Name 
   FROM Towns as t
   JOIN Countries AS c ON c.Id = t.CountryCode
  WHERE c.Name = @countryName";

    public const string COUNTRY_ID = @"SELECT Id
FROM Countries
WHERE Name = @countryName";

    public const string VILLAIN_ID_TASK_06 = @"SELECT Name FROM Villains WHERE Id = @villainId";

    public const string DELETE_VILLAIN_MINIONS = @"DELETE FROM MinionsVillains 
      WHERE VillainId = @villainId";

    public const string ALL_MINION_NAMES = @"SELECT Name FROM Minions";

    public const string UPDATE_MINIONS_AGE_BY_1 = @" UPDATE Minions
   SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
 WHERE Id = @Id";

    public const string GET_MINIONS_NAME_AGE = @"SELECT Name, Age FROM Minions";

    public const string GET_MINION_NAME_AND_AGE_BY_ID = @"
SELECT Name, Age FROM Minions WHERE Id = @Id";
}