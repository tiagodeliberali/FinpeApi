using System;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbCreditCardStatementDto : DbEntity
    {
        public int BillId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime BuyDate { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.CreditCardStatements (BillId, Description, Amount, BuyDate) VALUES(@BillId, @Description, @Amount, @BuyDate);";
        public override string GetName() => "finpedb.dbo.CreditCardStatements";
    }
}
