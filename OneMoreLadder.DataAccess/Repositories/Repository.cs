using Microsoft.Extensions.Logging;

namespace OneMoreLadder.DataAccess.Repositories
{
    public class Repository<L>
    {
        private readonly IDataAccessSettings _settings;
        private readonly ILogger<L> _logger;

        public Repository(IDataAccessSettings dataAccessSettings, ILogger<L> logger)            
        {
            _logger = logger;
            _settings = dataAccessSettings;
        }

        internal async Task<T> ExecuteAsync<T>(Func<ApplicationDbContext, Task<T>> dbOperation)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext(_settings.ConnectionString))
                {
                    return await dbOperation(dbContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot execute data base operation");
                throw;
            }
        }

        internal async Task ExecuteAsync(Func<ApplicationDbContext, Task> dbOperation)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext(_settings.ConnectionString))
                {
                    await dbOperation(dbContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot execute data base operation");
                throw;
            }
        }


    }
}
