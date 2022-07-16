using FunTranslator2.Services;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace FunTranslator2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<HttpClient>(
                            new InjectionFactory(x =>
                                new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]) }
                            )
                        );
            container.RegisterType<IFunTranslatorService, FunTranslatorService>();
            //container.RegisterType<FunTranslatorService>(new InjectionConstructor(ConfigurationManager.AppSettings["ApiUrl"]));
            //container.RegisterType<string>(ConfigurationManager.AppSettings["ApiUrl"]);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}