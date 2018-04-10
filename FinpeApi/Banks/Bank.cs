using System.Collections.Generic;
using System.Linq;

namespace FinpeApi.Banks
{
    public class Bank
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<BankStatement> BankStatements { get; private set; }

        private Bank() { }

        public BankStatement GetLatestStatement() => BankStatements.Last();

        public void SetLatestStatement(BankStatement lastStatement) => BankStatements = new List<BankStatement> { lastStatement };
    }
}
