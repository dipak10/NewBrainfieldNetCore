using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006202200)]
    public class N20211006202200_AddDownloadCategory : Migration
    {
        public const string TableName = "tblDownloadCategory";
        public const string DownloadCategoryID = "DownloadCategoryID";
        public const string DownloadCategoryName = "DownloadCategoryName";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(DownloadCategoryID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(DownloadCategoryName).AsString().NotNullable();
        }
    }
}
