namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbBankDto : DbEntity
    {
        public string Name { get; set; }

        public override string GetInsert()
        {
            return "INSERT INTO finpedb.dbo.Banks (Name) VALUES(@Name);";
        }

        public override string GetName()
        {
            return "finpedb.dbo.Banks";
        }
    }
}
