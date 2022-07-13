using CommunityToolkit.Mvvm.ComponentModel;

namespace RedditReader.ViewModels
{
    public partial class ItemViewModel : ObservableObject
    {
        [ObservableProperty] private string? _title = string.Empty;

        [ObservableProperty] private string? _selfText = string.Empty;

        [ObservableProperty] private string? _thumbnail;

        [ObservableProperty] private string? _image;
    }
}