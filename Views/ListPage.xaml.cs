using QRCodes.ViewModels;

namespace QRCodes.Views;

public partial class ListPage : ContentPage
{
    private readonly ListViewModel _viewModel;

    public ListPage(ListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadQrCodesAsync();
    }
}