using AutoMapper;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.ProductItemDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class ProductItemService : IProductItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            ProductItem productItem = await _unitOfWork.ProductItemRepository.GetAsync(p => p.Id == id && !p.IsDeleted);
            if (productItem == null)
            {
                throw new NotFoundException("productitem notfound");
            }
            productItem.IsDeleted = true;
            productItem.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProductItemListDTO>> GetAllAsync()
        {
            //List<ProductItemListDTO> productItemListDTOs = _mapper.Map<List<ProductItemListDTO>>(await _unitOfWork.ProductItemRepository.GetAllAsync(p => !p.IsDeleted,"Product", "Color", "Size"));

           
            List<ProductItemListDTO> productItemListDTOs = new List<ProductItemListDTO>();
            foreach (var item in await _unitOfWork.ProductItemRepository.GetAllAsync(/*p => !p.IsDeleted,*/ "Product", "Color", "Size"))
            {
                var dto = _mapper.Map<ProductItemListDTO>(item);
                dto.Color = item.Color.Name;
                dto.Product = item.Product.Name;
                dto.Price = item.Product.Price;
                dto.Size = item.Size.Name;
                dto.ColorCode = item.Color.Code;
                dto.MainImage = item.Product.MainImage;
                dto.SecondImage = item.Product.SecondImage;
                productItemListDTOs.Add(dto);
            }

            return productItemListDTOs;
        }

        public async Task<List<ProductItemListDTO>> GetAllForUsersAsync()
        {
            List<ProductItemListDTO> productItemListDTOs = new List<ProductItemListDTO>();
            foreach (var item in await _unitOfWork.ProductItemRepository.GetAllForUsersAsync(p => !p.IsDeleted, "Product", "Color", "Size"))
            {
                var dto = _mapper.Map<ProductItemListDTO>(item);
                dto.Color = item.Color.Name;
                dto.Product = item.Product.Name;
                dto.Price = item.Product.Price;
                dto.Size = item.Size.Name;
                dto.ColorCode = item.Color.Code;
                dto.MainImage = item.Product.MainImage;
                dto.SecondImage = item.Product.SecondImage;
                productItemListDTOs.Add(dto);
            }

            return productItemListDTOs;
        }

        public async Task<ProductItemGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            var product = await _unitOfWork.ProductItemRepository.GetAsync(p => p.Id == id, "Product.Brand", "Product.Model", "Product.Category","Product.ProductImages", "Color", "Size");
            //ProductItemGetDTO productItemGetDTO = _mapper.Map<ProductItemGetDTO>(await _unitOfWork.ProductItemRepository.GetAsync(p => p.Id == id,"Product", "Color", "Size"));
            ProductItemGetDTO productItemGetDTO = _mapper.Map<ProductItemGetDTO>(product);
            productItemGetDTO.Color = product.Color.Name;
            productItemGetDTO.ColorCode = product.Color.Code;
            productItemGetDTO.Size = product.Size.Name;
            productItemGetDTO.ProductName = product.Product.Name;
            productItemGetDTO.Description = product.Product.Description;
            productItemGetDTO.Price = product.Product.Price;
            productItemGetDTO.DiscountPrice = product.Product.DiscountPrice;
            productItemGetDTO.BrandName = product.Product.Brand.Name;
            productItemGetDTO.CategoryName = product.Product.Category.Name;
            productItemGetDTO.ProductCode = product.Product.Code;
            productItemGetDTO.Availability = product.Product.Availability;
            productItemGetDTO.MainImage=product.Product.MainImage;
            productItemGetDTO.SecondImage=product.Product.SecondImage;

            return productItemGetDTO;
        }

        public async Task PostAsync(ProductItemPostDTO productItemPostDTO)
        {
            ProductItem productItem = _mapper.Map<ProductItem>(productItemPostDTO);
            await _unitOfWork.ProductItemRepository.AddAsync(productItem);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, ProductItemPutDTO productItemPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            if (productItemPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not match");
            }
            ProductItem productItem = await _unitOfWork.ProductItemRepository.GetAsync(p => p.Id == id && !p.IsDeleted);
            if (productItem == null)
            {
                throw new NotFoundException("productitem notfound");
            }
            productItem.ProductId = productItemPutDTO.ProductId; 
            productItem.ColorId=productItemPutDTO.ColorId;
            productItem.SizeId=productItemPutDTO.SizeId;
            productItem.Count=productItemPutDTO.Count;
            productItem.UpdatedAt=DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }
            ProductItem productItem = await _unitOfWork.ProductItemRepository.GetAsync(p => p.Id == id && p.IsDeleted);
            if (productItem==null)
            {
                throw new NotFoundException("productitem notfound");
            }
            productItem.IsDeleted = false;
            productItem.DeletedAt =null;

            await _unitOfWork.CommitAsync();
        }

        
    }
}

