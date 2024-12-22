using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmilioMVVM.ViewModels
{
    internal class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private Models.Note _note;
        public string Text
        {
            get => _note.Text;
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.Date;

        public string Identifier => _note.Filename;

        public ICommand SaveCommand_EGuerrero { get; private set; }
        public ICommand DeleteCommand_EGuerrero { get; private set; }

        public NoteViewModel()
        {
            _note = new Models.Note();
            SaveCommand_EGuerrero = new AsyncRelayCommand(SaveEGuerrero);
            DeleteCommand_EGuerrero = new AsyncRelayCommand(Delete);
        }

        public NoteViewModel(Models.Note note)
        {
            _note = note;
            SaveCommand_EGuerrero = new AsyncRelayCommand(SaveEGuerrero);
            DeleteCommand_EGuerrero = new AsyncRelayCommand(Delete);
        }

        private async Task SaveEGuerrero()
        {
            _note.Date = DateTime.Now;
            _note.Save_EGuerrero();
            await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
        }

        private async Task Delete()
        {
            _note.Delete_EGuerrero();
            await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.Note.Load_EGuerrero(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _note = Models.Note.Load_EGuerrero(_note.Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
      
    }
}
