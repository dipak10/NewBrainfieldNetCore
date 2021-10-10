using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006200200)]
    public class N20211006200200_AddingBlogTable : Migration
    {
        public const string TableName = "tblBlogs";
        public const string BlogID = "BlogID";
        public const string BlogTitle = "BlogTitle";
        public const string BlogContent = "BlogContent";
        public const string BlogImage = "BlogImage";
        public const string CreatedDate = "CreatedDate";
        public const string IsActive = "IsActive";
        public const string IsAppOnly = "IsAppOnly";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(BlogID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(BlogTitle).AsString().NotNullable()
                .WithColumn(BlogContent).AsString().NotNullable()
                .WithColumn(CreatedDate).AsDateTime().NotNullable()
                .WithColumn(IsActive).AsBoolean().NotNullable()
                .WithColumn(IsAppOnly).AsBoolean().NotNullable();
        }
    }
}
