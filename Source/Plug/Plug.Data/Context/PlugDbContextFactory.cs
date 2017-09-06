using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Plug.Data
{
    /// <summary>
    /// Concrete Class of DB Contest Factory Interface - Plug Application
    /// </summary>
    public class PlugDbContextFactory : IDbContextFactory
    {
        #region Const

        const string ConnectionStringName = "PlugDBCon";

        const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=PlugDB;Trusted_Connection=True;";

        #endregion

        #region Fields

        private PlugDbContext _context;

        private IConfiguration configuration;

        #endregion

        #region Constructor

        public PlugDbContextFactory(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public PlugDbContextFactory()
        {
            configuration = null;
        }

        #endregion

        #region Implmentation

        public PlugDbContext Get()
        {
            if (_context == null)
            {
                InitialiseContext();
            }

            return _context;
        }

        #endregion

        #region Helpers

        private void InitialiseContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlugDbContext>();

            if (configuration == null)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            }
            _context = new PlugDbContext(optionsBuilder.Options);
        }

        #endregion
    }
}
