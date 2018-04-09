namespace FinpeApi.Statements
{
    public class Category
    {
        private int Id { get; set; }
        public string Name { get; private set; }

        private Category() { }

        public static Category Create(string categoryName)
        {
            return new Category()
            {
                Name = categoryName
            };
        }
    }
}
