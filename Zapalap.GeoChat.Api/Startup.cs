﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Akka;
using Akka.Actor;
using Zapalap.GeoChat.Api.AkkaActorSystem.Actors;

namespace Zapalap.GeoChat.Api
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

            var geoChatActorSystem = ActorSystem.Create("GeoChat");

            for (int i = 1; i <= 12; i++)
            {
                geoChatActorSystem.ActorOf(Props.Create(() => new RegionMaster(i)), $"RegionMaster:{i}");
            }

            services.AddSingleton(geoChatActorSystem);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}