using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeviceTesting.Pages.FilledCheckbox;
using Xamarin.Forms;

namespace DeviceTesting
{
    public class MainViewModel
    {
        private IFunctionalityNavigationService m_functionalityNavigationService;
        public MainViewModel()
        {
            NavigateToCommand = new Command(NavigateTo);
            Functionality = new ObservableCollection<string>() { "FilledCheckBox" };
        }

        public void Initialize(IFunctionalityNavigationService functionalityNavigationService)
        {
            m_functionalityNavigationService = functionalityNavigationService;
        }

        public ICommand NavigateToCommand { get; }

        private async void NavigateTo(object functionality)
        {
            if (functionality == null) return;
            await m_functionalityNavigationService.PushFunctionality(functionality.ToString());
        }

        public ObservableCollection<string> Functionality { get; }
    }
}