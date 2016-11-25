using RefugeeHousing.ApiAccess;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.Translations;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RefugeeHousing.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RefugeeHousing.App_Start.NinjectWebCommon), "Stop")]

namespace RefugeeHousing.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InTransientScope();
            kernel.Bind<ITranslationService>().To<TranslationService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IPropertyListingService>().To<PropertyListingService>().InSingletonScope();
            kernel.Bind<ILocationRepository>().To<LocationRepository>().InSingletonScope();
            kernel.Bind<IPlaceLookUpService>().To<PlaceLookUpService>().InSingletonScope();
            kernel.Bind<IPropertyEmailService>().To<PropertyEmailService>().InSingletonScope();
            kernel.Bind<IPropertyContactService>().To<PropertyContactService>().InSingletonScope();
            kernel.Bind<IEmailBuilder>().To<EmailBuilder>().InSingletonScope();
        }        
    }
}
