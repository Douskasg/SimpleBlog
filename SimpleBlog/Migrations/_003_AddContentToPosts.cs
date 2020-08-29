﻿using FluentMigrator;

namespace SimpleBlog.Migrations
{
    [Migration(3)]
    public class _003_AddContentToPosts : Migration
    {
        public override void Down()
        {
            Delete.Column("content").FromTable("posts");
        }

        public override void Up()
        {
            Create.Column("content").OnTable("posts").AsCustom("TEXT");
        }
    }
}