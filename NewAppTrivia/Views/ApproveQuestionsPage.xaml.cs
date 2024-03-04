using NewAppTrivia.ViewModels;

namespace NewAppTrivia.Views;

public partial class ApproveQuestionsPage : ContentPage
{
	public ApproveQuestionsPage(ApproveQuestionsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;

    }
}