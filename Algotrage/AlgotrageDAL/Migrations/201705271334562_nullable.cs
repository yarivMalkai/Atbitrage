namespace AlgotrageDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arbitrages", "FindTime", c => c.DateTime());
            AlterColumn("dbo.Arbitrages", "ExpireTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arbitrages", "ExpireTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Arbitrages", "FindTime", c => c.DateTime(nullable: false));
        }
    }
}
