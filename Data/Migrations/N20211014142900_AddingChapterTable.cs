using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211014142900)]
    public class N20211014142900_AddingChapterTable : Migration
    {
        public const string TableName = "tblChapters";
        public const string primaryTableName = "tblSubject";
        public const string primaryColumnName = "SubjectID";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("ChapterID").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("SubjectID").AsInt32().Nullable().ForeignKey(primaryTableName: primaryTableName, primaryColumnName: primaryColumnName)
                .WithColumn("ChapterName").AsCustom("varchar(max)").Nullable();
        }
    }
}
