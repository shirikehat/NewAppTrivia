using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using NewAppTrivia.Models;

namespace NewAppTrivia.Services
{
    public class TriviaServices
    {
        public List<Player> PlayerList { get; set; }
        public List<Level> levels { get; set; }
        public List<Question> QuestionList { get; set; }
        public Status status { get; set; }
        public Subject subject { get; set; }

        public TriviaServices() 
        {
            FillPlayers();
            fillLevels();
        }

        private void fillLevels()
        {
           levels = new List<Level>();
            levels.Add(new Level() { LevelCode = 1, Name = "Master" });
            levels.Add(new Level() { LevelCode = 2, Name = "Manager" });
            levels.Add(new Level() { LevelCode = 3, Name = "Rookie" });
        }

        public void AddPlayer(Player p)
        {
            PlayerList.Add(p);
        }
        private void FillPlayers()
        {
            PlayerList = new List<Player>();
            PlayerList.Add(new Player() { Name = "k1", Password = "123", LevelCode=1 , Points=100, PlayerId=0});
            PlayerList.Add(new Player() { Name = "k2", Password = "123", LevelCode=2 , Points=200, PlayerId=1});
            PlayerList.Add(new Player() { Name = "k3", Password = "123", LevelCode = 2, Points = 300 , PlayerId=2 }) ;

        }

        public bool Login(string playerName, string password)
        {
            return PlayerList.Any(x=>x.Name == playerName && x.Password == password);
        }

        //פעולה לדף רשימת שחקנים מהניקוד הגבוה
        public async Task<List<Player>> OrderPlayersByScore()
        {
            return this.PlayerList.OrderByDescending(p => p.Points).ToList();
        }

        public async Task<List<Player>> OrderPlayersByScoreFromLow()
        {
            return this.PlayerList.OrderBy(p => p.Points).ToList();
        }
        public async Task<List<Level>> GetLevels()
        {
            return levels;
        }


        

    }
}
