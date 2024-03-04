using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class UserQuestionsPage : ContentPage
{
	public UserQuestionsPage(UserQuestionsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;

    }
}