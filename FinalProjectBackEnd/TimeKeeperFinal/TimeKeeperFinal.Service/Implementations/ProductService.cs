using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.ProductDTO;
using TimeKeeperFinal.Service.Extentions;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Product product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null)
            {
                throw new NotFoundException("product not found");
            }

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProductListDTO>> GetAllAsync()
        {
            List<ProductListDTO> productListDTOs = _mapper.Map<List<ProductListDTO>>(await _unitOfWork.ProductRepository.GetAllAsync(/*p => !p.IsDeleted,*/"ProductImages"));
            return productListDTOs;
        }

        public async Task<List<ProductListDTO>> GetAllForUsersAsync()
        {
            List<ProductListDTO> productListDTOs = _mapper.Map<List<ProductListDTO>>(await _unitOfWork.ProductRepository.GetAllForUsersAsync(p => !p.IsDeleted, "ProductImages"));
            return productListDTOs;
        }

        public async Task<PagenetedListDTO<ProductListDTO>> GetAllPageIndexAsync(int pageIndex)
        {
            List<ProductListDTO> productListDTOs = _mapper.Map<List<ProductListDTO>>(await _unitOfWork.ProductRepository.GetAllAsync(/*p => !p.IsDeleted,*/ "Brand","Category"));
            PagenetedListDTO<ProductListDTO> pagenetedListDTO = new PagenetedListDTO<ProductListDTO>(productListDTOs, pageIndex, 12);
           

            return pagenetedListDTO;
        }

        public async Task<ProductGetDTO> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            ProductGetDTO productGetDTO = _mapper.Map<ProductGetDTO>(await _unitOfWork.ProductRepository.GetAsync(p => p.Id == id,"ProductImages"));
            return productGetDTO;
        }

        public async Task PostAsync(ProductPostDTO productPostDTO)
        {
            if (await _unitOfWork.ProductRepository.IsExistsAsync(p => p.Name == productPostDTO.Name))
            {
                throw new AlreadyExistsException($"{productPostDTO.Name} is exists");
            }

            Product product = _mapper.Map<Product>(productPostDTO);
            if (productPostDTO.FirstFile != null)
            {
                if (productPostDTO.FirstFile.CheckFileContextType("image/jpeg"))
                {
                    throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                }
                if (productPostDTO.FirstFile.CheckFileSize(2000))
                {
                    throw new BadRequestException("Please Select Coorect Image Size. Maximum 2 MB");
                }

                product.MainImage = await productPostDTO.FirstFile.CreateFileAsync(_env, "downloads");
            }
            if (productPostDTO.SecondFile != null)
            {
                if (productPostDTO.SecondFile.CheckFileContextType("image/jpeg"))
                {
                    throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                }
                if (productPostDTO.SecondFile.CheckFileSize(2000))
                {
                    throw new BadRequestException("Please Select Coorect Image Size. Maximum 2 MB");
                }
                product.SecondImage = await productPostDTO.SecondFile.CreateFileAsync(_env, "downloads");
            }
            if (productPostDTO.Files != null && productPostDTO.Files.Count > 0)
            {
                if (productPostDTO.Files.Count > 5)
                {
                    throw new BadRequestException("Can You Select Maximum 5 Images");
                }
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (IFormFile file in productPostDTO.Files)
                {
                    if (file.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select A Correct Image type. Example Jpeg Or Jpg");
                    }
                    if (file.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 2 MB");
                    }
                    ProductImages images = new ProductImages
                    {
                        Image = await file.CreateFileAsync(_env, "downloads")
                    };
                    productImages.Add(images);
                }
                product.ProductImages = productImages;
            }
            if (productPostDTO.TagIds != null && productPostDTO.TagIds.Count > 0)
            {
                List<ProductTag> productTags = new List<ProductTag>();

                foreach (int tagId in productPostDTO.TagIds)
                {
                    if (!await _unitOfWork.TagRepository.IsExistsAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        throw new BadRequestException("tag is incorrect");
                    }
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }

                product.ProductTags = productTags;
            }
            if (product.Count > 0)
            {
                product.Availability = true;
            }
            product.BasketCount = 0;
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, ProductPutDTO productPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (productPutDTO.Id != id)
            {
                throw new BadRequestException("id is not matched");
            }

            Product product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id == id && !p.IsDeleted, "ProductImages", "ProductTags");
            if (product == null)
            {
                throw new NotFoundException("product not found");
            }

            if (await _unitOfWork.ProductRepository.IsExistsAsync(p => p.Id != productPutDTO.Id && p.Name == productPutDTO.Name))
            {
                throw new AlreadyExistsException($"{productPutDTO.Name} is exists");
            }

            if (productPutDTO.FirstFile != null)
            {
                if (product.MainImage != null)
                {

                    if (productPutDTO.FirstFile.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                    }
                    if (productPutDTO.FirstFile.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select Coorect Image Size. Maximum 2 MB");
                    }
                    string fullpath = Path.Combine(_env.WebRootPath, "downloads", product.MainImage);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                product.MainImage = await productPutDTO.FirstFile.CreateFileAsync(_env, "downloads");

            }

            if (productPutDTO.SecondFile != null)
            {
                if (product.SecondImage != null)
                {

                    if (productPutDTO.SecondFile.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select Correct Image Type. Example Jpeg or Jpg");
                    }
                    if (productPutDTO.SecondFile.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select Coorect Image Size. Maximum 2 MB");
                    }
                    string fullpath = Path.Combine(_env.WebRootPath, "downloads", product.SecondImage);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                product.SecondImage = await productPutDTO.SecondFile.CreateFileAsync(_env, "downloads");

            }
            if (productPutDTO.Files != null && productPutDTO.Files.Count > 0)
            {
                int selectedimage = 5 - product.ProductImages.Count;
                if (selectedimage == 0)
                {
                    throw new BadRequestException($" Selected Max Images");
                }
                if (productPutDTO.Files != null && productPutDTO.Files.Count > 0 && selectedimage < product.ProductImages.Count)
                {
                    throw new BadRequestException($" You Can Select {selectedimage} Image");
                }
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (IFormFile file in productPutDTO.Files)
                {
                    if (file.CheckFileContextType("image/jpeg"))
                    {
                        throw new BadRequestException("Please Select A Correct Image type. Example Jpeg Or Jpg");
                    }
                    if (file.CheckFileSize(2000))
                    {
                        throw new BadRequestException("Please Select A Correct Image Size. Maximum 2 MB");
                    }
                    ProductImages images = new ProductImages
                    {
                        Image = await file.CreateFileAsync(_env, "downloads")
                    };
                    productImages.Add(images);
                }
                if (product.ProductImages != null && productPutDTO.Files.Count >= 0)
                {
                    product.ProductImages.AddRange(productImages);
                }
                else
                {
                    product.ProductImages = productImages;
                }
            }
            if (productPutDTO.TagIds != null && productPutDTO.TagIds.Count > 0)
            {
                List<ProductTag> productTags = new List<ProductTag>();

                foreach (int tagId in productPutDTO.TagIds)
                {
                    if (!await _unitOfWork.TagRepository.IsExistsAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        throw new BadRequestException("tag is incorrect");
                    }
                    //if (product.ProductTags != null && product.ProductTags.Count > 0)
                    //{
                    //    product.ProductTags.RemoveRange(tagId,productTags.Count);
                    //}

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }
                product.ProductTags = productTags;
            }

            product.Name = productPutDTO.Name;
            product.Description = productPutDTO.Description;
            product.Price = productPutDTO.Price;
            product.DiscountPrice = productPutDTO.DiscountPrice;
            product.Code = productPutDTO.Code;
            product.Count = productPutDTO.Count;
            if (product.Count > 0)
            {
                product.Availability = true;
            }
            product.BrandId = productPutDTO.BrandId;
            product.ModelId = productPutDTO.ModelId;
            product.CategoryId = productPutDTO.CategoryId;
            product.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();

        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Product product = await _unitOfWork.ProductRepository.GetAsync(p => p.Id == id && p.IsDeleted);

            if (product == null)
            {
                throw new NotFoundException("product not found");
            }

            product.IsDeleted = false;
            product.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
