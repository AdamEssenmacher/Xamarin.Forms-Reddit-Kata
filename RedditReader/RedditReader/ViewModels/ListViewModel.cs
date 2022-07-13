using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedditReader.Services;
using RedditReader.Views;

namespace RedditReader.ViewModels
{
    public partial class ListViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        [ObservableProperty] private ObservableCollection<ItemViewModel> _items = new();

        [ObservableProperty] private ItemViewModel? _selectedItem;

        public ListViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        private bool _firstAppearing = true;
        public async void Appearing()
        {
            if (_firstAppearing)
                _firstAppearing = false;
            else
                return;

            // async void operation wrapped in top-level try/catch to avoid unobserved task exceptions crashing app
            try
            {
                IList<ItemViewModel> items = await _dataService.GetItemsAsync();

                Items = new ObservableCollection<ItemViewModel>(items);
            }
            catch
            {
                // TODO: Retry/log
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        private async Task ViewAsync()
        {
            if (SelectedItem == null)
                return;

            ItemViewModel item = SelectedItem;
            SelectedItem = null;

            // TODO: ViewModel -> ViewModel or URI navigation would be more ideal.
            await App.PushNavigationAsync(new DetailPage(item));
        }
    }
}