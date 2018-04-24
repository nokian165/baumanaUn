namespace baumanaUn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "Level");
            DropColumn("dbo.Courses", "HumanType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "HumanType", c => c.String());
            AddColumn("dbo.Courses", "Level", c => c.String());
        }
    }
}
