using System;
using System.Collections.Generic;
using System.Text;
using DeviceTesting.ViewModels;
using LightInject;

namespace DeviceTesting
{
    public class ViewModelLocator
    {
        private ServiceContainer m_container;

        public void Initialize(ServiceContainer container) => m_container = container;

        public IMainViewModel MainViewModel => m_container.GetInstance<IMainViewModel>();
    }
}
