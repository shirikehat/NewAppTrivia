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
        public ICommand LoadPlayersCommand { get; private set; }//טעינה


        public BestScoresPageViewModel(TriviaServices service)
        {
            this.service = service;

            Players = new ObservableCollection<Player>();//רשימה ריקה

            LoadPlayersCommand = new Command(async () => await
             LoadPlayers());//טעינת התלמידים
        }



        //טעינת השחקנים
        private async Task LoadPlayers()
        {
            
            var fullList = await service.OrderPlayersByScore();//נביא את אוסף השחקנים
            //נעדכן את אוסף השחקנים המוצג במסך מהרשימה המלאה
            Players.Clear();
            foreach (var student in fullList)
                Players.Add(student);


        }
    }
}

