using System;
using System.Collections.Generic;
using System.Text;
using DeviceTesting.ViewModels;
using LightInject;

namespace DeviceTesting
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IMainViewModel,MainViewModel>();
        }
    }
}
