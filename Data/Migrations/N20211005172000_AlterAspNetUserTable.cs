using FluentMigrator;
using Microsoft.AspNetCore.Identity;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211005172000)]
    public class N20211005172000_AlterAspNetUserTable : Migration
    {

        public readonly string TableName = "AspNetUsers";

        public readonly string FullName = "FullName";
        public readonly string CreatedOn = "CreatedOn";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Alter.Table(TableName).AddColumn(FullName).AsString().SetExistingRowsTo(null);
            Alter.Table(TableName).AddColumn(CreatedOn).AsDateTime().SetExistingRowsTo(null);
        }
    }
}
