﻿using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EmilioMVVM.ViewModels
{
    internal class NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.NoteViewModel> AllNotes { get; }
        public ICommand NewCommand_EGuerrero { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModel()
        {
            AllNotes = new ObservableCollection<ViewModels.NoteViewModel>(Models.Note.LoadAll_EGuerrero().Select(n => new NoteViewModel(n)));
            NewCommand_EGuerrero = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModels.NoteViewModel>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePage));
        }

        private async Task SelectNoteAsync(ViewModels.NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Eliminado"))
            {
                string noteId = query["Eliminado"].ToString();
                NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("Guardado"))
            {
                string noteId = query["Guardado"].ToString();
                NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();


                if (matchedNote != null)
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);


                else
                    AllNotes.Insert(0, new NoteViewModel(Models.Note.Load_EGuerrero(noteId)));
            }
        }
      
    }
}
