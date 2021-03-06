﻿using Autofac;
using Autofac.Integration.WebApi;
using ConsoleApplicationOWIN.Formatter;
using ConsoleApplicationOWIN.IoC;
using ConsoleApplicationOWIN.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ValueProviders;

[assembly: OwinStartup(typeof(ConsoleApplicationOWIN.Startup))]

namespace ConsoleApplicationOWIN
{
    /// <summary>
    /// Startup de OWIN
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            ConfigureOAuth(appBuilder);
            ConfigureWebApi(appBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.EnableSwagger(c => c.SingleApiVersion("v3", "Curso Web Api 2.2")).EnableSwaggerUi();
            config.MapHttpAttributeRoutes();

            config.Formatters.Add(new CsvTypeFormatter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Services.Add(typeof(ValueProviderFactory), new HeaderValueProviderFactory());
            config.Services.Add(typeof(ValueProviderFactory), new DummyValueProviderFactory());

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterInstance<IPersonsRepository>(new PersonsRepository());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseWebApi(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            //http://localhost:9000/api/v3/token
            //Header ->
            //Content-Type - application/x-www-form-urlendoded

            //Body ->
            //grant_type - password
            //username - pikachumetal
            //password - secret

            //EN la llamada en Header ->
            //Authorization - Bearer {key}
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v3/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
