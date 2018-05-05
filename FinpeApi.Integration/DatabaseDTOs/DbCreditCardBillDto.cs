using System;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbCreditCardBillDto : DbEntity
    {
        public int CreditCardId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DueDate { get; set; }
        public int ClosingDay { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.CreditCardBills (CreditCardId, CategoryId, DueDate, ClosingDay, Paid, PaymentDate) VALUES(@CreditCardId, @CategoryId, @DueDate, @ClosingDay, @Paid, @PaymentDate);";
        public override string GetName() => "finpedb.dbo.CreditCardBills";
    }
}
