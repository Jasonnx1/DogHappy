using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {


        private List<String> breeds;

        public List<String> Breeds
        {
            get => breeds;
            set
            {
                breeds = value;
                OnPropertyChanged();
            }
        }

        private List<String> images;

        public List<String> Images
        {
            get => images;
            set
            {
                images = value;
                OnPropertyChanged();
            }
        }
        private String currentImage;
        public String CurrentImage
        {
            get => currentImage;
            set
            {
                currentImage = value;
                OnPropertyChanged();
            }
        }


        private int currentIndex;
        private int maxIndex;
        public int CurrentIndex
        {
            get => currentIndex;         
            set
            {
                currentIndex = value;
                DisplayCurrentIndex = CurrentIndex;
                OnPropertyChanged();
            }
        }


        private int displayCurrentIndex;
        public int DisplayCurrentIndex
        {
            get => displayCurrentIndex + 1;
            set
            {
                displayCurrentIndex = value;
                OnPropertyChanged();
            }
        }

        public int MaxIndex
        {
            get => maxIndex;
            set
            {
                maxIndex = value;
                OnPropertyChanged();
            }
        }



        private int numberOfImages;
        public int NumberOfImages
        {
            get => numberOfImages;
            set
            {
                numberOfImages = value;
                OnPropertyChanged();
            }
        }

        private String selectedBreed;
        public String SelectedBreed
        {
            get => selectedBreed;
            set
            {
                selectedBreed = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand FetchDogsCommand { get; set; }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }

        public RelayCommand ChangeLanguageCommand { get; set; }

        public MainViewModel()
        {
            FetchDogsCommand = new AsyncCommand(FetchDogs);
            NextImageCommand = new RelayCommand(NextImage, CanExecuteNext);
            PreviousImageCommand = new RelayCommand(PreviousImage, CanExecutePrevious);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
            Breeds = new List<String>();
            Task.Run(() => Load());
            
        }

        private void ChangeLanguage(object o)
        {
            if ((string)o != DogFetchApp.Properties.Settings.Default.Lang)
            {
                DogFetchApp.Properties.Settings.Default.Lang = (string)o;
                DogFetchApp.Properties.Settings.Default.Save();


                if((string)o == "fr")
                {
                    if ((MessageBox.Show("Voulez-vous redémarrez l'application?", " ", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                    {
                        var filename = Application.ResourceAssembly.Location;
                        var newFile = Path.ChangeExtension(filename, ".exe");
                        Process.Start(newFile);
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    if ((MessageBox.Show("Do you want to restart now?", " ", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                    {
                        var filename = Application.ResourceAssembly.Location;
                        var newFile = Path.ChangeExtension(filename, ".exe");
                        Process.Start(newFile);
                        Application.Current.Shutdown();
                    }
                }



            }


        }

        public async Task FetchDogs()
        {
            Images = await DogApiProcessor.GetImageUrl(SelectedBreed, NumberOfImages);
            MaxIndex = Images.Count;
            CurrentIndex = 0;
            CurrentImage = Images[CurrentIndex];
            NextImageCommand.RaiseCanExecuteChanged();
            PreviousImageCommand.RaiseCanExecuteChanged();

        }

        public void NextImage(object o)
        {
            CurrentIndex++;
            CurrentImage = Images[CurrentIndex];
            PreviousImageCommand.RaiseCanExecuteChanged();

            if(CurrentIndex == MaxIndex - 1)
            {
                NextImageCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecuteNext(object o)
        {
            if (CurrentIndex == MaxIndex - 1 || Images == null)
            {
                return false;
            }
            else return true;

        }


        public void PreviousImage(object o)
        {
            CurrentIndex--;
            CurrentImage = Images[CurrentIndex];
            NextImageCommand.RaiseCanExecuteChanged();

            if(currentIndex == 0)
            {
                PreviousImageCommand.RaiseCanExecuteChanged();
            }

        }

        public bool CanExecutePrevious(object o)
        {
            if (currentIndex == 0 || Images == null)
            {
                return false;
            }
            else return true;

        }

        private async Task Load()
        {
             Breeds = await DogApiProcessor.LoadBreedList();
             SelectedBreed = Breeds[0];
             NumberOfImages = 1;
             DisplayCurrentIndex = -1;
        }
    }
}
