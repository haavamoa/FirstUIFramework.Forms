using System;
using TheFramework.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace TheFramework.iOS.Entries
{
    public class FrameworkEntryRenderer : EntryRenderer
    {
        private bool m_wasInitialized;
        private FrameworkEntry m_frameworkEntry;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

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
        }

        private void SubscribeToEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}