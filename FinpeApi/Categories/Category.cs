using System;

namespace FinpeApi.Categories
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Category() { }

        public static Category Create(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentException("Must supply a valid category name", nameof(categoryName));

            return new Category()
            {
                Name = categoryName
            };
        }

        public bool Exists() => Id > 0;
    }
}
