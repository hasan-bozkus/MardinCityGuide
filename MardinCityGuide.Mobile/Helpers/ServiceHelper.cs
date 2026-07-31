using System;
using System.Collections.Generic;
using System.Text;

namespace MardinCityGuide.Mobile.Helpers
{
    public class ServiceHelper
    {
        public static IServiceProvider Services { get; private set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        public static TService GetService<TService>() => Services.GetRequiredService<TService>();
    }
}
