using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeviceTesting.Annotations;
using Xamarin.Forms;

namespace DeviceTesting.Pages.FilledCheckbox
{
    public class FilledCheckBoxViewModel : INotifyPropertyChanged   
    {
        private bool m_isChecked;

        public FilledCheckBoxViewModel()
        {
            Command = new Command(() => IsChecked = !IsChecked);    
        }

        public ICommand Command { get; }

        public bool IsChecked
        {
            get => m_isChecked;
            set
            {
                m_isChecked = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } }