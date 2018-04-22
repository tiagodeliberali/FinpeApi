namespace FinpeApi.Test.Integration.DatabaseDTOs
{
    public class DbCategoryDto: DbEntity
    {
        public string Name { get; set; }

        public override string GetInsert()
        {
            return "INSERT INTO finpedb.dbo.Categories (Name) VALUES(@Name);";
        }

        public override string GetName()
        {
            return "finpedb.dbo.Categories";
        }
    }
}
