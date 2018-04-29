namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbCategoryDto: DbEntity
    {
        public string Name { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.Categories (Name) VALUES(@Name);";
        public override string GetName() => "finpedb.dbo.Categories";
    }
}
