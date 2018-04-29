using System;

namespace FinpeApi.Integration.DatabaseDTOs
{
    public class DbBankStatementDto : DbEntity
    {
        public int BankId { get; set; }
        public DateTime ExecutionDate { get; set; }
        public decimal Amount { get; set; }

        public override string GetInsert() => "INSERT INTO finpedb.dbo.BankStatements (ExecutionDate, Amount, BankId) VALUES(@ExecutionDate, @Amount, @BankId);";
        public override string GetName() => "finpedb.dbo.BankStatements";
    }
}
