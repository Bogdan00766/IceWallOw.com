using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public IAddressRepository AddressRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
        Task SaveAsync();

    }
}
