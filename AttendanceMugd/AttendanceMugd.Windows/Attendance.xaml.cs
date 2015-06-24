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
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceMugd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Attendance : Page

    { 
        private IMobileServiceTable<Student> Table = App.MobileService.GetTable<Student>();  
        private MobileServiceCollection<Student, Student> items; 
        public Attendance()
        {
            this.InitializeComponent();


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items = await Table
                     .Where(Student => Student.Roll_no == Email.Text)
                      .ToCollectionAsync();
            Student a = new Student();
            a = items[0];
            if (a.dateLastUpdated == int.Parse(DateTime.Now.Day.ToString()))
            {
                string mess = "Attndance of " + a.FullName + " has been already updated today";
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog(mess);
                await msgbox.ShowAsync();
            }
            else
            {
                a.Attendance++;
                a.LastAttended = DateTime.Today;
                a.dateLastUpdated = int.Parse(DateTime.Now.Day.ToString());
                await Table.UpdateAsync(a);
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                string mess2 = "Attndance of " + a.FullName + " has been updated succesfully :)";
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox2 = new MessageDialog(mess2);
                await msgbox2.ShowAsync();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
