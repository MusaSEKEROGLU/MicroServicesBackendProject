namespace Microservices.Beverages.Api.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CategoryCollectionName { get; set; }
        public string BeveragesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
