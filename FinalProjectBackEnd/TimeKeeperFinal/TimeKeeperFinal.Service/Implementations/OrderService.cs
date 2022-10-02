using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.OrderDTO;
using TimeKeeperFinal.Service.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtManager _jwtManager;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IJwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _jwtManager = jwtManager;
        }

        public async Task<List<OrderListDTO>> GetAllAsync()
        {  
            List<OrderListDTO> orderListDTOs = _mapper.Map<List<OrderListDTO>>(await _unitOfWork.OrderRepository.GetAllAsync());
            return orderListDTOs;
        }

        public async Task<List<OrderListDTO>> GetAllForUsersAsync(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new NotFoundException("User not found");


            List<OrderListDTO> orderListDTOs = _mapper.Map<List<OrderListDTO>>(await _unitOfWork.OrderRepository.GetAllForUsersAsync(o => !o.IsDeleted && o.AppUserId == appUser.Id));
            return orderListDTOs;
        }

        public async Task<OrderGetDTO> GetByIdAsync(int? id,string username)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new NotFoundException("User not found");

            OrderGetDTO orderGetDTO = _mapper.Map<OrderGetDTO>(await _unitOfWork.OrderRepository.GetAsync(o=>o.Id==id && o.AppUserId==appUser.Id));
            return orderGetDTO;
        }

        public async Task PostAsync(OrderPostDTO orderPostDTO)
        {
            Order order = _mapper.Map<Order>(orderPostDTO);

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();
        }
    }
}
