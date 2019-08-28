using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[ContentProperty("InteractiveButtons")]
    public partial class FloatingMultiButton : ContentView
    {
        private bool m_hasAnimatedUp;

        public static readonly BindableProperty InteractiveButtonsProperty = BindableProperty.Create(
            nameof(InteractiveButtons),
            typeof(IEnumerable<Button>),
            typeof(FloatingMultiButton),
            new List<Button>(),
            propertyChanged: OnInteractiveButtonsChanged);

        public static readonly BindableProperty MainButtonHeightRequestProperty = BindableProperty.Create(
            nameof(MainButtonHeightRequest),
            typeof(double),
            typeof(FloatingMultiButton),
            70.0,
            propertyChanged: OnMainButtonHeightRequestChanged);

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

        private static void OnMainButtonHeightRequestChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var floatingMultiButton = (FloatingMultiButton)bindable;
            floatingMultiButton.NavigationButtonGrid.HeightRequest += (double)newvalue;
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

        private static void MainButtonChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var floatingMultiButton = (FloatingMultiButton)bindable;
            var mainButton = (Button)newvalue;
            SetPlacementForMainButton(mainButton);
            floatingMultiButton.NavigationButtonGrid.HeightRequest += mainButton.HeightRequest;
            //MainButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            mainButton.Clicked += floatingMultiButton.ToggleShowingButtons;
        }

        private static void SetPlacementForMainButton(Button mainButton)
        {
            mainButton.SetValue(Grid.ColumnProperty, 0);
            mainButton.SetValue(Grid.ColumnSpanProperty, 3);
            mainButton.SetValue(Grid.RowSpanProperty, 3);
        }

        private static readonly Button s_defaultMainButton = new Button() { HeightRequest = 70, WidthRequest = 70, CornerRadius = 35, };

        private static void OnInteractiveButtonsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var floatingMultiButton = (FloatingMultiButton)bindable;
            //FirstButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            //SecondButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            //ThirdButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            foreach (var interactiveButton in floatingMultiButton.InteractiveButtons)
            {
                floatingMultiButton.NavigationButtonGrid.HeightRequest += interactiveButton.HeightRequest;
            }
        }

        public IEnumerable<Button> InteractiveButtons
        {
            get => (IEnumerable<Button>)GetValue(InteractiveButtonsProperty);
            set => SetValue(InteractiveButtonsProperty, value);
        }

        public FloatingMultiButton()
        {
            InitializeComponent();
        }

        protected void ToggleShowingButtons(object sender, EventArgs e)
        {
            uint speed = 100;
            var style = Easing.CubicIn;
            var spacebetweenbuttons = 55;
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    if (!m_hasAnimatedUp)
                    {
                        FirstButton.TranslateTo(FirstButton.X, MainButton.TranslationY - MainButton.HeightRequest, speed, style);
                        SecondButton.TranslateTo(
                            SecondButton.X,
                            MainButton.TranslationY - MainButton.HeightRequest - spacebetweenbuttons,
                            speed,
                            style);
                        ThirdButton.TranslateTo(
                            ThirdButton.X,
                            MainButton.TranslationY - MainButton.HeightRequest - spacebetweenbuttons * 2,
                            speed,
                            style);
                    }
                    else
                    {
                        ThirdButton.TranslateTo(ThirdButton.X, MainButton.TranslationY, speed, style);
                        SecondButton.TranslateTo(SecondButton.X, MainButton.TranslationY, speed, style);
                        FirstButton.TranslateTo(FirstButton.X, MainButton.TranslationY, speed, style);
                    }

                    m_hasAnimatedUp = !m_hasAnimatedUp;
                });
        }
    }
}