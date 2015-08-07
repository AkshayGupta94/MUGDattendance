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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceMugd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class userDel : Page
    {
        private IMobileServiceTable<Users> Table = App.MobileService.GetTable<Users>();
        private MobileServiceCollection<Users, Users> items;
        private IMobileServiceTable<Colleges> Table2 = App.MobileService.GetTable<Colleges>();
        private MobileServiceCollection<Colleges,Colleges> items2; 
        public userDel()
        {
            this.InitializeComponent();
            List<string> type = new List<string>{
            "Admin","P.R. Manager","Core","Member"
            };
            Type.DataContext = type;
            onlaunch();
            
        }
        private async void onlaunch()
        {
            items2 = await Table2.ToCollectionAsync();
            List<string> names = new List<string>();
            foreach(Colleges c in  items2)
            {
                names.Add(c.collegeName);

            }
            Coll.DataContext = names;
        }
        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog m = new MessageDialog("");
            if (Namen.Text.Length == 0)
            {
                m.Title = "enter Name";
                m.ShowAsync();
            }
            else if (Email.Text.Length == 0)
            {
                m.Title = "enter Email";
                m.ShowAsync();
            }
           
            else if (Type.SelectedValue == null)
            {
                m.Title = "select type";
                m.ShowAsync();
            }
            else if (Coll.SelectedValue == null)
            {
                m.Title = "select College";
                m.ShowAsync();
            }
            else
            {
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items = await Table.Where(TableItem => TableItem.userName == Email.Text).ToCollectionAsync();
            if (items != null)
            {
                await App.MobileService.GetTable<Users>().DeleteAsync(items[0]);
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("User has been deleted succesfully");
                await msgbox.ShowAsync();
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("User not found");
                await msgbox.ShowAsync();
            }
        }
    }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(MainPage));
        }

    }
    
}
