using System;

namespace Web.Components.Session
{
    public static class SessionProviderExtension
    {
        public static T Get<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T).BaseType);
        }
    }
}