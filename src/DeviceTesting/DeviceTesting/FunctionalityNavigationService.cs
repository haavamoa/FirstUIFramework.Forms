using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceTesting.Pages.Expander;
using DeviceTesting.Pages.FilledCheckbox;
using DeviceTesting.Pages.FloatingMultiButton;
using Xamarin.Forms;

namespace DeviceTesting
{
    public class FunctionalityNavigationService : IFunctionalityNavigationService
    {
        private readonly INavigation m_navigation;
        private IDictionary<string, Lazy<Page>> m_functionalityDictionary;

        public FunctionalityNavigationService(INavigation navigation)
        {
            m_navigation = navigation;
            m_functionalityDictionary = new Dictionary<string, Lazy<Page>>()
            {
                { "FilledCheckBox", new Lazy<Page>(() => new FilledCheckBoxPage()) },
                { "FloatingMultiButton", new Lazy<Page>(() => new FloatingMultiButtonPage()) },
                { "Expander", new Lazy<Page>(() => new ExpanderPage()) }
            };
        }

        private Page GetFunctionality(string functionality)
        {
            foreach (var functionalityKeyValue in m_functionalityDictionary.Where(
                functionalityKeyValue => functionalityKeyValue.Key.Equals(functionality)))
            {
                return functionalityKeyValue.Value.Value;
            }

            return new MainPage();
        }

        public async Task PushFunctionality(string functionality)
        {
            await m_navigation.PushAsync(GetFunctionality(functionality));
        }

        public IEnumerable<string> GetFunctionalities()
        {
            return m_functionalityDictionary.Select(keyValue => keyValue.Key);
        }
    }

    public interface IFunctionalityNavigationService
    {
        Task PushFunctionality(string functionality);
        IEnumerable<string> GetFunctionalities();
    }
}