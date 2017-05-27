namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scrapingInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "ScrapingInfo_GameListExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_DateExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_DateAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_DateFormat", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_HomeTeamNameExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_HomeTeamAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_AwayTeamNameExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_AwayTeamAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_HomeRatioExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_HomeRatioAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_RatioXExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_RatioXAttribute", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_AwayRatioExpression", c => c.String());
            AddColumn("dbo.Sites", "ScrapingInfo_AwayRatioAttribute", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "ScrapingInfo_AwayRatioAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_AwayRatioExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_RatioXAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_RatioXExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_HomeRatioAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_HomeRatioExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_AwayTeamAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_AwayTeamNameExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_HomeTeamAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_HomeTeamNameExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_DateFormat");
            DropColumn("dbo.Sites", "ScrapingInfo_DateAttribute");
            DropColumn("dbo.Sites", "ScrapingInfo_DateExpression");
            DropColumn("dbo.Sites", "ScrapingInfo_GameListExpression");
        }
    }
}
