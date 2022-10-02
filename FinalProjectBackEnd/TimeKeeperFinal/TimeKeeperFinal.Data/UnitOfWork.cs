using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.IRepositories;
using TimeKeeperFinal.Data.Repositories;

namespace TimeKeeperFinal.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly BrandRepository brandRepository;
        private readonly ModelRepository modelRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ProductRepository productRepository;
        private readonly ColorRepository colorRepository;
        private readonly ProductItemRepository productItemRepository;
        private readonly TagRepository tagRepository;
        private readonly SliderRepository sliderRepository;
        private readonly BlogRepository blogRepository;
        private readonly SizeRepository sizeRepository;
        private readonly AddressInformationRepository addressInformationRepository;
        private readonly OrderRepository orderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBrandRepository BrandRepository => brandRepository != null ? brandRepository : new BrandRepository(_context);
        public IModelRepository ModelRepository => modelRepository != null ? modelRepository : new ModelRepository(_context);
        public ICategoryRepository CategoryRepository => categoryRepository != null ? categoryRepository : new CategoryRepository(_context);
        public IProductRepository ProductRepository => productRepository != null ? productRepository : new ProductRepository(_context);
        public IColorRepository ColorRepository => colorRepository != null ? colorRepository : new ColorRepository(_context);
        public IProductItemRepository ProductItemRepository => productItemRepository != null ? productItemRepository : new ProductItemRepository(_context);
        public ITagRepository TagRepository => tagRepository != null ? tagRepository : new TagRepository(_context); 
        public ISliderRepository SliderRepository => sliderRepository != null ? sliderRepository : new SliderRepository(_context);
        public IBlogRepository BlogRepository => blogRepository != null ? blogRepository : new BlogRepository(_context);
        public ISizeRepository SizeRepository => sizeRepository != null ? sizeRepository : new SizeRepository(_context);
        public IAddressInformationRepository AddressInformationRepository => addressInformationRepository != null ? addressInformationRepository : new AddressInformationRepository(_context);
        public IOrderRepository OrderRepository => orderRepository != null ? orderRepository : new OrderRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
