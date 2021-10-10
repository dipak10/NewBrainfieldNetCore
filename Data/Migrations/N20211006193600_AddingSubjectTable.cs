using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006193600)]
    public class N20211006193600_AddingSubjectTable : Migration
    {
        public const string TableName = "tblSubject";
        public const string SubjectID = "SubjectID";
        public const string SubjectName = "SubjectName";
        public const string FKTableName = "tblStandard";
        public const string FKStandardID = "StandardID";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(SubjectID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(SubjectName).AsString().NotNullable()
                .WithColumn(FKStandardID).AsInt32().ForeignKey(FKTableName, FKStandardID);
        }
    }
}
