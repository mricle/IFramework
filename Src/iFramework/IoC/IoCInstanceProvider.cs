﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;

namespace IFramework.IoC
{
    public class IocInstanceProvider : IInstanceProvider
    {
        Type _serviceType;
        IContainer _container;

        public IocInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
            _container = IoCFactory.Instance.CurrentContainer;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return _container.Resolve(_serviceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable)
                ((IDisposable)instance).Dispose();
        }

        #endregion
    }
}
