using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211013102200)]
    public class N20211013102200_AlterStudyMaterialFileTable : Migration
    {
        public const string TableName = "tblStudyMaterialFiles";

        public override void Down()
        {
            
        }

        public override void Up()
        {
            //Create.Table(TableName)
            //    .WithColumn("StudyMaterialFilesID").AsInt32().Identity().NotNullable().PrimaryKey()
            //    .WithColumn("StudyMaterialCategoryID").AsInt32().NotNullable()
            //    .WithColumn("Title").AsCustom("varchar(max)").Nullable()
            //    .WithColumn("Description").AsCustom("varchar(max)").Nullable()
            //    .WithColumn("FileName").AsCustom("varchar(max)").Nullable()
            //    .WithColumn("ImageName").AsCustom("varchar(max)").Nullable()
            //    .WithColumn("Price").AsDecimal(5, 2).Nullable()
            //    .WithColumn("CreatedOn").AsDateTime().Nullable();


            Alter.Table(TableName).AlterColumn("Price").AsDecimal(9, 2).Nullable();

        }
    }
}
