namespace Boardgames;

using Data;
using DataProcessor;
using Microsoft.EntityFrameworkCore;

public class StartUp
{
    public static void Main()
    {
        var context = new BoardgamesContext();

        ResetDatabase(context, true);

        string projectDir = GetProjectDirectory();

        ImportEntities(context, projectDir + @"Datasets/", projectDir + @"ImportResults/");

        ExportEntities(context, projectDir + @"ExportResults/");

        using var transaction = context.Database.BeginTransaction();

        transaction.Rollback();
    }

    private static void ImportEntities(BoardgamesContext context, string baseDir, string exportDir)
    {
        string creators =
            Deserializer.ImportCreators(context,
                File.ReadAllText(baseDir + "creators.xml"));

        PrintAndExportEntityToFile(creators, exportDir + "Actual Result - ImportCreators.txt");

        string sellers =
            Deserializer.ImportSellers(context,
                File.ReadAllText(baseDir + "sellers.json"));

        PrintAndExportEntityToFile(sellers, exportDir + "Actual Result - ImportSellers.txt");
    }

    private static void ExportEntities(BoardgamesContext context, string exportDir)
    {
        string exportCreatorsWithTheirBoardgames = Serializer.ExportCreatorsWithTheirBoardgames(context);
        Console.WriteLine(exportCreatorsWithTheirBoardgames);
        File.WriteAllText(exportDir + "Actual Result - ExportCreatorsWithTheirBoardgames.xml", exportCreatorsWithTheirBoardgames);

        int year = 2021;
        double rating = 9.50;
        string exportSellersWithMostBoardgames = Serializer.ExportSellersWithMostBoardgames(context, year, rating);
        Console.WriteLine(exportSellersWithMostBoardgames);
        File.WriteAllText(exportDir + "Actual Result - ExportSellersWithMostBoardgames.json", exportSellersWithMostBoardgames);
    }

    private static void ResetDatabase(BoardgamesContext context, bool shouldDropDatabase = false)
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