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
        int i = 5;
        private IMobileServiceTable<Users> Table1 = App.MobileService.GetTable<Users>();
        private MobileServiceCollection<Users, Users> items1;

        private IMobileServiceTable<Student> Table = App.MobileService.GetTable<Student>();
        private MobileServiceCollection<Student, Student> items;
        public MainPage()
        {
            this.InitializeComponent();
         
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.InitializeComponent();
            if (i == 0)
            {
                red.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                green.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                yello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                blue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
                onlaunchadmin();
                onlaunchcore();
            }
            else if (i == 1)
            {
                red.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                green.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                yello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                blue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
                onlaunchcore();
            }
            else
            {
                input.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
        }
        private void onlaunchcore()
        {
            List<datamodel> myList = new List<datamodel>();
            datamodel temp = new datamodel();
            temp.back = "Red";
            temp.name = "Register";
            temp.src = "/Assets/student.png";
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


            core.DataContext = myList;

        }

        private void onlaunchadmin()
        {
            List<datamodel> myList = new List<datamodel>();
            datamodel temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/user.png";
            temp.title = "Add new user";
            temp.desc = "Use this option to add new user to the database of users. Users are either core members or admins or publicity and have different priviledges accordingly";
            myList.Add(temp);
            temp.back = "Blue";
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/userdel.png";
            temp.title = "Delete user";
            temp.desc = "Use this option to delete user. Once deleted the user will losse all access to this app";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/searchuser.png";
            temp.title = "Search For users";
            temp.desc = "Use this option to find users and see their details";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/collegedel.png";
            temp.title = "Add College";
            temp.desc = "Use this option to Add Colleges";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/college.png";
            temp.title = "Remove College";
            temp.desc = "Use this option to remove college";
            myList.Add(temp);
            temp = new datamodel();
            temp.back = "Blue";
            temp.src = "/Assets/events.png";
            temp.title = "Add event";
            temp.desc = "Use this option to add new event";
            myList.Add(temp);
            temp = new datamodel();

            temp.back = "Blue";
            temp.name = "Add Idea";
            temp.src = "/Assets/student.png";
            temp.title = "Add new Idea";
            temp.desc = "Use this option to add new idea to the group";
            myList.Add(temp);
            temp = new datamodel();

            temp.back = "Blue";
            temp.src = "/Assets/notice.png";
            temp.title = "Add notice";
            temp.desc = "Use this option to Add Notice";
            myList.Add(temp);
            admin.DataContext = myList;
            temp = new datamodel();


            admin.DataContext = myList;


        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //TODO add forgot password logic
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO add forgot password logic
        }

        private void admin_ItemClick(object sender, ItemClickEventArgs e)
        {
            datamodel lolol = e.ClickedItem as datamodel;
            if (lolol.title == "Add new user")
            {
                Frame.Navigate(typeof(UserReg));
            }
            else if (lolol.title == "Delete user")
            {
                Frame.Navigate(typeof(userDel));
            }
            else if (lolol.title == "Search For users")
            {
                Frame.Navigate(typeof(searchUser));
            }
            else if (lolol.title == "Add College")
            {
                Frame.Navigate(typeof(CollegeMan));
            }
            else if (lolol.title == "Remove College")
            {
                Frame.Navigate(typeof(collegeDel));
            }
            else if (lolol.title == "Add event")
            {
                Frame.Navigate(typeof(RegisterEvent));
            }
            else if (lolol.title == "Add notice")
            {
                Frame.Navigate(typeof(noticeAdd));
            }
            else if (lolol.title == "Add new Idea")
            {
                Frame.Navigate(typeof(ideaPage));
            }


        }





        private void Core_ItemClick(object sender, ItemClickEventArgs e)
        {
            datamodel lolol = e.ClickedItem as datamodel;
            if (lolol.name == "Search")
            {
                Frame.Navigate(typeof(searchUser));
            }
            else if (lolol.name == "Register")
            {
                Frame.Navigate(typeof(Register));
            }
            else if (lolol.name == "Attendance")
            {
                Frame.Navigate(typeof(Attendance));
            }

        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {

            MessageDialog m = new MessageDialog("");

            if (userName.Text == "lol1234")
            {
                i = 0;
                m.Title = "Welcome aboard Supreme Commander... :):)";
                await m.ShowAsync();
                red.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                green.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                yello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                blue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
                onlaunchadmin();
                onlaunchcore();
            }
            else
            {

                if (userName.Text.Length == 0)
                {
                    m.Title = "enter Name";
                    await m.ShowAsync();
                }
                else if (password.Password.Length == 0)
                {
                    m.Title = "enter Password";
                    await m.ShowAsync();
                }
                else
                {

                    myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    myProgressBar.IsIndeterminate = true;
                    items1 = await Table1.Where(TableItem => TableItem.userName == userName.Text).ToCollectionAsync();
                    if (items1.Count != 0)
                    {
                        myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                        if (items1[0].password == password.Password)
                        {
                            if (items1[0].type == "Admin")
                            {
                                m.Title = "Welcome aboard Captain... :):)";
                                i = 0;
                                await m.ShowAsync();
                                red.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                green.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                yello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                blue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
                                onlaunchadmin();
                                onlaunchcore();
                            }
                            else if (items1[0].type == "Member")
                            {
                                m.Title = "Sorry Sweety you are not allowed to access this :)";
                                await m.ShowAsync();
                            }
                            else if (items1[0].type == "Core")
                            {
                                i = 1;
                                red.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                green.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                yello.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                blue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                Page.Visibility = Windows.UI.Xaml.Visibility.Visible;
                                onlaunchcore();
                            }
                        }
                    }
                    else
                    {
                        myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        m.Title = "Tumse Na Ho Payega ;)";
                        await m.ShowAsync();

                    }


                }

            }
        }



      
    }
}
