using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211010124500)]
    public class N20211010124500_UpdateBlogTable : Migration
    {
        public const string TableName = "tblBlogs";

        public const string BlogImage = "BlogImage";


        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Alter.Table(TableName)
                .AddColumn("BlogTitle").AsCustom("varchar(max)").Nullable()
                .AddColumn("BlogContent").AsCustom("varchar(max)").Nullable()
                .AddColumn(BlogImage).AsCustom("varchar(max)").Nullable();
        }
    }
}
