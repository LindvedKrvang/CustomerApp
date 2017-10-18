using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomerRestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var facade = new BLLFacade();

                var address = facade.AddressService.Create(new AddressBO() {
                    City = "Esbjerg",
                    Street = "Spangsbjergkirkevej",
                    Number = "103"
                });

                var address2 = facade.AddressService.Create(new AddressBO() {
                    City = "Ølgod",
                    Street = "Vestergade",
                    Number = "13"
                });

                var address3 = facade.AddressService.Create(new AddressBO() {
                    City = "Odense",
                    Street = "H.C. Andersenvej",
                    Number = "40"
                });

                var customer = facade.CustomerService.CreateCustomer(
                    new CustomerBO() {
                        FirstName = "Peter",
                        LastName = "Stegger",
                        AddressIds = new List<int>() { address.Id, address3.Id }
                    });
                facade.CustomerService.CreateCustomer(
                    new CustomerBO() {
                        FirstName = "Jeppe",
                        LastName = "Moritz",
                        AddressIds = new List<int>() { address.Id, address2.Id }

                    });

                facade.OrderService.CreateOrder(
                    new OrderBO() {
                        OrderDate = DateTime.Now.AddMonths(-1),
                        DeliveryDate = DateTime.Now.AddMonths(1),
                        CustomerId = customer.Id
                    });
            }

            app.UseMvc();
        }
    }
}