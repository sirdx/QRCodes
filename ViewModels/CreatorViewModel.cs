using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCodes.Data;
using QRCodes.Models;

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

    [RelayCommand]
    private async Task SubmitForm()
    {
        if (string.IsNullOrWhiteSpace(FormContent))
        {
            return;
        }

        var entity = new QrCode
        { 
            Content = FormContent
        };

        await _databaseContext.QrCodes.AddAsync(entity);
        int entries = await _databaseContext.SaveChangesAsync();

        var snackbarMsg = entries == 0 ? "An error occured." : "Submit successful!";
        var snackbar = Snackbar.Make(snackbarMsg);
        await snackbar.Show();
    }
}