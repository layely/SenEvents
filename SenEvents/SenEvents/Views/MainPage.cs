using System;

using Xamarin.Forms;

namespace SenEvents
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage = null;
            Page listEventsPage = null; 

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new ItemsPage()
                    {
                        Title = "Browse"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };

                    listEventsPage = new ListEventsPage()
                    {
                        Title = "Explorer"
                    };
                    break;
            }

            //Children.Add(itemsPage);
            //Children.Add(aboutPage);
            Children.Add(listEventsPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
