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
    public sealed partial class MainPage : Page
    {
        private IMobileServiceTable<Student> Table = App.MobileService.GetTable<Student>();
        private MobileServiceCollection<Student, Student> items; 
        public MainPage()
        {
            this.InitializeComponent();
            onlaunchadmin();
            onlaunchcore();
            onlaunchsettings();
        }

        private void onlaunchsettings()
        {
            List<datamodel> myList = new List<datamodel>();
            datamodel temp = new datamodel();
            temp.back = "Green";
            temp.src = "/Assets/Plus.png";
            temp.title = "Add new member";
            temp.desc = "Use this option to add new members to the group, This is similar to registeration and will not generate any user id and passwords";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Green";
            temp.src = "/Assets/register.png";
            temp.title = "Mark Attendance";
            temp.desc = "Use this option to mark attendance of the members present at the event";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Green";
            temp.src = "/Assets/search.png";
            temp.title = "Search For members";
            temp.desc = "Use this option to find members and see their details";
            myList.Add(temp);
            settings.DataContext = myList;
           
        }

        private void onlaunchcore()
        {
            List<datamodel> myList = new List<datamodel>();
            datamodel temp = new datamodel();
            temp.back = "Red";
            temp.name = "Register";
            temp.src = "/Assets/Plus.png";
            temp.title = "Add new member";
            temp.desc = "Use this option to add new members to the group, This is similar to registeration and will not generate any user id and passwords";
            myList.Add(temp);
            temp = new datamodel();

            temp.name = "Attendance";
            temp.back = "Red";
            temp.src = "/Assets/register.png";
            temp.title = "Mark Attendance";
            temp.desc = "Use this option to mark attendance of the members present at the event";
            myList.Add(temp);
            temp = new datamodel();

            temp.name = "Search";
            temp.back = "Red";
            temp.src = "/Assets/search.png";
            temp.title = "Search For members";
            temp.desc = "Use this option to find members and see their details";
            myList.Add(temp);
            temp = new datamodel();

            temp.name = "Update";
            temp.back = "Red";
            temp.src = "/Assets/update.png";
            temp.title = "Update the database";
            temp.desc = "Use this option to update members database";
            myList.Add(temp);
            core.DataContext = myList;
            settings.DataContext = myList;
        }

        private void onlaunchadmin()
        {
            List<datamodel> myList = new List<datamodel>();
            datamodel temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/Plus.png";
            temp.title = "Add new user";
            temp.desc = "Use this option to add new user to the database of users. Users are either core members or admins or publicity and have different priviledges accordingly";
            myList.Add(temp);
            temp.back = "Blue";
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/minus.png";
            temp.title = "Delete user";
            temp.desc = "Use this option to delete user. Once deleted the user will losse all access to this app";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/search.png";
            temp.title = "Search For users";
            temp.desc = "Use this option to find users and see their details";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/Plus.png";
            temp.title = "Add College";
            temp.desc = "Use this option to Add Colleges";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/minus.png";
            temp.title = "Remove College";
            temp.desc = "Use this option to remove college";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/minus.png";
            temp.title = "Add event";
            temp.desc = "Use this option to remove college";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/minus.png";
            temp.title = "Add notice";
            temp.desc = "Use this option to remove college";
            myList.Add(temp);
            admin.DataContext = myList;

        }
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myProgressBar.IsIndeterminate = true;
            items = await Table
                  .Where(Student => Student.IsMember == false)
                   .ToCollectionAsync();
            foreach (Student a in items)
            { 
               if(a.LastAttended==DateTime.Today)
               {
                   a.Consecutive++;
                   if(a.Consecutive==3)
                   {
                       a.IsMember = true;
                   }

               }
               else
               {
                   a.Consecutive = 0;
               }
               await Table.UpdateAsync(a);
               myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
               MessageDialog msgbox = new MessageDialog("Database has been updated succesfully");
               await msgbox.ShowAsync();

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (PasswordBpx.Password == "WeWantToParty")
            //{
            //    Login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //    Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //}

        }

        private void myTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //if (myTextBox.Password == "WeWantToParty")
            //{
            //    Login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //    Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void admin_ItemClick(object sender, ItemClickEventArgs e)
        {
            datamodel lolol = e.ClickedItem as datamodel;
            if (lolol.title=="Add new user")
            {
                Frame.Navigate(typeof(UserReg));
            }
            else if (lolol.title == "Delete user")
            {
                Frame.Navigate(typeof(Register));
            }
            else if (lolol.title == "Search For users")
            {
                Frame.Navigate(typeof(Register));
            }
            else if (lolol.title == "Add College")
            {
                Frame.Navigate(typeof(CollegeMan));
            }
            else if (lolol.title == "Remove College")
            {
                Frame.Navigate(typeof(Register));
            }
            else if (lolol.title == "Add event")
            {
                Frame.Navigate(typeof(RegisterEvent));
            }
            
        }

        private void Core_ItemClick(object sender, ItemClickEventArgs e)
        {
            datamodel lolol = e.ClickedItem as datamodel;
            if(lolol.name=="Search")
            {
            
            }
            else if(lolol.name=="Register")
            {
                Frame.Navigate(typeof(Register));
            }
            else if(lolol.name=="Attendance")
            {
                Frame.Navigate(typeof(Attendance));
            }
            else if (lolol.name == "Update")
            {
                Frame.Navigate(typeof(update));
            }
        }

    }
}
