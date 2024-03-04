using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class UserAdminPage : ContentPage
{
	public UserAdminPage(UserAdminPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;

    }
}