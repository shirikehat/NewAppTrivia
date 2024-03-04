using NewAppTrivia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppTrivia.ViewModels
{
    public class UserAdminPageViewModel : ViewModel
    {
        private TriviaServices service;
        public UserAdminPageViewModel(TriviaServices service)
        {
            this.service = service;
            //המשך הקוד הושמט לטובת בהירות
        }
    }
}
