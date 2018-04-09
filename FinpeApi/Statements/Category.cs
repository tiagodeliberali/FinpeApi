namespace FinpeApi.Statements
{
    public class Category
    {
        private Category() { }

        public static Category Create(string name)
        {
            return new Category()
            {
                Name = name
            };
        }

        public int Id { get; set; }
        public string Name { get; private set; }
    }
}
