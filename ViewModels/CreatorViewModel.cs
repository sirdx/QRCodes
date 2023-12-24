using CommunityToolkit.Mvvm.ComponentModel;
using QRCodes.Data;

namespace QRCodes.ViewModels;

public partial class CreatorViewModel : ObservableObject
{
    private readonly DatabaseContext _databaseContext;

    public CreatorViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
}