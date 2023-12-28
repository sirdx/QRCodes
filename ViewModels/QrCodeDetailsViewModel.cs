using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCodes.Data;
using QRCodes.Models;
using QRCodes.Utils;

namespace QRCodes.ViewModels;

public partial class QrCodeDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly DatabaseContext _databaseContext;
    private QrCode? _qrCode = null;

    [ObservableProperty]
    private string _formContent = string.Empty;

    public QrCodeDetailsViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _qrCode = query[nameof(QrCode)] as QrCode;
       
        if (_qrCode is null)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        FormContent = _qrCode.Content;
    }

    private QrCode? UpdateQrCode()
    {
        if (string.IsNullOrWhiteSpace(FormContent))
        {
            return null;
        }

        if (_qrCode is null)
        {
            return null;
        }

        _qrCode.Content = FormContent;
        return _qrCode;
    }

    [RelayCommand]
    private async Task SubmitFormAsync()
    {
        var entity = UpdateQrCode();

        if (entity is null)
        {
            return;
        }

        _databaseContext.QrCodes.Update(entity);
        await _databaseContext.SaveChangesAsync();

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (_qrCode is null)
        {
            return;
        }

        _databaseContext.QrCodes.Remove(_qrCode);
        await _databaseContext.SaveChangesAsync();

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        var entity = UpdateQrCode();

        if (entity is null)
        {
            return;
        }

        await QrCodeShareService.PrintAsync(entity);
    }

    [RelayCommand]
    private async Task SaveToFileAsync()
    {
        var entity = UpdateQrCode();

        if (entity is null)
        {
            return;
        }

        string? path = QrCodeShareService.SaveToGallery(entity);
        var snackbarMsg = path is null ? "An error occured." : "Saved to gallery";
        var snackbar = Snackbar.Make(snackbarMsg);
        await snackbar.Show();
    }
}
