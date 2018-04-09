using System;

namespace FinpeApi.Overviews
{
    public class StatementDto
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
