namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameSiteRatios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        HomeRatio = c.Double(nullable: false),
                        DrawRatio = c.Double(nullable: false),
                        AwayRatio = c.Double(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.GameId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamPossibleNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        PossibleName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Arbitrages", "HomeRatioSiteId", "dbo.Sites");
            DropForeignKey("dbo.Arbitrages", "GameId", "dbo.Games");
            DropForeignKey("dbo.Arbitrages", "DrawRatioSiteId", "dbo.Sites");
            DropForeignKey("dbo.Arbitrages", "AwayRatioSiteId", "dbo.Sites");
            DropForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Games", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "AwayTeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamPossibleNames", new[] { "TeamId" });
            DropIndex("dbo.Games", new[] { "AwayTeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeamId" });
            DropIndex("dbo.GameSiteRatios", new[] { "SiteId" });
            DropIndex("dbo.GameSiteRatios", new[] { "GameId" });
            DropIndex("dbo.Arbitrages", new[] { "AwayRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "DrawRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "HomeRatioSiteId" });
            DropIndex("dbo.Arbitrages", new[] { "GameId" });
            DropTable("dbo.Users");
            DropTable("dbo.TeamPossibleNames");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
            DropTable("dbo.GameSiteRatios");
            DropTable("dbo.Sites");
            DropTable("dbo.Arbitrages");
        }
    }
}
