using OneMoreLadder.DataAccess;

namespace OneMoreLadder
{
    public class DataAccessSettings : IDataAccessSettings
    {
        private const string DbConnectionString = "DbConnectionString";

        private readonly IConfiguration _configuration;
        public DataAccessSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ConnectionString
        {
            get
            {
                var cs = _configuration[DbConnectionString];
                return cs == null ? string.Empty : cs;
            }
        }

    }
}
