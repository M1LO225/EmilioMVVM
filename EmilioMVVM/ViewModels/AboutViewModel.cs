using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


namespace EmilioMVVM.ViewModels
{
    internal class AboutViewModel
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://www.instagram.com/emi_lio_gue/";
        public string Message => "Emilio Guerrero";
        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
