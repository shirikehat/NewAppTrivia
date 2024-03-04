using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class Login : ContentPage
{
	public Login(Login vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}