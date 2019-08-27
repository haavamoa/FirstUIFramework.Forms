using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[ContentProperty("InteractiveButtons")]
    public partial class FloatingMultiButton : ContentView
    {
        private bool m_hasAnimatedUp;


        public static readonly BindableProperty InteractiveButtonsProperty = BindableProperty.Create(nameof(InteractiveButtons), typeof(IEnumerable<Button>), typeof(FloatingMultiButton), new List<Button>(), propertyChanged:OnInteractiveButtonsChanged);

        public static readonly BindableProperty MainButtonProperty = BindableProperty.Create(nameof(MainButton), typeof(Button), typeof(FloatingMultiButton), s_defaultMainButton, propertyChanged:PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var mainButton = (Button)newvalue;
            SetPlacementForMainButton(mainButton);
            mainButton.Clicked += ((FloatingMultiButton)bindable).ToggleShowingButtons;
        }

        private static void SetPlacementForMainButton(Button mainButton)
        {
            mainButton.SetValue(Grid.ColumnProperty, 0);
            mainButton.SetValue(Grid.ColumnSpanProperty, 3);
            mainButton.SetValue(Grid.RowSpanProperty, 3);
        }

        private static readonly Button s_defaultMainButton = new Button()
        {
            HeightRequest = 70,
            WidthRequest = 70,
            CornerRadius = 35,
        };

        public Button MainButton
        {
            get => (Button)GetValue(MainButtonProperty);
            set => SetValue(MainButtonProperty, value);
        }
        private static void OnInteractiveButtonsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var floatingMultiButton = (FloatingMultiButton)bindable;
        }

        public IEnumerable<Button> InteractiveButtons
        {
            get => (IEnumerable<Button>)GetValue(InteractiveButtonsProperty);
            set => SetValue(InteractiveButtonsProperty, value);
        }
        public FloatingMultiButton()
        {
            InitializeComponent();

            SetPlacementForMainButton(s_defaultMainButton);

            //Set Height request
            NavigationButtonGrid.HeightRequest = MainButton.HeightRequest + FirstButton.HeightRequest + SecondButton.HeightRequest +
                                                 ThirdButton.HeightRequest;
            MainButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            FirstButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            SecondButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
            ThirdButton.TranslationY = NavigationButtonGrid.HeightRequest - MainButton.HeightRequest;
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
                        FirstButton.TranslateTo(FirstButton.X, (MainButton.TranslationY - MainButton.HeightRequest), speed, style);
                        SecondButton.TranslateTo(SecondButton.X, (MainButton.TranslationY - MainButton.HeightRequest) - spacebetweenbuttons, speed, style);
                        ThirdButton.TranslateTo(ThirdButton.X, (MainButton.TranslationY - MainButton.HeightRequest) - spacebetweenbuttons * 2, speed, style);
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