using Avalonia.Controls;
using Avalonia;
using System;
using System.IO;
using System.Net.Http;

namespace Mangadexter.Gui.Controls;
public class WebImage : UserControl
{
    public static readonly DirectProperty<WebImage, string> ImageUrlProperty =
        AvaloniaProperty.RegisterDirect<WebImage, string>(
            nameof(ImageUrl),
            o => o.ImageUrl,
            (o, v) => o.ImageUrl = v);

    private string _imageUrl;
    public string ImageUrl
    {
        get { return _imageUrl; }
        set
        {
            SetAndRaise(ImageUrlProperty, ref _imageUrl, value);
            DownloadImage(value);
        }
    }

    private Image _imageControl;

    static HttpClient client = new HttpClient { MaxResponseContentBufferSize = 1024 * 1024 * 10 };

    public WebImage()
    {
        _imageControl = new Image();
        this.Content = _imageControl;
    }

    private async void DownloadImage(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            _imageControl.Source = null;
            return;
        }

        try
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mangadexter GUI");
            byte[] bytes = await client.GetByteArrayAsync(url);
            Stream stream = new MemoryStream(bytes);
            var image = new Avalonia.Media.Imaging.Bitmap(stream);
            _imageControl.Source = image;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            _imageControl.Source = null; // Could not download...
        }
    }
}
