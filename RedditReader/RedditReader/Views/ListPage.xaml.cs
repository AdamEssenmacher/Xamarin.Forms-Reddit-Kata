using RedditReader.ViewModels;

namespace RedditReader.Views
{
    public partial class ListPage
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<ListViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = (ListViewModel) BindingContext;
            viewModel.Appearing();
        }
    }
}