using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Data.Migrations
{
    [Migration(20211006195800)]
    public class N20211006195800_AddingNewsTable : Migration
    {
        public const string TableName = "tblNews";
        public const string NewsID = "ChapterID";
        public const string NewsHeadline = "NewsHeadline";
        public const string NewsDetail = "NewsDetail";
        public const string ImageName = "ImageName";
        public const string CreatedDate = "CreatedDate";
        public const string IsActive = "IsActive";

        public override void Down()
        {
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn(NewsID).AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn(NewsHeadline).AsCustom("varchar(max)").NotNullable()
                .WithColumn(NewsDetail).AsCustom("varchar(max)").NotNullable()
                .WithColumn(CreatedDate).AsDateTime().NotNullable()
                .WithColumn(IsActive).AsBoolean().NotNullable();
        }
    }
}
