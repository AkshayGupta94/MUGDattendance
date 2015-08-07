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
    public sealed partial class searchUser : Page
    {
        private IMobileServiceTable<Users> Table = App.MobileService.GetTable<Users>();
        private MobileServiceCollection<Users, Users> items;
      

        public searchUser()
        {
            this.InitializeComponent();
            
        }
       
        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog m = new MessageDialog("");
            if (Namen.Text.Length == 0)
            {
                m.Title = "enter Name";
                m.ShowAsync();
            }
           
            else
            {
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
                items = await Table.Where(TableItem => TableItem.name == Namen.Text).ToCollectionAsync();
                if (items.Count != 0)
                {
                    Users lol = new Users();
                    lol.college = items[0].college;
                    lol.Id = items[0].Id;
                    lol.name = items[0].name;
                    lol.password = items[0].password;
                    lol.type = items[0].type;
                    lol.userName = items[0].userName;
                    myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    Frame.Navigate(typeof(userDetail), lol);
                }
                else
                {
                    myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
