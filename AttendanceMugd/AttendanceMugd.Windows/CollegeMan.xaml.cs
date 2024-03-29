﻿using Microsoft.WindowsAzure.MobileServices;
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
    public sealed partial class CollegeMan : Page
    {
            
        public CollegeMan()
        {
            this.InitializeComponent();
            
                //Type.DataContext = type;
                //onlaunch();
        }
        
                

            
            
            private async void submit_Click(object sender, RoutedEventArgs e)
            {
                Colleges item = new Colleges
                {
                    collegeName=Namen.Text,
                    university=Email.Text
                };
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
                await App.MobileService.GetTable<Colleges>().InsertAsync(item);
                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("College has been added succesfully");
                await msgbox.ShowAsync();
            }

            private void back_Click(object sender, RoutedEventArgs e)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }

