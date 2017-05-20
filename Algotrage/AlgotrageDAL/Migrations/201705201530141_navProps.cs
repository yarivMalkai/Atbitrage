namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class navProps : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Games", new[] { "AwayTeam_TeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeam_TeamId" });
            DropColumn("dbo.Games", "AwayTeamId");
            DropColumn("dbo.Games", "HomeTeamId");
            RenameColumn(table: "dbo.Games", name: "AwayTeam_TeamId", newName: "AwayTeamId");
            RenameColumn(table: "dbo.Games", name: "HomeTeam_TeamId", newName: "HomeTeamId");
            AlterColumn("dbo.Games", "AwayTeamId", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "HomeTeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "HomeTeamId");
            CreateIndex("dbo.Games", "AwayTeamId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "AwayTeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeamId" });
            AlterColumn("dbo.Games", "HomeTeamId", c => c.Int());
            AlterColumn("dbo.Games", "AwayTeamId", c => c.Int());
            RenameColumn(table: "dbo.Games", name: "HomeTeamId", newName: "HomeTeam_TeamId");
            RenameColumn(table: "dbo.Games", name: "AwayTeamId", newName: "AwayTeam_TeamId");
            AddColumn("dbo.Games", "HomeTeamId", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "AwayTeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "HomeTeam_TeamId");
            CreateIndex("dbo.Games", "AwayTeam_TeamId");
        }
    }
}
