using System.Windows.Input;
using TheFramework.ContentViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilledCheckBox : ContentView
    {
        public FilledCheckBox()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FilledCheckBox),
            null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(FilledCheckBox),
            null);

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked),
            typeof(bool),
            typeof(FilledCheckBox),
            false,
            propertyChanged: OnIsCheckedChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(
            nameof(HeightRequest),
            typeof(double),
            typeof(FilledCheckBox),
            25.0,
            propertyChanged: OnAnySizeChanged);

        public double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        public static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(
            nameof(WidthRequest),
            typeof(double),
            typeof(FilledCheckBox),
            25.0,
            propertyChanged: OnAnySizeChanged);

        public double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(FilledCheckBox),
            new CornerRadius(0),
            propertyChanged: OnAnySizeChanged);

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(
            nameof(AnimationSpeed),
            typeof(int),
            typeof(FilledCheckBox),
            250);

        public int AnimationSpeed
        {
            get => (int)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }

        public static readonly BindableProperty UnFillColorProperty = BindableProperty.Create(
            nameof(UnFillColor),
            typeof(Color),
            typeof(FilledCheckBox),
            Color.LightGray);

        public Color UnFillColor
        {
            get => (Color)GetValue(UnFillColorProperty);
            set => SetValue(UnFillColorProperty, value);
        }

        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor),
            typeof(Color),
            typeof(FilledCheckBox),
            Color.Gray);

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(FilledCheckBox),
            Color.Black);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            nameof(BorderWidth),
            typeof(int),
            typeof(FilledCheckBox),
            0,
            propertyChanged: OnAnySizeChanged);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(FilledCheckBox), null, propertyChanged:OnAnySizeChanged);

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly BindableProperty ImageHeightRequestProperty = BindableProperty.Create(nameof(ImageHeightRequest), typeof(double), typeof(FilledCheckBox), 12.5, propertyChanged: OnAnySizeChanged);

        public double ImageHeightRequest
        {
            get => (double)GetValue(ImageHeightRequestProperty);
            set => SetValue(ImageHeightRequestProperty, value);
        }

        public static readonly BindableProperty ImageWidthRequestProperty = BindableProperty.Create(nameof(ImageWidthRequest), typeof(double), typeof(FilledCheckBox), 12.5, propertyChanged: OnAnySizeChanged);

        public double ImageWidthRequest
        {
            get => (double)GetValue(ImageWidthRequestProperty);
            set => SetValue(ImageWidthRequestProperty, value);
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FilledCheckBox fillingButton)
            {
                fillingButton.Animate();
            }
        }
        private static void OnAnySizeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FilledCheckBox fillingButton)
            {
                fillingButton.AddBorder();
                fillingButton.MoveImage();
            }
        }

        private void AddBorder()
        {
            var scale = 1 - (0.03 * BorderWidth);
            if (outerCircle.Scale != scale)
            {
                outerCircle.Scale = scale;
                border.IsVisible = true;
            }
        }

        private void MoveImage()
        {
            if (ImageSource == null) return;
            var newXValue = WidthRequest / 2 - ImageWidthRequest / 2;
            
            if (image.X != newXValue)
            {
                image.TranslationX = newXValue;
                image.IsVisible = true;
            }
            var newYValue = HeightRequest / 2 - ImageHeightRequest / 2;
            if (image.Y != newYValue)
            {
                image.TranslationY = newYValue;
            }
        }

        private async void Animate()
        {
            if (IsChecked)
            {
                await innerCircle.ScaleTo(1 - (0.03 * BorderWidth), (uint)AnimationSpeed);
            }
            else
            {
                await innerCircle.ScaleTo(0, (uint)AnimationSpeed);
            }
        }
    }
}