using System.Windows;

namespace WpfLayoutsMvvm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new LayoutsViewModel();
            DataContext = viewModel;

            InitializeComponent();

            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var viewModel = (LayoutsViewModel)DataContext;

            viewModel.LayoutName = e.NewSize.Width < 411 ? "Partial" : "Full";
        }
    }
}
