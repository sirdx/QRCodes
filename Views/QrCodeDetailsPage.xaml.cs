using QRCodes.ViewModels;

namespace QRCodes.Views;

public partial class QrCodeDetailsPage : ContentPage
{
	private readonly QrCodeDetailsViewModel _viewModel;

	public QrCodeDetailsPage(QrCodeDetailsViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}