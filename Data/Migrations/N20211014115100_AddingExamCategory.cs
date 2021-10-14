using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211014115100)]
    public class N20211014115100_AddingExamCategory : Migration
    {
        public const string TableName = "tblExamCategory";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("ExamCategoryID").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("ExamCategoryName").AsCustom("varchar(max)").Nullable();
        }
    }
}
