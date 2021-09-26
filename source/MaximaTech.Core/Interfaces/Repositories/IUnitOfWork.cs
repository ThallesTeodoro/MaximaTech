namespace MaximaTech.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }

        void Commit();
        void Rollback();
    }
}