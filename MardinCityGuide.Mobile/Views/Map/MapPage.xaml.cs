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

        string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css' />
    <style> html, body, #map {{ height: 100%; margin: 0; padding: 0; }} </style>
</head>
<body>
    <div id='map'></div>
    <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
    <script>
        var map = L.map('map').setView([37.3129, 40.7352], 15);
        L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
            attribution: '&copy; OpenStreetMap contributors'
        }}).addTo(map);
        {markersJs}
    </script>
</body>
</html>";

        MyMapView.Source = new HtmlWebViewSource { Html = html };
    }
}
