using System.Windows.Controls;
using System.Windows.Input;
using API.Context;
using DesktopView.ViewModels;

namespace DesktopView.WPF
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow
	{
		private TweetWindow tweetWindow;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnTweetDoubleClick(object sender, MouseButtonEventArgs e)
		{
			tweetWindow?.Close();
			TweetContext tweetContext = ((ListViewItem) sender).Content as TweetContext;
			TweetViewModel viewModel = new TweetViewModel { Tweet = tweetContext };
			tweetWindow = new TweetWindow { DataContext = viewModel };
			tweetWindow.Show();
		}
	}
}
