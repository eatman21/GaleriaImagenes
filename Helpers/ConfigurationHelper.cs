using Microsoft.Extensions.Configuration;

namespace GaleriaImagenes.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration? _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    _configuration = builder.Build();
                }
                return _configuration;
            }
        }

        public static string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name)
                ?? throw new InvalidOperationException($"Connection string '{name}' not found in configuration.");
        }
    }
}
