using Android.App;
using Android.Runtime;

namespace MardinCityGuide.Mobile
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            // C# katmanındaki unhandled hataları yakalar
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Android.Util.Log.Error("CRITICAL_MAUI_ERROR", e.ExceptionObject?.ToString());
            };

            // Android Native katmanındaki hataları yakalar
            AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
            {
                Android.Util.Log.Error("CRITICAL_ANDROID_ERROR", e.Exception?.ToString());
                e.Handled = true; // Uygulamanın anında kapanmasını engellemeye çalışır
            };
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
