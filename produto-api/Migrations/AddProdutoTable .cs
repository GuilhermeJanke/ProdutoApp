using FluentMigrator;

[Migration(202311060001)]
public class AddProdutoTable : Migration
{
    public override void Up()
    {
        Create.Table("Produtos")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Descricao").AsString().Nullable()
            .WithColumn("Preco").AsDecimal().NotNullable()
            .WithColumn("DataCadastro").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
    }

    public override void Down()
    {
        Delete.Table("Produtos");
    }
}
