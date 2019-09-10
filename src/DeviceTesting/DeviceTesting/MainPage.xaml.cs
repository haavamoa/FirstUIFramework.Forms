using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeviceTesting.Annotations;
using Xamarin.Forms;

namespace DeviceTesting
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool m_hasAnimatedUp;
        private bool m_isChecked;

        public MainPage()
        {
            TheFramework.TheFramework.Init();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            uint speed = 100;
            var style = Easing.CubicIn;
            var spacebetweenbuttons = 55;
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    if (!m_hasAnimatedUp)
                    {
                        FirstButton.TranslateTo(FirstButton.X, -(NavigationButton.Y + NavigationButton.HeightRequest), speed, style);
                        SecondButton.TranslateTo(
                            SecondButton.X,
                            -(NavigationButton.Y + NavigationButton.HeightRequest) - spacebetweenbuttons,
                            speed,
                            style);
                        ThirdButton.TranslateTo(
                            ThirdButton.X,
                            -(NavigationButton.Y + NavigationButton.HeightRequest) - spacebetweenbuttons * 2,
                            speed,
                            style);
                    }
                    else
                    {
                        ThirdButton.TranslateTo(ThirdButton.X, 35 / 2, speed, style);
                        SecondButton.TranslateTo(SecondButton.X, 35 / 2, speed, style);
                        FirstButton.TranslateTo(FirstButton.X, 35 / 2, speed, style);
                    }

                    m_hasAnimatedUp = !m_hasAnimatedUp;
                });
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    switch (e.StatusType)
                    {
                        case GestureStatus.Running:
                            Label.Text = e.TotalY.ToString();
                            break;
                    }
                });
        }
    }

    public class MyTestViewModel : INotifyPropertyChanged
    {
        private bool m_isChecked;

        public MyTestViewModel()
        {
            FillingButtonCommand = new Command(() => { IsChecked = !IsChecked; });
        }

        public ICommand FillingButtonCommand { get; }

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
    }
}