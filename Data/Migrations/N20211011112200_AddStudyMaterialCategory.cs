using FluentMigrator;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211011112200)]
    public class N20211011112200_AddStudyMaterialCategory : Migration
    {
        public const string TableName = "tblStudyMaterialCategories";
        public const string StudyMaterialCategoryID = "StudyMaterialCategoryID";
        public const string StudyMaterialCategoryName = "StudyMaterialCategoryName";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(StudyMaterialCategoryID).AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn(StudyMaterialCategoryName).AsCustom("varchar(max)").Nullable();
        }
    }
}
