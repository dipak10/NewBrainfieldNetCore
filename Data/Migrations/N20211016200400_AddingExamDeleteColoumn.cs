using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211016200400)]
    public class N20211016200400_AddingExamDeleteColoumn : Migration
    {
        public const string TableName = "tblExamMaster";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Alter.Table(TableName)
                .AddColumn("IsDeleted").AsBoolean().WithDefaultValue(0)
                .AddColumn("IsVisible").AsBoolean().WithDefaultValue(1);
        }
    }
}
