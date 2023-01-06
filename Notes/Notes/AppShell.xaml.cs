using Notes.Views;
using Xamarin.Forms;

namespace Notes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //will need to remove this 
            Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
            
            //To register from new goal tab bar back to list of goals
            Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));
        }
    }
}
