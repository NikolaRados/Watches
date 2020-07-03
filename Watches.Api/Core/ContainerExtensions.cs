using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watches.Application;
using Watches.Application.Commands;
using Watches.Application.Email;
using Watches.Application.Queries;
using Watches.Implementation.Commands;
using Watches.Implementation.Email;
using Watches.Implementation.Logging;
using Watches.Implementation.Queries;
using Watches.Implementation.Validators;

namespace Watches.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<IGetProductsQuery, EfGetProductsQuery>();
            services.AddTransient<IGetOneProductQuery, EfGetOneProductQuery>();
            services.AddTransient<IGetOneBrandQuery, EfGetOneBrandQuery>();
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<IGetBrandsQuery, EfGetBrandsQuery>();
            services.AddTransient<IDeleteBrandCommand, EfDeleteBrandCommand>();
            services.AddTransient<IUpdateBrandCommand, EfUpdateBrandCommand>();
            services.AddTransient<IDeleteProductCommand, EfDeleteDeleteProductCommand>();
            services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
            services.AddTransient<ICreateBrandCommand, EfCreateBrandCommand>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
            services.AddTransient<IGetOneOrderQuery, EfGetOneOrderQuery>();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IDeleteOrderCommand, EfDeleteOrderCommand>();
            services.AddTransient<IUpdateOrderCommand, EfUpdateOrderCommand>();
            services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
            services.AddTransient<IGetOneCommentQuery, EfGetOneCommentQuery>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, EfUpdateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetOneUserQuery, EfGetOneUserQuery>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IGetUseCaseLogQuery, EfGetUseCaseLogQuery>();

            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<UpdateBrandValidator>();
            services.AddTransient<CreateBrandValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<UpdateOrderValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<UpdateCommentValidator>();

            services.AddTransient<IEmailSender, SmptEmailSender>();
            services.AddTransient<UseCaseExecutor>();
            services.AddControllers();
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;
                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;
                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });
        }

        public static void AddJwt(this IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
