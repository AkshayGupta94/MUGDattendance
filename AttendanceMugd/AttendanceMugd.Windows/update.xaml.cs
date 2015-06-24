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
    public sealed partial class update : Page
    {
        private IMobileServiceTable<Student> Table = App.MobileService.GetTable<Student>();
        private MobileServiceCollection<Student, Student> items;
        private IMobileServiceTable<Colleges> Table2 = App.MobileService.GetTable<Colleges>();
        private MobileServiceCollection<Colleges, Colleges> items2; 
        public update()
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
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items = await Table
                  .Where(Student => Student.IsMember == false && Student.college == Coll.SelectedItem as string)
                  .OrderBy(Student => Student.dateLastUpdated)
                  .ToCollectionAsync();
            if (items[0].dateLastUpdated == int.Parse(DateTime.Now.Day.ToString()))
            {
                MessageDialog msgbox = new MessageDialog("Database has already been updated today");
                await msgbox.ShowAsync();
            }
            if (items[items.Count-1].dateLastUpdated != int.Parse(DateTime.Now.Day.ToString()))
            {
                MessageDialog msgbox = new MessageDialog("No attendance for today has been recorded!!");
                await msgbox.ShowAsync();
            }  
            else
            {
                foreach (Student a in items)
                {
                    if (a.LastAttended == DateTime.Today)
                    {
                        a.Consecutive++;
                        if (a.Consecutive == 3)
                        {
                            a.IsMember = true;
                            Users item = new Users
                            {
                                name = a.FullName,
                                userName = a.EmailId,
                                password = a.Mobile_no,
                                type = "Member",
                                college = Coll.SelectedItem as string
                            };
                            await App.MobileService.GetTable<Users>().InsertAsync(item);
                        }

                    }
                    else
                    {
                        a.Consecutive = 0;
                    }
                    a.dateLastUpdated = int.Parse(DateTime.Now.Day.ToString());
                    await Table.UpdateAsync(a);
                   

                }
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("Database has been updated succesfully");
                await msgbox.ShowAsync();
            }
        }

    }
}
