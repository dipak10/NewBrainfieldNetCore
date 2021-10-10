using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006195000)]
    public class N20211006195000_AddingChapterTable : Migration
    {
        public const string TableName = "tblChapter";
        public const string ChapterID = "ChapterID";
        public const string ChapterName = "ChapterName";
        public const string FKTableName = "tblSubject";
        public const string FKSubjectID = "SubjectID";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(ChapterID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(ChapterName).AsString().NotNullable()
                .WithColumn(FKSubjectID).AsInt32().ForeignKey(FKTableName, FKSubjectID);
        }
    }
}
