using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace MardinCityGuide.Mobile.Views.Map;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var pins = new[]
        {
        new { Label = "Kayseriyye Çarşısı", Lat = 37.3135, Lng = 40.7370 },
        new { Label = "Mardin Bakırcılar Çarşısı", Lat = 37.3112, Lng = 40.7385 },
        new { Label = "Gümüşçüler ve Telkarcılar Çarşısı", Lat = 37.3140, Lng = 40.7340 }
    };

        var markersJs = string.Join("\n", pins.Select(p =>
            $"L.marker([{p.Lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
            $"{p.Lng.ToString(System.Globalization.CultureInfo.InvariantCulture)}])" +
            $".addTo(map).bindPopup('{p.Label}');"));

        string html = $@"<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no'>
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css' />
    <style> 
        html, body, #map {{ height: 100%; margin: 0; padding: 0; width: 100%; }} 
    </style>
</head>
<body>
    <div id='map'></div>
    <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
    <script>
        var map = L.map('map').setView([37.3129, 40.7352], 15);

        // OpenStreetMap yerine WebView engeline takılmayan CartoDB harita katmanı kullanıyoruz:
        L.tileLayer('https://{{s}}.basemaps.cartocdn.com/rastertiles/voyager/{{z}}/{{x}}/{{y}}{{r}}.png', {{
            attribution: '&copy; <a href=""https://www.openstreetmap.org/copyright"">OpenStreetMap</a> contributors &copy; <a href=""https://carto.com/attributions"">CARTO</a>',
            subdomains: 'abcd',
            maxZoom: 19
        }}).addTo(map);

        {markersJs}
    </script>
</body>
</html>";

        MyMapView.Source = new HtmlWebViewSource { Html = html };
    }
}
