using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceMugd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private IMobileServiceTable<Users> Table = App.MobileService.GetTable<Users>();
        private MobileServiceCollection<Users, Users> items;
        private IMobileServiceTable<Colleges> Table2 = App.MobileService.GetTable<Colleges>();
        private MobileServiceCollection<Colleges, Colleges> items2; 
        public Register()
        {
            this.InitializeComponent();
            onlaunch();
        }

        private async void onlaunch()
        {
            items2 = await Table2.ToCollectionAsync();
            List<string> names = new List<string>();
            foreach (Colleges c in items2)
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
                await m.ShowAsync();
            }
            else if (Email.Text.Length == 0)
            {
                m.Title = "enter Email";
                await m.ShowAsync();
            }
            else if (Mobile.Text.Length == 0)
            {
                m.Title = "enter Mobile";
                await m.ShowAsync();
            }
            else if (Roll.Text.Length == 0)
            {
                m.Title = "enter Roll No.";
                await m.ShowAsync();
            }
            else if (Coll.SelectedValue == null)
            {
                m.Title = "select College";
                await m.ShowAsync();
            }
            else
            {
                Student item = new Student
                {
                    Roll_no = Roll.Text,
                    Attendance = 1,
                    IsMember = false,
                    Consecutive = 1,
                    EmailId = Email.Text,
                    FullName = Namen.Text,
                    Mobile_no = Mobile.Text,
                    LastAttended = DateTime.Today,
                    college = Coll.SelectedItem as string,
                    dateLastUpdated = int.Parse(DateTime.Now.Day.ToString()),
                    monthLastUpdated = int.Parse(DateTime.Now.Month.ToString())

                };
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
                await App.MobileService.GetTable<Student>().InsertAsync(item);
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("User has been added succesfully");
                await msgbox.ShowAsync();
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
