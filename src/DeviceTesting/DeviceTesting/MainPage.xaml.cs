using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DeviceTesting.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;

namespace DeviceTesting
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            TheFramework.TheFramework.Init();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            NavigateCommand = ((MainViewModel)BindingContext).NavigateToCommand;
        }

        public void OnStart()
        {
            ((MainViewModel)BindingContext).Initialize(new FunctionalityNavigationService(App.Current.MainPage.Navigation));
        }

        public ICommand NavigateCommand { get; }
    }
}