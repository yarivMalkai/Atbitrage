namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arbitrages", "ProfitPercent", c => c.Double(nullable: false));
            DropColumn("dbo.Arbitrages", "ProfitPervent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Arbitrages", "ProfitPervent", c => c.Double(nullable: false));
            DropColumn("dbo.Arbitrages", "ProfitPercent");
        }
    }
}
