using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211012102600)]
    public class N20211012102600_AddingFacultyTable : Migration
    {
        public const string TableName = "tblFaculties";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                 .WithColumn("FacultyID").AsInt32().Identity().NotNullable().PrimaryKey()
                 .WithColumn("FacultyName").AsCustom("varchar(max)").Nullable()
                 .WithColumn("SubjectName").AsCustom("varchar(max)").Nullable()
                 .WithColumn("Details").AsCustom("varchar(max)").Nullable()
                 .WithColumn("Experience").AsCustom("varchar(max)").Nullable()
                 .WithColumn("Photo").AsCustom("varchar(max)").Nullable();
        }
    }
}
