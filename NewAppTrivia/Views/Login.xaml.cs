using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}