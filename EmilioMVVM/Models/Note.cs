using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilioMVVM.Models
{
    internal class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Note()
        {
            Filename = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
        }

        public void Save_EGuerrero() =>
        File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

        public void Delete_EGuerrero() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static Note Load_EGuerrero(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("No se ha podido encontrar el archivo", filename);

            return
                new()
                {
                    Filename = Path.GetFileName(filename),
                    Text = File.ReadAllText(filename),
                    Date = File.GetLastWriteTime(filename)
                };
        }

        public static IEnumerable<Note> LoadAll_EGuerrero()
        {

            string appDataPath = FileSystem.AppDataDirectory;

            return Directory


                    .EnumerateFiles(appDataPath, "*.notes.txt")


                    .Select(filename => Note.Load_EGuerrero(Path.GetFileName(filename)))


                    .OrderByDescending(note => note.Date);
        }
    }
}
