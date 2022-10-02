using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.BlogDTO;
using TimeKeeperFinal.Service.Extentions;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id id required");
            }

            Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (blog == null)
            {
                throw new NotFoundException("blog not found");
            }

            blog.IsDeleted = true;
            blog.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<BlogListDTO>> GetAllAsync()
        {
            List<BlogListDTO> blogListDTOs = _mapper.Map<List<BlogListDTO>>(await _unitOfWork.BlogRepository.GetAllAsync(/*b => !b.IsDeleted*/));
            return blogListDTOs;
        }

        public async Task<List<BlogListDTO>> GetAllForUsersAsync()
        {
            List<BlogListDTO> blogListDTOs = _mapper.Map<List<BlogListDTO>>(await _unitOfWork.BlogRepository.GetAllForUsersAsync(b => !b.IsDeleted));
            return blogListDTOs;
        }

        public async Task<BlogGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id id required");
            }
            BlogGetDTO blogGetDTO = _mapper.Map<BlogGetDTO>(await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id));
            return blogGetDTO;
        }

        public async Task PostAsync(BlogPostDTO blogPostDTO)
        {
            if (await _unitOfWork.BlogRepository.IsExistsAsync(b=>b.Name==blogPostDTO.Name))
            {
                throw new AlreadyExistsException($"{blogPostDTO.Name} is exists");
            }
            Blog blog = _mapper.Map<Blog>(blogPostDTO);

            if (blogPostDTO.File != null)
            {
                blog.Image = await blogPostDTO.File.CreateFileAsync(_env, "blogs");
            }
            await _unitOfWork.BlogRepository.AddAsync(blog);
             await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, BlogPutDTO blogPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id id required");
            }

            if (blogPutDTO.Id != id)
            {
                throw new BadRequestException("id id not match");
            }

            Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && !b.IsDeleted);
            if (blog==null)
            {
                throw new NotFoundException("blog not found");
            }
            if (await _unitOfWork.BlogRepository.IsExistsAsync(b => b.Id != blogPutDTO.Id && b.Name==blogPutDTO.Name))
            {
                throw new AlreadyExistsException($"{blogPutDTO.Name} is exists");
            }

            if (blogPutDTO.File != null)
            {
                if (blog.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "blogs", blog.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                blog.Image = await blogPutDTO.File.CreateFileAsync(_env, "blogs");

            }

            blog.Name = blogPutDTO.Name;
            blog.MainBlog = blogPutDTO.MainBlog;
            blog.SubBlog = blogPutDTO.SubBlog;
            blog.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id id required");
            }

            Blog blog = await _unitOfWork.BlogRepository.GetAsync(b => b.Id == id && b.IsDeleted);
            if (blog == null)
            {
                throw new NotFoundException("blog not found");
            }

            blog.IsDeleted = false;
            blog.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
