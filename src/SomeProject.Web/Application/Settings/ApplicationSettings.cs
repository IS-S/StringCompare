namespace SomeProject.Web.Application.Settings
{
    public class ApplicationSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
