namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AwayTeam_TeamId = c.Int(),
                        HomeTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_TeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_TeamId)
                .Index(t => t.AwayTeam_TeamId)
                .Index(t => t.HomeTeam_TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamPossibleNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        PossibleName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
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
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.SiteId);
            
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
            DropForeignKey("dbo.Games", "HomeTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameSiteRatios", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.GameSiteRatios", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "AwayTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPossibleNames", "TeamId", "dbo.Teams");
            DropIndex("dbo.GameSiteRatios", new[] { "SiteId" });
            DropIndex("dbo.GameSiteRatios", new[] { "GameId" });
            DropIndex("dbo.TeamPossibleNames", new[] { "TeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeam_TeamId" });
            DropIndex("dbo.Games", new[] { "AwayTeam_TeamId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sites");
            DropTable("dbo.GameSiteRatios");
            DropTable("dbo.TeamPossibleNames");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
        }
    }
}
