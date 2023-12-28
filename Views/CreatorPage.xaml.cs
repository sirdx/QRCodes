using QRCodes.ViewModels;

namespace QRCodes.Views;

public partial class CreatorPage : ContentPage
{
    private readonly CreatorViewModel _viewModel;

    public CreatorPage(CreatorViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}
