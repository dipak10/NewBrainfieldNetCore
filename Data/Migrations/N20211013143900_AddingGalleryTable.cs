using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211013143900)]
    public class N20211013143900_AddingGalleryTable : Migration
    {
        public const string TableName = "tblGallery";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("GalleryID").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("ImageName").AsCustom("varchar(max)").Nullable();
        }
    }
}
