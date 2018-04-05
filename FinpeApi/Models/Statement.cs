using System;

namespace FinpeApi.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public StatementDirection Direction { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
