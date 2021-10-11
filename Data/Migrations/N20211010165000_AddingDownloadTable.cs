using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211010165000)]
    public class N20211010165000_AddingDownloadTable : Migration
    {
        public const string TableName = "tblDownloads";
      
        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                 .WithColumn("DownloadsID").AsInt32().Identity().NotNullable().PrimaryKey()
                 .WithColumn("Title").AsCustom("varchar(max)").Nullable()
                 .WithColumn("FileName").AsCustom("varchar(max)").Nullable()                 
                 .WithColumn("IsFree").AsBoolean().Nullable()
                 .WithColumn("UploadOn").AsDateTime().Nullable()
                 .WithColumn("Section").AsInt32().Nullable();
        }
    }
}
