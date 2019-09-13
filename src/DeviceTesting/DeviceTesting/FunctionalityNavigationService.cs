using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DeviceTesting.Pages.FilledCheckbox;
using DeviceTesting.Pages.FloatingMultiButton;
using Xamarin.Forms;

namespace DeviceTesting
{
    public class FunctionalityNavigationService : IFunctionalityNavigationService
    {
        private readonly INavigation m_navigation;
        public FunctionalityNavigationService(INavigation navigation)
        {
            m_navigation = navigation;
        }

        private static Page GetFunctionality(string functionality)
        {
            switch (functionality)
            {
                case "FilledCheckBox":
                    return new FilledCheckBoxPage();
                case "FloatingMultiButton":
                    return new FloatingMultiButtonPage();
                default:
                    return new MainPage();
            }
        }   

        public async Task PushFunctionality(string functionality)
        {
            await m_navigation.PushAsync(GetFunctionality(functionality));
        }
    }

    public interface IFunctionalityNavigationService
    {
        Task PushFunctionality(string functionality);
    }
}
