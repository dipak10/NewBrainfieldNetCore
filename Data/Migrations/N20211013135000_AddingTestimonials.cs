using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211013135000)]
    public class N20211013135000_AddingTestimonials : Migration
    {
        public const string TableName = "tblTestimonials";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("TestimonailID").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("TestimonialBy").AsCustom("varchar(max)").Nullable()
                .WithColumn("TestimonialText").AsCustom("varchar(max)").Nullable()
                .WithColumn("StudentImage").AsCustom("varchar(max)").Nullable()
                .WithColumn("CreatedOn").AsDateTime().Nullable();
        }
    }
}
