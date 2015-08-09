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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AttendanceMugd
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class noticeAdd : Page
    {
        StorageFile media = null;
        public noticeAdd()
        {
            this.InitializeComponent();
            imgn.Content = "Select Image";
            Descn.Content = "select file";
        }
        string about;

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog m = new MessageDialog("");
            if (Titlen.Text.Length == 0)
            {
                m.Title = "enter title";
                await m.ShowAsync();
            }
            else if (about.Length == 0)
            {
                m.Title = "enter description";
                await m.ShowAsync();
            }
            else if (issuedn.Text.Length == 0)
            {
                m.Title = "enter issued by";
                await m.ShowAsync();
            }
            else if (Venuen.Text.Length == 0)
            {
                m.Title = "enter venue";
                await m.ShowAsync();
            }
            else if (typen.SelectedValue==null)
            {
                m.Title = "select type";
                await m.ShowAsync();
            }
            else if(media==null)
            {
                m.Title = "select image";
                await m.ShowAsync();
            }
            else
            {
                Notice  item = new Notice
                {
                    Title = Titlen.Text,
                    Desc = about,
                    Date = daten.Date.DateTime,
                    type = typen.SelectedValue.ToString(),
                    issuedBy = issuedn.Text,
                    college = Venuen.Text,
                    time = Timen.Text
                    

                };
                string errorString = string.Empty;

            //    if (media != null)
            //    {
            //        // Set blob properties of TodoItem.
            //        item.ContainerName = "eventImages";
            //        item.ResourceName = media.Name;
            //    }



              myProgressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                myProgressBar.IsIndeterminate = true;
               await App.MobileService.GetTable<Notice>().InsertAsync(item);
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
                MessageDialog msgbox = new MessageDialog("Event has been added succesfully");
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
            imgn.Content = media.Name;
        }
        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            media = await openPicker.PickSingleFileAsync();
            about = await Windows.Storage.FileIO.ReadTextAsync(media);
            Descn.Content = media.Name;
        }
    }
  }

