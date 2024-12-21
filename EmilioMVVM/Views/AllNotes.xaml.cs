namespace EmilioMVVM.Views;

public partial class AllNotes : ContentPage
{
	public AllNotes()
	{
		InitializeComponent();
        BindingContext = new Models.AllNotes();
    }
    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked_EGuerrero(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }


    private async void notesCollection_EGuerrero_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0) 
        {
            var note = (Models.Note)e.CurrentSelection[0];
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");
            notesCollection_EGuerrero.SelectedItem = null;
        }
    }

}