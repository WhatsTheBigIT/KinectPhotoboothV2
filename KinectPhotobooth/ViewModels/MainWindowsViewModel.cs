using KinectPhotobooth.Common;
using KinectPhotobooth.Models;
using KinectPhotobooth.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectPhotobooth.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private EmailService _email;

        private WriteableBitmap _bitmap = null;

        public ICommand OnClearClicked { get; set; }

        private bool _LeaveTrails;
        public bool LeaveTrails
        {
            get { return _LeaveTrails; }
            set
            {
                _LeaveTrails = value;
                OnProperyChanged();
            }
        }

        private bool _PersonFill;
        public bool PersonFill
        {
            get { return _PersonFill; }
            set
            {
                _PersonFill = value;
                OnProperyChanged();
            }
        }
        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _bitmap;
            }
        }

        private ImageSource _nis;
        public ImageSource NewImageSource
        {
            get
            {
                return _nis;
            }
            set
            {
                _nis = value;
                OnProperyChanged();
            }
        }

        private double _BackgroundDistance;
        public double BackgroundDistance
        {
            get
            {
                return _BackgroundDistance;
            }

            set
            {
                _BackgroundDistance = value;
                Inches = value;
                OnProperyChanged();
            }
        }

        public double Inches
        {
            get
            {
                return 0.039370 * _BackgroundDistance;
            }
            set
            {
                OnProperyChanged();
            }
        }

        private string _StatusText;
        public  string StatusText
        {
            get
            {
                return _StatusText;
            }
            set
            {
                _StatusText = value;
                OnProperyChanged();
            }
        }

        private BackgroundModel _SelectedBackground;
        public BackgroundModel SelectedBackground
        {
            get { return _SelectedBackground; }
            set
            {
                _SelectedBackground = value;
                OnProperyChanged();
            }
        }

        private string _EmailAddress = "wifink@microsoft.com";

        public string EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                _EmailAddress = value;
                OnProperyChanged();
            }
        }


        private bool _ContactMe = true;
        public bool ContactMe
        {
            get
            {
                return _ContactMe;
            }
            set
            {
                _ContactMe = value;
                OnProperyChanged();
            }
        }

        public ObservableCollection<BackgroundModel> Backgrounds { get; set; }

        private void ClearClickedCommand()
        {
            EmailAddress = "";
            ContactMe = true;
        }
        public MainWindowViewModel()
        {
            OnClearClicked = new RelayCommand(ClearClickedCommand);

            Backgrounds = new ObservableCollection<BackgroundModel>();
            
            //Set the initial Background depth to 4000 mm
            _BackgroundDistance = 4000;

           // Backgrounds.Add(new BackgroundModel() { Name = "Kalahari 001", ImagePath = "Images/Backgrounds/kalahari001.jpg" });
           // Backgrounds.Add(new BackgroundModel() { Name = "Kalahari 002", ImagePath = "Images/Backgrounds/kalahari002.jpg" });


            Backgrounds.Add(new BackgroundModel() { Name = "Beach", ImagePath = "Images/Backgrounds/Beach.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Building", ImagePath = "Images/Backgrounds/FromBuilding.jpg" });
            

            Backgrounds.Add(new BackgroundModel() { Name = "Cliff Jump", ImagePath = "Images/Backgrounds/Cliff-jump.jpg" });

            Backgrounds.Add(new BackgroundModel() { Name = "Crowd", ImagePath = "Images/Backgrounds/Crowd.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Dinosaur", ImagePath = "Images/Backgrounds/Dino.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Explosion", ImagePath = "Images/Backgrounds/explosion.jpg" });




                
            Backgrounds.Add(new BackgroundModel() { Name = "Flames", ImagePath = "Images/Backgrounds/Flames.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Flowers", ImagePath = "Images/Backgrounds/Flowers1.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Galaxy", ImagePath = "Images/Backgrounds/Galaxy.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Lightning", ImagePath = "Images/Backgrounds/lightning.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Mars", ImagePath = "Images/Backgrounds/mars.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Moon", ImagePath = "Images/Backgrounds/astronaut_on_the_moon.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Presidents", ImagePath = "Images/Backgrounds/Presidents2.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Shark", ImagePath = "Images/Backgrounds/Shark.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Spaceship Window", ImagePath = "Images/Backgrounds/SpaceshipWindow.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Star Trek", ImagePath = "Images/Backgrounds/StarTrek1.jpg" });
            //Backgrounds.Add(new BackgroundModel() { Name = "St. Louis Skyline 1", ImagePath = "Images/Backgrounds/StLouisSkyline.jpg" });
            //Backgrounds.Add(new BackgroundModel() { Name = "St. Louis Skyline 2", ImagePath = "Images/Backgrounds/StLouis2.jpg" });
            //Backgrounds.Add(new BackgroundModel() { Name = "Chicago", ImagePath = "Images/Backgrounds/chicago.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Tornado", ImagePath = "Images/Backgrounds/tornado.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Volcano", ImagePath = "Images/Backgrounds/volcano.jpg" });
            Backgrounds.Add(new BackgroundModel() { Name = "Warp", ImagePath = "Images/Backgrounds/Warp.jpg" });


            var myPhotos = String.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Photobooth");


            var path = new DirectoryInfo(myPhotos);
            if (!path.Exists)
            {
                path.Create();
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public async void SendMail(BitmapEncoder bitmap)
        {

            if (_email == null)
            {
                _email = new EmailService();
            }


            bool mailSent = await _email.SendMail(_EmailAddress, bitmap);

        }


    }
}
