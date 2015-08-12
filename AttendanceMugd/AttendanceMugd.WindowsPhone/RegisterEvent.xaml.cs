using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class RegisterEvent : Page
    {
        StorageFile media = null;
        private IMobileServiceTable<Events> Table = App.MobileService.GetTable<Events>();
        private MobileServiceCollection<Events, Events> items;
        public RegisterEvent()
        {
            this.InitializeComponent();
        }
        string about;

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog m = new MessageDialog("");
            if (Title.Text.Length == 0)
            {
                m.Title = "enter title";
                await m.ShowAsync();
            }
            else if (about.Length == 0)
            {
                m.Title = "enter description";
                await m.ShowAsync();
            }
            else if (issued.Text.Length == 0)
            {
                m.Title = "enter issued by";
                await m.ShowAsync();
            }
            else if (Venue.Text.Length == 0)
            {
                m.Title = "enter venue";
                await m.ShowAsync();
            }
            else if (type.SelectedValue == null)
            {
                m.Title = "select type";
                await m.ShowAsync();
            }
            else if (media == null)
            {
                m.Title = "select image";
                await m.ShowAsync();
            }
            else
            {
                Events item = new Events
                {
                    Title = Title.Text,
                    Desc = about,
                    Date = date.Date.DateTime,
                    type = type.SelectedValue.ToString(),
                    issuedBy = issued.Text,
                    college = Venue.Text,
                    time = Time.Text,
                    url = Url.Text,
                    cost = Cost.Text,
                    mobile = Contact.Text,
                    email = Email.Text
                };
                string errorString = string.Empty;

                if (media != null)
                {
                    // Set blob properties of TodoItem.
                    item.ContainerName = "eventImages";
                    item.ResourceName = media.Name;
                }



                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
                await App.MobileService.GetTable<Events>().InsertAsync(item);
                if (!string.IsNullOrEmpty(item.SasQueryString))
                {
                    // Get the URI generated that contains the SAS 
                    // and extract the storage credentials.
                    StorageCredentials cred = new StorageCredentials(item.SasQueryString);
                    var imageUri = new Uri(item.ImageUri);

                    // Instantiate a Blob store container based on the info in the returned item.
                    CloudBlobContainer container = new CloudBlobContainer(
                        new Uri(string.Format("https://{0}/{1}",
                            imageUri.Host, item.ContainerName)), cred);

                    // Get the new image as a stream.
                    using (var fileStream = await media.OpenStreamForReadAsync())
                    {
                        // Upload the new image as a BLOB from the stream.
                        CloudBlockBlob blobFromSASCredential =
                            container.GetBlockBlobReference(item.ResourceName);
                        await blobFromSASCredential.UploadFromStreamAsync(fileStream.AsInputStream());
                    }

                    // When you request an SAS at the container-level instead of the blob-level,
                    // you are able to upload multiple streams using the same container credentials.
                }


                myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MessageDialog msgbox = new MessageDialog("User has been added succesfully");
                await msgbox.ShowAsync();
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            media = await openPicker.PickSingleFileAsync();
            img.Content = media.Name;
        }
        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            media = await openPicker.PickSingleFileAsync();
            about = await Windows.Storage.FileIO.ReadTextAsync(media);
            Desc.Content = media.Name;
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
