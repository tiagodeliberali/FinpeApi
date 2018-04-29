using System;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbStatementDto: DbEntity
    {
        
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int Direction { get; set; }
        public int CategoryId { get; set; }

        public override string GetInsert()
        {
            return "INSERT INTO finpedb.dbo.Statements (Description, Amount, DueDate, Paid, Direction, CategoryId, PaymentDate) VALUES(@Description, @Amount, @DueDate, @Paid, @Direction, @CategoryId, @PaymentDate);";
        }

        public override string GetName()
        {
            return "finpedb.dbo.Statements";
        }
    }
}
