using System;
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

        public static Bank Create(int id, string name, IEnumerable<BankStatement> monthStatements)
        {
            if (id >= 0)
                throw new ArgumentException("Must supply a bank with valid id", "id");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Must supply a bank with valid name", "name");

            if (monthStatements == null)
                throw new ArgumentNullException("monthStatements");

            return new Bank()
            {
                Id = id,
                Name = name,
                BankStatements = monthStatements.ToList()
            };
        }

        public BankStatement GetLatestStatement() => BankStatements.Last();
    }
}
