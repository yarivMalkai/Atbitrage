namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arbitrageTableAdd2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games");
            DropForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites");
            CreateTable(
                "dbo.Arbitrages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        HomeRatio = c.Double(nullable: false),
                        HomeRatioSiteId = c.Int(nullable: false),
                        DrawRatio = c.Double(nullable: false),
                        DrawRatioSiteId = c.Int(nullable: false),
                        AwayRatio = c.Double(nullable: false),
                        AwayRatioSiteId = c.Int(nullable: false),
                        HomeBetPercent = c.Double(nullable: false),
                        DrawBetPercent = c.Double(nullable: false),
                        AwayBetPercent = c.Double(nullable: false),
                        ProfitPervent = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        FindTime = c.DateTime(nullable: false),
                        ExpireTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.AwayRatioSiteId)
                .ForeignKey("dbo.Sites", t => t.DrawRatioSiteId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Sites", t => t.HomeRatioSiteId)
                .Index(t => t.GameId)
                .Index(t => t.HomeRatioSiteId)
                .Index(t => t.DrawRatioSiteId)
                .Index(t => t.AwayRatioSiteId);
            
            AddForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games", "Id");
            AddForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams", "TeamId");
            AddForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games");
            DropForeignKey("dbo.Arbitrages", "HomeRatioSiteId", "dbo.Sites");
            DropForeignKey("dbo.Arbitrages", "GameId", "dbo.Games");
            DropForeignKey("dbo.Arbitrages", "DrawRatioSiteId", "dbo.Sites");
            DropForeignKey("dbo.Arbitrages", "AwayRatioSiteId", "dbo.Sites");
            DropIndex("dbo.Arbitrages", new[] { "AwayRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "DrawRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "HomeRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "GameId" });
            DropTable("dbo.Arbitrages");
            AddForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams", "TeamId", cascadeDelete: true);
            AddForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}
