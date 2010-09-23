using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace TaskNotes.Infrastructure
{
    public class NinjectMvcServiceLocator : IMvcServiceLocator
    {
        private readonly IKernel _kernel;

        public NinjectMvcServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _kernel.GetAll<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _kernel.Get<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _kernel.Get<TService>(key);
        }

        public object GetInstance(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _kernel.Get(serviceType, key);
        }

        public void Release(object instance)
        {
            _kernel.Release(instance);
        }
    }
}