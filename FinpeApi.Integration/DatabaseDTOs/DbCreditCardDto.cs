namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbCreditCardDto : DbEntity
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public int EndNumbers { get; set; }
        public int ClosingDay { get; set; }
        public int PaymentDay { get; set; }
        public int CategoryId { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.CreditCards (Name, Owner, EndNumbers, ClosingDay, CategoryId, PaymentDay) VALUES(@Name, @Owner, @EndNumbers, @ClosingDay, @CategoryId, @PaymentDay);";
        public override string GetName() => "finpedb.dbo.CreditCards";
    }
}
