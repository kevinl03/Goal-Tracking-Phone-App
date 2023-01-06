using System;
using System.IO;
using Notes.Models;
using Xamarin.Forms;

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [DesignTimeVisible(false)]

    public partial class NoteEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Note();
        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }


        //the toggler
        bool selection1Active = true;
        bool selection2Active = false;
        private double valueX, valueY;
        private bool IsTurnX, IsTurnY;

        public void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var x = e.TotalX; // TotalX Left/Right
            var y = e.TotalY; // TotalY Up/Down

            // StatusType
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    Debug.WriteLine("Started");
                    break;
                case GestureStatus.Running:
                    Debug.WriteLine("Running");

                    // Check that the movement is x or y
                    if ((x >= 5 || x <= -5) && !IsTurnX && !IsTurnY)
                    {
                        IsTurnX = true;
                    }

                    if ((y >= 5 || y <= -5) && !IsTurnY && !IsTurnX)
                    {
                        IsTurnY = true;
                    }

                    // X (Horizontal)
                    if (IsTurnX && !IsTurnY)
                    {
                        if (x <= valueX)
                        {
                            // Left
                            if (selection2Active)
                            {
                                //make selection1 move to selection 2

                                selection1Active = true;
                                selection2Active = false;
                                text2.TextColor = Color.Black;
                                text1.TextColor = Color.White;
                                runningFrame.TranslateTo(runningFrame.X, 0, 220);
                                var duration = TimeSpan.FromSeconds(1);
                                Vibration.Vibrate(duration);

                            }
                        }

                        if (x >= valueX)
                        {
                            // Right
                            if (selection1Active)
                            {
                                //make selection2 move to selection 1
                                selection1Active = false;
                                selection2Active = true;
                                text2.TextColor = Color.White;
                                text1.TextColor = Color.Black;
                                runningFrame.TranslateTo(runningFrame.X + 140, 0, 220);
                                var duration = TimeSpan.FromSeconds(1);
                                Vibration.Vibrate(duration);

                            }
                        }
                    }


                    break;
                case GestureStatus.Completed:
                    Debug.WriteLine("Completed");

                    valueX = x;
                    valueY = y;

                    IsTurnX = false;
                    IsTurnY = false;

                    break;
                case GestureStatus.Canceled:
                    Debug.WriteLine("Canceled");
                    break;


            }
        }

        void OnText1Tapped(System.Object sender, System.EventArgs e)
        {
            if (selection2Active)
            {
                //make selection1 move to selection 2

                selection1Active = true;
                selection2Active = false;
                text2.TextColor = Color.Black;
                text1.TextColor = Color.White;
                runningFrame.TranslateTo(runningFrame.X, 0, 220);
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
                
            }
        }

        async void OnText2Tapped(System.Object sender, System.EventArgs e)
        {
            if (selection1Active)
            {
                //make selection2 move to selection 1
                selection1Active = false;
                selection2Active = true;
                text2.TextColor = Color.White;
                text1.TextColor = Color.Black;
                runningFrame.TranslateTo(runningFrame.X + 140, 0, 220);
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
               
            }
        }
    }
}
