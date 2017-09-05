using Plug.DataRepositories;

namespace Plug.Business
{
    public abstract class ServiceBase
    {
        // Unit of work Field
        protected IUnitOfWork UnitOfWork { get; }

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
