using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211013112000)]
    public class N20211013112000_AddingVideosTable : Migration
    {
        public const string TableName = "tblVideos";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("VideosID").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Title").AsCustom("varchar(max)").Nullable()
                .WithColumn("VideoURL").AsCustom("varchar(max)").Nullable()
                .WithColumn("IsFree").AsBoolean().Nullable()
                .WithColumn("CreatedOn").AsDateTime().Nullable();            
        }
    }
}
