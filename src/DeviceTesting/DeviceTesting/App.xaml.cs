using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeviceTesting
{
    public partial class App : Application
    {
        private readonly MainPage m_mainpage;

        public App()
        {
            InitializeComponent();

            m_mainpage = new MainPage();
            //TheFramework.TheFramework.Init();
            MainPage = new NavigationPage(m_mainpage);
        }

        protected override void OnStart()
        {
            m_mainpage.OnStart();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
