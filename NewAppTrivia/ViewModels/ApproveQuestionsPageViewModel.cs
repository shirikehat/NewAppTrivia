
using NewAppTrivia.Models;
using NewAppTrivia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Subject = NewAppTrivia.Models.Subject;

namespace NewAppTrivia.ViewModels
{
    public class ApproveQuestionsPageViewModel :ViewModel
    {
        private TriviaServices service;

        public ObservableCollection<Question> Questions { get; set; }//אוסף שחקנים
        public ObservableCollection<Subject> Subjects { get; set; }//אוסף שחקנים
        public ICommand LoadQuestionsCommand { get; private set; }//טעינה
        public ICommand ApproveCommand {  get; private set; }
        public ICommand DeclineCommand { get; private set; }
      

        private Subject selectedSubject;
        private int selectedIndex;

        public Subject SelectedSubject { get => selectedSubject; set { selectedSubject = value; OnPropertyChanged(); } }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); } }
        //בחירה מרובה
        public ObservableCollection<Question> SelectedQuestions { get; set; } = new ObservableCollection<Question>();//אוסף הסטודנטים שנבחרו


        //טעינת השחקנים
        private async Task LoadQuestions()
        {

            var fullList = await service.PendingQuestion();//נביא את אוסף השחקנים
            //נעדכן את אוסף השחקנים המוצג במסך מהרשימה המלאה
            Questions.Clear();
            foreach (var player in fullList)
                Questions.Add(player);
        }

        public ApproveQuestionsPageViewModel(TriviaServices service)
        {
            this.service = service;

            Questions = new ObservableCollection<Question>();//רשימה ריקה
            Subjects = new ObservableCollection<Subject>();
            foreach (Subject s in service.subjects)
            {
                Subjects.Add(s);
            }
            LoadQuestionsCommand = new Command(async () => await
             LoadQuestions());//טעינת התלמידים

            RefreshCommand = new Command(async () => await Refresh());
            DeleteCommand = new Command(() => Delete());
            ApproveCommand = new Command<Question>((q) => ChangeStatusToApprove(q));
            DeclineCommand = new Command<Question>((q) => ChangeStatusToDecline(q));

            fullList = new List<Question>();//רשימה ריקה

            FilterCommand = new Command(async () => await
            Filter(), () => SelectedIndex != -1);//סינון התלמידים
            ClearFilterCommand = new Command(async () => {; await LoadQuestions(); });//החזרת כל הערכים לקדמותם
            SelectedIndex = -1;
        }

        public void ChangeStatusToApprove(Question q)
        {
            q.StatusCode = 1;
            Questions.Remove(q);
        }

        public void ChangeStatusToDecline(Question q)
        {
            q.StatusCode = 2;
            Questions.Remove(q);
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
            await LoadQuestions();//נטען את הרשימה מחדש
            IsRefreshing = false;//בסיום נבטל את אייקון הרענון

        }
        





        public ICommand DeleteCommand { get; private set; }//מחיקת סטודנט
                                                           //פעולות נוספות שנרצה להציג בתפריט


        private void Delete()
        {
            //בחירה מרובה
            //if(SelectedStudents.Count>0)
            //foreach(var student in SelectedStudents)
            //Students.Remove(student);
            //SelectedStudents.Clear();//איפוס
        }



        private List<Question> fullList;//  אוסף סטודנטים המלא
        public ICommand FilterCommand { get; private set; }//פעולת הסינון
        public ICommand ClearFilterCommand { get; private set; }//ביטול סינון







        //טעינת התלמידים

        public async Task Filter()
        {

            var filteredList = await service.PendingQuestion();//אוסף הפריטים שעונה על התנאי
            filteredList = filteredList.Where(x => x.SubjectCode == SelectedSubject.SubjectCode).ToList();              //הרצת התנאי.......	
            await Task.Delay(1000);

            Questions.Clear();
            foreach (var x in filteredList)
                Questions.Add(x);
            SelectedIndex = -1;//שדה בחירה יהיה ריק  

        }



        

    }
}
