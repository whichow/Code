using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ServiceSystem
    {
        private Dictionary<Type, IService> services;

        public void RegisterService<T>(IService service) where T : IService
        {
            services.Add(typeof(T), service);
        }

        public T GetService<T>() where T : IService
        {
            IService service;
            services.TryGetValue(typeof(T), out service);
            if(service != null)
            {
                return (T)service;
            }
            return default(T);
        }

        public IService GetService(Type type)
        {
            IService service;
            services.TryGetValue(type, out service);
            if (service != null)
            {
                return service;
            }
            return null;
        }
    }
}
