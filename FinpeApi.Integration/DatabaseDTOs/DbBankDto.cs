namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbBankDto : DbEntity
    {
        public string Name { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.Banks (Name) VALUES(@Name);";
        public override string GetName() => "finpedb.dbo.Banks";
    }
}
