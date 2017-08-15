using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XPhotoFrame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent( );

            imgFrame.Source = ImageSource.FromResource( "XPhotoFrame.Resources.Images.image_frame.png" );
            imgUser.Source = ImageSource.FromResource( "XPhotoFrame.Resources.Images.image_user.png" );

            var tgrUser = new TapGestureRecognizer( );
            tgrUser.Tapped += async ( sender, args ) =>
            {
                await CrossMedia.Current.Initialize( );

                if( !CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported )
                {
                    await DisplayAlert( "No Camera", ":( No camera available.", "OK" );
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync( new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    SaveToAlbum = true,
                } );
                if( file == null )
                {
                    return;
                }

                imgUser.Source = ImageSource.FromStream( () =>
                {
                    var stream = file.GetStream( );
                    file.Dispose( );
                    return stream;
                } );
            };
            imgUser.GestureRecognizers.Add( tgrUser );
        }
    }
}