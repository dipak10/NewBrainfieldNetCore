using FluentMigrator;
using Microsoft.AspNetCore.Identity;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211005130000)]
    public class N20211005130000_AddingDataToRoles : Migration
    {

        public readonly string TableName = "AspNetRoles";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Insert.IntoTable(TableName).Row(new IdentityRole { Name = "Visitor", NormalizedName = "VISITOR" });
            Insert.IntoTable(TableName).Row(new IdentityRole { Name = "Student", NormalizedName = "STUDENT" });
            Insert.IntoTable(TableName).Row(new IdentityRole { Name = "Employee", NormalizedName = "EMPLOYEE" });
            Insert.IntoTable(TableName).Row(new IdentityRole { Name = "Admin", NormalizedName = "Admin" });
        }
    }
}
