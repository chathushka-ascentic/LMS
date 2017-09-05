namespace Plug.Data
{
    /// <summary>
    /// DB Context Factory Interface
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// Get a Plug DB Context
        /// </summary>
        /// <returns>Plug DB Context</returns>
        PlugDbContext Get();
    }
}
