using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TheFramework.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingMultiButton : ContentView
    {
        private bool m_hasAnimatedUp;
        private const uint AnimationSpeed = 100;
        private readonly Easing m_animationStyle = Easing.CubicIn;
        private const int SpaceBetweenButtons = 55;

        public static readonly BindableProperty InteractiveButtonsProperty = BindableProperty.Create(
            nameof(InteractiveButtons),
            typeof(IList<Button>),
            typeof(FloatingMultiButton),
            new List<Button>(),
            propertyChanged: OnInteractiveButtonsChanged);

        public static readonly BindableProperty MainButtonHeightRequestProperty = BindableProperty.Create(
            nameof(MainButtonHeightRequest),
            typeof(double),
            typeof(FloatingMultiButton),
            70.0);

        public static readonly BindableProperty MainButtonBackgroundColorProperty = BindableProperty.Create(
            nameof(MainButtonBackgroundColor),
            typeof(Color),
            typeof(FloatingMultiButton),
            Color.Gray);

        public static readonly BindableProperty MainButtonBorderColorProperty = BindableProperty.Create(
            nameof(MainButtonBorderColor),
            typeof(Color),
            typeof(FloatingMultiButton),
            MainButtonBackgroundColorProperty.DefaultValue);

        public static readonly BindableProperty MainButtonBorderWidthProperty = BindableProperty.Create(
            nameof(MainButtonBorderWidth),
            typeof(double),
            typeof(FloatingMultiButton),
            0.0);

        public static readonly BindableProperty MainButtonWidthRequestProperty = BindableProperty.Create(
            nameof(MainButtonWidthRequest),
            typeof(double),
            typeof(FloatingMultiButton),
            70.0);

        public double MainButtonBorderWidth
        {
            get => (double)GetValue(MainButtonBorderWidthProperty);
            set => SetValue(MainButtonBorderWidthProperty, value);
        }

        public Color MainButtonBorderColor
        {
            get => (Color)GetValue(MainButtonBorderColorProperty);
            set => SetValue(MainButtonBorderColorProperty, value);
        }

        public Color MainButtonBackgroundColor
        {
            get => (Color)GetValue(MainButtonBackgroundColorProperty);
            set => SetValue(MainButtonBackgroundColorProperty, value);
        }

        public double MainButtonWidthRequest
        {
            get => (double)GetValue(MainButtonWidthRequestProperty);
            set => SetValue(MainButtonWidthRequestProperty, value);
        }

        public double MainButtonHeightRequest
        {
            get => (double)GetValue(MainButtonHeightRequestProperty);
            set => SetValue(MainButtonHeightRequestProperty, value);
        }

        private static void OnInteractiveButtonsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var floatingMultiButton = (FloatingMultiButton)bindable;

            //Clear the list and add interaction buttons first, then add main button at the end
            //This is to avoid showing the interactive buttons on top of the main button when they animate
            floatingMultiButton.NavigationButtonGrid.Children.Clear();
            foreach (var interactiveButton in floatingMultiButton.InteractiveButtons)
            {
                floatingMultiButton.NavigationButtonGrid.Children.Add(interactiveButton);
            }
            floatingMultiButton.NavigationButtonGrid.Children.Add(floatingMultiButton.MainButton);

            //Set height of the AbsoluteLayout in order to make sure that all buttons are not out of bounds
            floatingMultiButton.NavigationButtonGrid.HeightRequest += floatingMultiButton.MainButton.HeightRequest;
            foreach (var interactiveButton in floatingMultiButton.InteractiveButtons)
            {
                floatingMultiButton.NavigationButtonGrid.HeightRequest += interactiveButton.HeightRequest;
            }

            floatingMultiButton.NavigationButtonGrid.HeightRequest += SpaceBetweenButtons;

            //Move all buttons to the bottom of the page and make sure interactive buttons are placed in the middle of the main button
            floatingMultiButton.MainButton.TranslationY =
                floatingMultiButton.NavigationButtonGrid.HeightRequest - floatingMultiButton.MainButton.HeightRequest;
            foreach (var interactiveButton in floatingMultiButton.InteractiveButtons)
            {
                floatingMultiButton.TranslateToMiddleOfMainButton(interactiveButton);
            }
        }

        public IList<Button> InteractiveButtons
        {
            get => (IList<Button>)GetValue(InteractiveButtonsProperty);
            set => SetValue(InteractiveButtonsProperty, value);
        }

        public FloatingMultiButton()
        {
            InitializeComponent();
        }

        protected void ToggleShowingButtons(object sender, EventArgs e)
        {
            
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    if (!m_hasAnimatedUp)
                    {
                        foreach (var interactiveButton in InteractiveButtons)
                        {
                            var indexOfThisButton = InteractiveButtons.IndexOf(interactiveButton);

                            TranslateFromMiddleOfMainButton(interactiveButton, indexOfThisButton);
                            
                        }
                    }
                    else
                    {
                        foreach (var interactiveButton in InteractiveButtons)
                        {
                            TranslateToMiddleOfMainButton(interactiveButton);
                        }
                    }

                    m_hasAnimatedUp = !m_hasAnimatedUp;
                });
        }

        protected void TranslateToMiddleOfMainButton(VisualElement button)
        {

            button.TranslateTo(MainButton.TranslationX+MainButton.WidthRequest/2-button.WidthRequest/2, MainButton.TranslationY+MainButton.HeightRequest/2-button.HeightRequest/2, AnimationSpeed, m_animationStyle);
        }

        protected void TranslateFromMiddleOfMainButton(VisualElement button, int spaceMultiplier)
        {
            button.TranslateTo(
                MainButton.WidthRequest / 2 - button.WidthRequest / 2,
                MainButton.TranslationY - MainButton.HeightRequest - SpaceBetweenButtons * spaceMultiplier,
                AnimationSpeed,
                m_animationStyle);
        }
    }
}