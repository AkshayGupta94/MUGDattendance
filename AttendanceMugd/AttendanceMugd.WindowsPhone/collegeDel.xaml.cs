using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AttendanceMugd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class collegeDel : Page
    {
        private IMobileServiceTable<Colleges> Table2 = App.MobileService.GetTable<Colleges>();
        private MobileServiceCollection<Colleges, Colleges> items2;
        public collegeDel()
        {
            this.InitializeComponent();
        }
        private async void submit_Click(object sender, RoutedEventArgs e)
        {

            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items2 = await Table2.Where(Table2item => Table2item.collegeName == Namen.Text).ToCollectionAsync();
            if (items2 != null)
            {
                await App.MobileService.GetTable<Colleges>().DeleteAsync(items2[0]);
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("College has been deleted succesfully");
                await msgbox.ShowAsync();
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("College not found");
                await msgbox.ShowAsync();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
