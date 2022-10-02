using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.IRepositories;

namespace TimeKeeperFinal.Core
{
    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }
        IModelRepository ModelRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IColorRepository ColorRepository { get; }
        IProductItemRepository ProductItemRepository { get; }
        ITagRepository TagRepository { get; }
        ISliderRepository SliderRepository { get; }
        IBlogRepository BlogRepository { get; }
        ISizeRepository SizeRepository { get; }
        IAddressInformationRepository AddressInformationRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
