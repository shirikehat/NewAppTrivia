using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        this.BindingContext = new LoginViewModel();
    }
}