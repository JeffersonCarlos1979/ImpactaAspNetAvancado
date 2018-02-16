namespace Loja.Repositorios.SqlServer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddprodutoEmLeilao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Emleilao", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "Emleilao");
        }
    }
}
