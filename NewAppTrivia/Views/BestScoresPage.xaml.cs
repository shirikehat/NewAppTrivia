using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class BestScoresPage : ContentPage
{
	public BestScoresPage(BestScoresPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;

    }
}