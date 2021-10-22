using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211022095200)]
    public class N20211022095200_AddingCustomUserIDColoumn : Migration
    {
        public const string TableName = "AspNetUsers";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Alter.Table(TableName)
                .AddColumn("UserID").AsInt32().Identity().NotNullable();
        }
    }
}
