using NewAppTrivia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppTrivia.ViewModels
{
    public class ApproveQuestionsPageViewModel :ViewModel
    {
        private TriviaServices service;
        public ApproveQuestionsPageViewModel(TriviaServices service)
        {
            this.service = service;
            //המשך הקוד הושמט לטובת בהירות
        }

    }
}
