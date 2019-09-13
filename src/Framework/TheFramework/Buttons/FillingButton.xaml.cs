using System.Windows.Input;
using TheFramework.ContentViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingButton : ContentView
    {
        public FillingButton()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FillingButton),
            null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(FillingButton),
            null);

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked),
            typeof(bool),
            typeof(FillingButton),
            false,
            propertyChanged: OnIsCheckedChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty ButtonHeightRequestProperty = BindableProperty.Create(
            nameof(ButtonHeightRequest),
            typeof(double),
            typeof(FillingButton),
            25.0,
            propertyChanged: OnAnySizeChanged);

        public double ButtonHeightRequest
        {
            get => (double)GetValue(ButtonHeightRequestProperty);
            set => SetValue(ButtonHeightRequestProperty, value);
        }

        public static readonly BindableProperty ButtonWidthRequestProperty = BindableProperty.Create(
            nameof(ButtonWidthRequest),
            typeof(double),
            typeof(FillingButton),
            25.0,
            propertyChanged: OnAnySizeChanged);

        public double ButtonWidthRequest
        {
            get => (double)GetValue(ButtonWidthRequestProperty);
            set => SetValue(ButtonWidthRequestProperty, value);
        }

        public static readonly BindableProperty ButtonCornerRadiusProperty = BindableProperty.Create(
            nameof(ButtonCornerRadius),
            typeof(CornerRadius),
            typeof(FillingButton),
            new CornerRadius(0),
            propertyChanged: OnAnySizeChanged);

        public CornerRadius ButtonCornerRadius
        {
            get => (CornerRadius)GetValue(ButtonCornerRadiusProperty);
            set => SetValue(ButtonCornerRadiusProperty, value);
        }

        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(
            nameof(AnimationSpeed),
            typeof(int),
            typeof(FillingButton),
            250);

        public int AnimationSpeed
        {
            get => (int)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }

        public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(
            nameof(ButtonColor),
            typeof(Color),
            typeof(FillingButton),
            Color.LightGray);

        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }

        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor),
            typeof(Color),
            typeof(FillingButton),
            Color.Gray);

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static readonly BindableProperty ButtonBorderColorProperty = BindableProperty.Create(
            nameof(ButtonBorderColor),
            typeof(Color),
            typeof(FillingButton),
            Color.Black);

        public Color ButtonBorderColor
        {
            get => (Color)GetValue(ButtonBorderColorProperty);
            set => SetValue(ButtonBorderColorProperty, value);
        }

        public static readonly BindableProperty ButtonBorderWidthProperty = BindableProperty.Create(
            nameof(ButtonBorderWidth),
            typeof(int),
            typeof(FillingButton),
            0,
            propertyChanged: OnAnySizeChanged);

        private static void OnAnySizeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FillingButton fillingButton)
            {
                fillingButton.AddBorder();
            }
        }

        private void AddBorder()
        {
            var scale = 1 - (0.03 * ButtonBorderWidth);
            if (outerCircle.Scale != scale)
            {
                outerCircle.Scale = scale;
                border.IsVisible = true;

            }
        }

        public int ButtonBorderWidth
        {
            get => (int)GetValue(ButtonBorderWidthProperty);
            set => SetValue(ButtonBorderWidthProperty, value);
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FillingButton fillingButton)
            {
                fillingButton.Animate();
            }
        }

        private async void Animate()
        {
            if (IsChecked)
            {
                await innerCircle.ScaleTo(1 - (0.03 * ButtonBorderWidth), (uint)AnimationSpeed);
            }
            else
            {
                await innerCircle.ScaleTo(0, (uint)AnimationSpeed);
            }
        }
    }
}