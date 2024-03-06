using NewAppTrivia.Models;
using NewAppTrivia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewAppTrivia.ViewModels
{
    public class BestScoresPageViewModel : ViewModel
    {
        private TriviaServices service;
        public ObservableCollection<Player> Players { get; set; }//אוסף שחקנים
        public ObservableCollection<Level> Levels { get; set; }//אוסף שחקנים
        public ICommand LoadPlayersCommand { get; private set; }//טעינה

        private bool IsOrderd=true;

        private Level selectedLevel;
        private int selectedIndex;
        private Player selectedPlayer;

        public Level SelectedLevel { get => selectedLevel; set { selectedLevel = value; OnPropertyChanged(); } }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); } }
        public Player SelectedPlayer
        {
            get => selectedPlayer; set { selectedPlayer = value; OnPropertyChanged(); }
        }
                                      //בחירה מרובה
        public ObservableCollection<Player> SelectedPlayers { get; set; } = new ObservableCollection<Player>();//אוסף הסטודנטים שנבחרו


        //טעינת השחקנים
        private async Task LoadPlayers()
        {

            var fullList = await service.OrderPlayersByScore();//נביא את אוסף השחקנים
            //נעדכן את אוסף השחקנים המוצג במסך מהרשימה המלאה
            Players.Clear();
            foreach (var player in fullList)
                Players.Add(player);
            await UpdateLevel();//נאפשר לסנן בהתאם לחודשים של התלמידים שנשלפו


        }

        

        private async Task UpdateLevel()
        {
            Levels.Clear();
            var levels =await  service.GetLevels();//נשלוף את רשימת החודשים של רשימת התלמידים המלאה. נמיין לפי הסדר
            foreach (var level in levels)//נוסיף לרשימת החודשים המוצגת.
                Levels.Add(level);
            SelectedIndex = -1;//שדה בחירה יהיה ריק 
        }

        public BestScoresPageViewModel(TriviaServices service)
        {
            this.service = service;
            
            Players = new ObservableCollection<Player>();//רשימה ריקה
            Levels = new ObservableCollection<Level>();
            LoadPlayersCommand = new Command(async () => await
             LoadPlayers());//טעינת התלמידים

            RefreshCommand = new Command(async () => await Refresh());
            DeleteCommand = new Command(() => Delete());

            fullList = new List<Player>();//רשימה ריקה

            FilterCommand = new Command(async () => await
            Filter(), () =>  SelectedIndex != -1 ) ;//סינון התלמידים
            ClearFilterCommand = new Command(async () => {; await LoadPlayers(); });//החזרת כל הערכים לקדמותם
            SelectedIndex = -1;
        }






        #region רענון מסך
        private bool isRefreshing;

        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; private set; }
        #endregion


        private async Task Refresh()
        {
            IsRefreshing = true;
            //נפעיל את אייקון הרענון
            await ReloadAndOrder();//נטען את הרשימה מחדש
            IsRefreshing = false;//בסיום נבטל את אייקון הרענון

        }
        private async Task ReloadAndOrder()
        {
            List<Player> list; 
            if(IsOrderd)
            {
                list = await service.OrderPlayersByScoreFromLow();
                IsOrderd = false;   
            }
            else
            {
                list=await service.OrderPlayersByScore();
                IsOrderd = true;
            }
            Players.Clear();
            foreach (var player in list)
                Players.Add(player);
        }





        public ICommand DeleteCommand { get; private set; }//מחיקת סטודנט
                                                           //פעולות נוספות שנרצה להציג בתפריט
        

        private void Delete()
        {

            if (SelectedPlayer != null)
            {
                Players.Remove(SelectedPlayer);//הסרת התלמיד מהאוסף
                SelectedPlayer = null;//איפוס הבחירה
            }
            //בחירה מרובה
            //if(SelectedStudents.Count>0)
            //foreach(var student in SelectedStudents)
            //Students.Remove(student);
            //SelectedStudents.Clear();//איפוס
        }



        private List<Player> fullList;//  אוסף סטודנטים המלא
        public ICommand FilterCommand { get; private set; }//פעולת הסינון
        public ICommand ClearFilterCommand { get; private set; }//ביטול סינון
       






        //טעינת התלמידים

        public async Task Filter()
        {

            var filteredList = await service.OrderPlayersByScore();//אוסף הפריטים שעונה על התנאי
            filteredList = filteredList.Where(player => player.LevelCode == SelectedLevel.LevelCode).ToList();              //הרצת התנאי.......	
            await Task.Delay(1000);

             Players.Clear();
            foreach (var player in filteredList)
                Players.Add(player);
            SelectedIndex = -1;//שדה בחירה יהיה ריק  

        }





    }
}


