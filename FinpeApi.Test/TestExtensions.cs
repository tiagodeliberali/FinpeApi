using System;

namespace FinpeApi.Test
{
    public static class TestExtensions
    {
        public static void SetId(this Object entity, int id)
        {
            entity.SetProperty("Id", id);
        }

        public static void SetProperty<T>(this Object entity, string fieldName, T value)
        {
            var prop = entity.GetType().GetField(
                $"<{fieldName}>k__BackingField",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            prop.SetValue(entity, value);
        }
    }
}
