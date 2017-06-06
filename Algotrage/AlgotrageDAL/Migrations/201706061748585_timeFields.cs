namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "ScrapingInfo_TimeExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_TimeAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_TimeFormat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "ScrapingInfo_TimeFormat");
            DropColumn("dbo.Sites", "ScrapingInfo_TimeAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_TimeExpression");
        }
    }
}
