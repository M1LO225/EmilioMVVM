namespace EmilioMVVM.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();

	}
    private async void LearnMore_Clicked_EGuerrero(object sender, EventArgs e)
    {
        if (BindingContext is Models.About about)
        {
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}