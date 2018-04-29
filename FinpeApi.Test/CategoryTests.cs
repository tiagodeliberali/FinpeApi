using FinpeApi.Categories;
using System;
using Xunit;

namespace FinpeApi.Test
{
    public class CategoryTests
    {
        [Fact]
        public void CreateCategory()
        {
            // Arrange
            string name = "test";

            // Act
            var category = Category.Create(name);

            // Assert
            Assert.Equal(name, category.Name);
            Assert.Equal(0, category.Id);
            Assert.False(category.Exists());
        }

        [Fact]
        public void CategoryWithIdExists()
        {
            // Arrange
            string name = "test";
            var category = Category.Create(name);
            category.SetId(1);

            // Act
            var exists = category.Exists();

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void CategoryMustHaveValidName()
        {
            // Arrange
            string emptyName = string.Empty;

            // Act
            Action createCategory = () => Category.Create(emptyName);

            // Assert
            Assert.Throws<ArgumentException>(createCategory);
        }
    }
}
