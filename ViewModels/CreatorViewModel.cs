using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCodes.Data;
using QRCodes.Models;
using QRCodes.Utils;

namespace QRCodes.ViewModels;

public partial class CreatorViewModel : ObservableObject
{
    private readonly DatabaseContext _databaseContext;

    [ObservableProperty]
    private string _formContent = string.Empty;

    public CreatorViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    private QrCode? CreateQrCode()
    {
        if (string.IsNullOrWhiteSpace(FormContent))
        {
            return null;
        }

        return new QrCode
        {
            Content = FormContent
        };
    }

    [RelayCommand]
    private async Task SubmitFormAsync()
    {
        var entity = CreateQrCode();

        if (entity is null)
        {
            return;
        }

        await _databaseContext.QrCodes.AddAsync(entity);
        int entries = await _databaseContext.SaveChangesAsync();

        var snackbarMsg = entries == 0 ? "An error occured." : "Submit successful!";
        var snackbar = Snackbar.Make(snackbarMsg);
        await snackbar.Show();
    }

    [RelayCommand]
    private async Task PrintAsync()
    {
        var entity = CreateQrCode();

        if (entity is null)
        {
            return;
        }

        await QrCodeShareService.PrintAsync(entity);
    }

    [RelayCommand]
    private async Task SaveToFileAsync()
    {
        var entity = CreateQrCode();

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
