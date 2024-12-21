namespace EmilioMVVM.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
	public NotePage()
	{
		InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }
    private async void GuardarButton_Clicked_EGuerrero(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
            File.WriteAllText(note.Filename, TextEditor_EGuerrero.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void EliminarButton_Clicked_EGuerrero(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }
        await Shell.Current.GoToAsync("..");

    }
    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }
    public string ItemId
    {
        set { LoadNote(value); }
    }
}