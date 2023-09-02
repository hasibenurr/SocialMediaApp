using SocialMediaWebApi.Entities.IEntities;

namespace SocialMediaWebApi.Entities
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public DatabaseSettings()
        {
        }

        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
