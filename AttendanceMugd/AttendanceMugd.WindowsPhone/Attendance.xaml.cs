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
    public sealed partial class Attendance : Page
    {
        private IMobileServiceTable<Student> Table = App.MobileService.GetTable<Student>();
        private MobileServiceCollection<Student, Student> items;
        public Attendance()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items = await Table
                     .Where(Student => Student.Roll_no == Box.Text)
                      .ToCollectionAsync();
            Student a = new Student();
            a = items[0];
            a.Attendance++;
            a.LastAttended = DateTime.Today;
            await Table.UpdateAsync(a);
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            string mess = "Attndance of {0} has been updates succesfully";
            string.Format(mess, a.FullName);
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MessageDialog msgbox = new MessageDialog(mess);
            await msgbox.ShowAsync();
        }
    }
}
