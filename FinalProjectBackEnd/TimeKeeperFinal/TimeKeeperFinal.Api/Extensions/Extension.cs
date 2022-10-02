using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RentalCarFinalProject.Service.Exceptions;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Data;
using TimeKeeperFinal.Service.Implementations;
using TimeKeeperFinal.Service.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Services;

namespace TimeKeeperFinal.Api.Extensions
{
    public static class Extension
    {
        public static void AddScoppedService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IProductItemService, ProductItemService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressInformationService, AddressInformationService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IEmailService, EmailService>();

        }

        public static void ExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                error =>
                {

                    error.Run(async context =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                        int statusCode = 500;
                        string message = "Internal Server Error";

                        if (feature.Error is AlreadyExistsException)
                        {
                            statusCode = 400;
                            message = feature.Error.Message;
                        }
                        else if (feature.Error is BadRequestException)
                        {
                            statusCode = 400;
                            message = feature.Error.Message;
                        }
                        else if (feature.Error is NotFoundException)
                        {
                            statusCode = 404;
                            message = feature.Error.Message;

                        }
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsync(message);
                    });
                });
        }
    }
}
