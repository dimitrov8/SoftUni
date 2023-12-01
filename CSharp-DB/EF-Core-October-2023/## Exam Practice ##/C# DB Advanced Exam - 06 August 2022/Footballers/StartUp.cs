﻿namespace Footballers;

using Data;
using DataProcessor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class StartUp
{
    public static void Main()
    {
        var context = new FootballersContext();
        
        //var config = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<FootballersProfile>();
        //});
        //var mapper = new Mapper(config);

        ResetDatabase(context, true);

        string projectDir = GetProjectDirectory();

        ImportEntities(context, projectDir + @"Datasets/", projectDir + @"ImportResults/");

        ExportEntities(context, projectDir + @"ExportResults/");

        using (var transaction = context.Database.BeginTransaction())
        {
            transaction.Rollback();
        }
    }

    private static void ImportEntities(FootballersContext context, string baseDir, string exportDir)
    {
        string coaches =
            Deserializer.ImportCoaches(context,
                File.ReadAllText(baseDir + "coaches.xml"));

        PrintAndExportEntityToFile(coaches, exportDir + "Actual Result - ImportCoaches.txt");

        string teams =
            Deserializer.ImportTeams(context,
                File.ReadAllText(baseDir + "teams.json"));

        PrintAndExportEntityToFile(teams, exportDir + "Actual Result - ImportTeams.txt");
    }

    private static void ExportEntities(FootballersContext context, string exportDir)
    {
        string exportCoachesWithTheirFootballers = Serializer.ExportCoachesWithTheirFootballers(context);
        Console.WriteLine(exportCoachesWithTheirFootballers);
        File.WriteAllText(exportDir + "Actual Result - ExportCoachesWithTheirFootballers.xml", exportCoachesWithTheirFootballers);

        var dateTime = DateTime.ParseExact("31/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string exportTeamsWithMostPlayers = Serializer.ExportTeamsWithMostFootballers(context, dateTime);
        Console.WriteLine(exportTeamsWithMostPlayers);
        File.WriteAllText(exportDir + "Actual Result - ExportTeamsWithMostFootballers.json", exportTeamsWithMostPlayers);
    }

    private static void ResetDatabase(FootballersContext context, bool shouldDropDatabase = false)
    {
        if (shouldDropDatabase)
        {
            context.Database.EnsureDeleted();
        }

        if (context.Database.EnsureCreated())
        {
            return;
        }

        string disableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
        context.Database.ExecuteSqlRaw(disableIntegrityChecksQuery);

        string deleteRowsQuery = "EXEC sp_MSforeachtable @command1='SET QUOTED_IDENTIFIER ON;DELETE FROM ?'";
        context.Database.ExecuteSqlRaw(deleteRowsQuery);

        string enableIntegrityChecksQuery =
            "EXEC sp_MSforeachtable @command1='ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";

        context.Database.ExecuteSqlRaw(enableIntegrityChecksQuery);

        string reseedQuery =
            "EXEC sp_MSforeachtable @command1='IF OBJECT_ID(''?'') IN (SELECT OBJECT_ID FROM SYS.IDENTITY_COLUMNS) DBCC CHECKIDENT(''?'', RESEED, 0)'";

        context.Database.ExecuteSqlRaw(reseedQuery);
    }

    private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
    {
        Console.WriteLine(entityOutput);
        File.WriteAllText(outputPath, entityOutput.TrimEnd());
    }

    private static string GetProjectDirectory()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string directoryName = Path.GetFileName(currentDirectory);
        string relativePath = directoryName.StartsWith("net6.0") ? @"../../../" : string.Empty;

        return relativePath;
    }
}
