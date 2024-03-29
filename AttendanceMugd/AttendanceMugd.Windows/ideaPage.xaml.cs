﻿using System;
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
    public sealed partial class ideaPage : Page
    {
        public ideaPage()
        {
            this.InitializeComponent();
        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog m = new MessageDialog("");

            if (title.Text.Length == 0)
            {
                m.Title = "enter title";
                await m.ShowAsync();
            }
            else if (option_1.Text.Length == 0)
            {
                m.Title = "enter description";
                await m.ShowAsync();
            }
            else if (option_2.Text.Length == 0)
            {
                m.Title = "enter issued by";
                await m.ShowAsync();
            }
            else if (option_3.Text.Length == 0)
            {
                m.Title = "enter venue";
                await m.ShowAsync();
            }
            else
            {
                idea temp = new idea();
                temp.Title = title.Text;
                temp.option1 = option_1.Text;
                temp.option2 = option_2.Text;
                temp.option3 = option_3.Text;
                // entered into azure
                myProgressBar.Visibility = Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
                await App.MobileService.GetTable<idea>().InsertAsync(temp);
                myProgressBar.Visibility = Visibility.Collapsed;
                Windows.UI.Popups.MessageDialog messageBox = new MessageDialog("Idea has been added successfully");
                await messageBox.ShowAsync();

            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
