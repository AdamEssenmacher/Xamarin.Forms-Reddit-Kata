using RedditReader.ViewModels;

namespace RedditReader.Views
{
    public partial class DetailPage
    {
        public DetailPage(ItemViewModel itemViewModel)
        {
            InitializeComponent();
            BindingContext = itemViewModel;
        }
    }
}