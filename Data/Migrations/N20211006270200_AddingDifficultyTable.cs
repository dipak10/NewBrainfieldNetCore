using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006270200)]
    public class N20211006270200_AddingDifficultyTable : Migration
    {
        public const string TableName = "tblDifficultyLevel";
        public const string DifficultyLevelID = "DifficultyLevelId";
        public const string DifficultyLevelName = "DifficultyLevelName";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(DifficultyLevelID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(DifficultyLevelName).AsString().NotNullable();
        }
    }
}
