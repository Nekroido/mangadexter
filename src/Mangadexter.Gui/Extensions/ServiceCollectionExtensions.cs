using Mangadexter.Gui.Helpers;
using Mangadexter.Gui.Repositories;
using Mangadexter.Gui.ViewModels;
using Mangadexter.Gui.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangadexter.Gui.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<SearchViewModel>();
        collection.AddTransient<MangaViewModel>();
        collection.AddTransient<ViewLocator>();
        collection.AddTransient<IMangaRepository>((_) => new MangadexRepository());
        collection.AddScoped<MainView>();
        collection.AddScoped<SearchView>();
        collection.AddScoped<MangaView>();
    }
}
