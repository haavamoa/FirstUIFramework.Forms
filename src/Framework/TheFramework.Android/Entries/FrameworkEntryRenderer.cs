using Android.Content;
using Android.Graphics;
using TheFramework.Android.Entries;
using TheFramework.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(FrameworkEntry), typeof(FrameworkEntryRenderer))]

namespace TheFramework.Android.Entries
{
    public class FrameworkEntryRenderer : EntryRenderer
    {
        private bool m_wasInitialized = false;
        private FrameworkEntry m_frameworkEntry;
        public FrameworkEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //When the element is drawn
            if (e.NewElement != null)
            {
                m_frameworkEntry = e.NewElement as FrameworkEntry;
                if (!m_wasInitialized)
                {
                    Control.Background = null;
                    m_wasInitialized = true;
                }

                SubscribeToEvents();
            }

            //When the element is destroyed
            if (e.OldElement != null)
            {
                UnsubscribeToEvents();
            }
        }

        private void UnsubscribeToEvents()
        {
            Control.FocusChange -= OnFocusCHanged;
        }

        private void OnFocusCHanged(object sender, FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                if (m_frameworkEntry.HasShadow)
                {
                    Control.SetShadowLayer(10, 10, 10, m_frameworkEntry.ShadowColor.ToAndroid());
                }
            }
            else
            {
                if (m_frameworkEntry.HasShadow)
                {
                    Control.SetShadowLayer(10, 10, 10, Color.Transparent);
                }
            }
        }

        private void SubscribeToEvents()
        {
            Control.FocusChange += OnFocusCHanged;
        }
    }
}