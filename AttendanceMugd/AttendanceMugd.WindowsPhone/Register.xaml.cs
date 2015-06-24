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
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack == true)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(MainPage));
        }

        private async void submit_Click(object sender, RoutedEventArgs e)
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
                college = "BVCOE",
                dateLastUpdated = int.Parse(DateTime.Today.Date.ToString())
            };
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            await App.MobileService.GetTable<Student>().InsertAsync(item);
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MessageDialog msgbox = new MessageDialog("User has been added succesfully");
            await msgbox.ShowAsync();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
