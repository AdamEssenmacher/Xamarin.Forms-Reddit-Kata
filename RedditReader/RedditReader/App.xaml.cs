using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedditReader.Services;
using RedditReader.ViewModels;
using RedditReader.Views;
using Xamarin.Forms;

namespace RedditReader
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            SetupServices();

            var navigationPage = new NavigationPage(new ListPage());
            MainPage = navigationPage;
            NavigationPage = navigationPage;
        }

        protected static IServiceProvider? ServiceProvider { get; private set; }

        private static NavigationPage? NavigationPage { get; set; }

        public static Task PushNavigationAsync(Page page, bool isAnimated = true)
        {
            if (NavigationPage == null)
                throw new Exception($"{nameof(NavigationPage)} has not been initialized.");

            return NavigationPage.PushAsync(page, isAnimated);
        }

        public static TViewModel GetViewModel<TViewModel>()
        {
            if (ServiceProvider == null)
                throw new Exception($"{nameof(ServiceProvider)} has not been initialized.");

            var service = ServiceProvider.GetService<TViewModel>();
            if (service == null)
                throw new Exception("Could not resolve service.");

            return service;
        }

        private static void SetupServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<ListViewModel>();

            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<HttpClient>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}