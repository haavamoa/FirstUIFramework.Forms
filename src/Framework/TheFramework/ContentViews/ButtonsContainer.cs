using System.Collections.Generic;
using Xamarin.Forms;

namespace TheFramework.ContentViews
{
    public class ButtonsContainer : ContentView
    {
        public static readonly BindableProperty ButtonsProperty = BindableProperty.Create(
            nameof(Buttons),
            typeof(List<Button>),
            typeof(ButtonsContainer),
            new List<Button>());

        private bool m_buttonsAreVisible;

        public List<Button> Buttons
        {
            get => (List<Button>)GetValue(ButtonsProperty);
            set => SetValue(ButtonsProperty, value);
        }

        public void ShowButtons()
        {
            var container = new AbsoluteLayout();
            foreach (var button in Buttons)
            {
                container.Children.Add(button);
            }

            Content = container;
        }

        public void HideButtons()
        {
            Content = null;
        }

        public void ToggleButtons()
        {
            if (m_buttonsAreVisible)
            {
                HideButtons();
            }
            else
            {
                ShowButtons();
            }

            m_buttonsAreVisible = !m_buttonsAreVisible;
        }
    }
}