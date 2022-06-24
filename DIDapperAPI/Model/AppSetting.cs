
namespace DIDapperAPI.Model
{
    public class AppSetting
    {
        public LoggingConfig? Logging { get; set; }

        public string? AllowedHosts { get; set; }
        public TestSection? TestSection { get; set; }
        public class LoggingConfig
        {
        }
    }

    public class TestSection 
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public TestSection? Alias { get; set; }
    }
}
