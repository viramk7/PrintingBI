﻿namespace PrintingBI.Data.Infrastructure
{
    public static class DbContextExtensions
    {
        //public static string GenerateCreateScript(this DatabaseFacade database)
        //{
        //    var model = database.GetService<IModel>();
        //    var differ = database.GetService<IMigrationsModelDiffer>();
        //    var generator = database.GetService<IMigrationsSqlGenerator>();
        //    var sql = database.GetService<ISqlGenerationHelper>();

        //    var operations = differ.GetDifferences(null, model);
        //    var commands = generator.Generate(operations, model);

        //    var builder = new StringBuilder();
        //    foreach (var command in commands)
        //    {
        //        builder
        //            .Append(command.CommandText)
        //            .AppendLine(sql.BatchTerminator);
        //    }

        //    return builder.ToString();
        //}
    }
}
