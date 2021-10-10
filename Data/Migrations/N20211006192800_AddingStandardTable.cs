using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006192800)]
    public class N20211006192800_AddingStandardTable : Migration
    {
        public const string TableName = "tblStandard";

        public const string StandardID = "StandardID";
        public const string StandardName = "StandardName";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(StandardID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(StandardName).AsString().NotNullable();
        }
    }
}
