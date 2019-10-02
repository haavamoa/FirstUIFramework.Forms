using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheFramework.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Expander
    {
        public Expander()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(nameof(MainContent), typeof(View), typeof(Expander));

        public View MainContent
        {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        public static readonly BindableProperty ExpandedContentProperty = BindableProperty.Create(nameof(ExpandedContent), typeof(View), typeof(Expander));


        public View ExpandedContent
        {
            get => (View)GetValue(ExpandedContentProperty);
            set => SetValue(ExpandedContentProperty, value);
        }

        public static readonly BindableProperty FinishedCommandProperty = BindableProperty.Create(nameof(FinishedCommand), typeof(ICommand), typeof(Expander), null);

        public ICommand FinishedCommand
        {
            get => (ICommand)GetValue(FinishedCommandProperty);
            set => SetValue(FinishedCommandProperty, value);
        }

        public static readonly BindableProperty FinishedCommandParameterProperty = BindableProperty.Create(nameof(FinishedCommandParameter), typeof(object), typeof(Expander), null);

        public object FinishedCommandParameter
        {
            get => (object)GetValue(FinishedCommandParameterProperty);
            set => SetValue(FinishedCommandParameterProperty, value);
        }

        public static readonly BindableProperty RateProperty = BindableProperty.Create(nameof(Rate), typeof(int), typeof(Expander), 16);

        public int Rate
        {
            get => (int)GetValue(RateProperty);
            set => SetValue(RateProperty, value);
        }

        public static readonly BindableProperty SpeedProperty = BindableProperty.Create(nameof(Speed), typeof(int), typeof(Expander), 250);
        private bool m_isExpanded;
        private double m_fullHeight;
        private bool m_hasSetInitialFullHeight;

        public int Speed
        {
            get => (int)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (height > 0 && !m_hasSetInitialFullHeight)
            {
                m_hasSetInitialFullHeight = true;
                m_fullHeight = height;
                expandedView.HeightRequest = 0;
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            var expandedHeight = m_fullHeight - mainView.Content.Height;
            var start = m_isExpanded ? expandedHeight : 0;
            var end = m_isExpanded ? 0 : expandedHeight;

            expandedView.Animate("expandedView", d => expandedView.HeightRequest = d, start, end, (uint)Rate, (uint)Speed, null, (d, b) => FinishedCommand?.Execute(FinishedCommandParameter));
            m_isExpanded = !m_isExpanded;
        }
    }
}