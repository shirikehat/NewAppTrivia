using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NewAppTrivia.Models;

namespace NewAppTrivia.Services
{
    internal class TriviaServices
    {
        public List<Player> PlayerList { get; set; }
        public Level level { get; set; }
        public List<Question> QuestionList { get; set; }
        public Status status { get; set; }
        public Subject subject { get; set; }

        public TriviaServices() 
        {
            FillPlayers();
        }
        public void AddPlayer(Player p)
        {
            PlayerList.Add(p);
        }
        private void FillPlayers()
        {
            PlayerList = new List<Player>();
            PlayerList.Add(new Player() { Name = "k", Password = "123" });
        }

        public bool Login(string playerName, string password)
        {
            return PlayerList.Any(x=>x.Name == playerName && x.Password == password);
        }

        //פעולה לדף רשימת שחקנים מהניקוד הגבוה
        public List<Player> OrderPlayersByScore()
        {
            return this.PlayerList.OrderByDescending(p => p.Points).ToList();
        }
    }
}
