using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeviceTesting.Annotations;
using DeviceTesting.Pages.FilledCheckbox;
using Xamarin.Forms;

namespace DeviceTesting
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IFunctionalityNavigationService m_functionalityNavigationService;
        private ObservableCollection<string> m_functionality;

        public MainViewModel()
        {
            NavigateToCommand = new Command(NavigateTo);
        }

        public void Initialize(IFunctionalityNavigationService functionalityNavigationService)
        {
            m_functionalityNavigationService = functionalityNavigationService;
            var functionalities = m_functionalityNavigationService.GetFunctionalities();
            Functionality = new ObservableCollection<string>(functionalities);
        }

        public ICommand NavigateToCommand { get; }

        private async void NavigateTo(object functionality)
        {
            if (functionality == null) return;
            await m_functionalityNavigationService.PushFunctionality(functionality.ToString());
        }

        public ObservableCollection<string> Functionality
        {
            get => m_functionality;
            set
            {
                m_functionality = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}