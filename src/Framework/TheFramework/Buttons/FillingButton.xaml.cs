using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FillingButton), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FillingButton), null);

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(FillingButton), false, propertyChanged:OnIsCheckedChanged);

        private static void OnIsCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FillingButton fillingButton)
            {
                fillingButton.Animate();
            }
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(FillingButton), 25.0);

        public double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        public static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(FillingButton), 25.0);

        public double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(FillingButton), new CornerRadius(0));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(nameof(AnimationSpeed), typeof(int), typeof(FillingButton), 250);

        public int AnimationSpeed
        {
            get => (int)GetValue(AnimationSpeedProperty);
            set => SetValue(AnimationSpeedProperty, value);
        }
        private async void Animate()
        {
            if (IsChecked)
            {
                await innerCircle.ScaleTo(1, length:(uint)AnimationSpeed);
            }
            else
            {
                await innerCircle.ScaleTo(0, length:(uint)AnimationSpeed);
            }
        }
    }
}