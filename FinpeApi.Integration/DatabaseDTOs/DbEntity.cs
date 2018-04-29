namespace FinpeApi.Integration.DatabaseDTOs
{
    public abstract class DbEntity
    {
        public int Id { get; set; }

        public abstract string GetName();
        public abstract string GetInsert();
    }
}
