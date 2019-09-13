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
        private double m_heightRequest = 140;
        private double m_widthRequest = 140;
        private double m_cornerRadius = 70;
        private string m_fillColor = "#98B2AE";
        private string m_unFillColor = "LightGray";

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

        public double HeightRequest
        {
            get => m_heightRequest;
            set
            {
                m_heightRequest = value;
                OnPropertyChanged();
            }
        }

        public double WidthRequest
        {
            get => m_widthRequest;
            set
            {
                m_widthRequest = value; 
                OnPropertyChanged();
            }
        }

        public double CornerRadius
        {
            get => m_cornerRadius;
            set
            {
                m_cornerRadius = value; 
                OnPropertyChanged();
            }
        }

        public string FillColor
        {
            get => m_fillColor;
            set
            {
                m_fillColor = value; 
                OnPropertyChanged();
            }
        }

        public string UnFillColor
        {
            get => m_unFillColor;
            set
            {
                m_unFillColor = value; 
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