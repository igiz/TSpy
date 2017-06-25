using System.Collections.Generic;
using API.Interrogation;
using Data.Interrogation;
using Data.Logging;
using DesktopView.ViewModels;
using DesktopView.WPF;
using System.Windows;
using API.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DesktopView
{
	/// <summary>Interaction logic for App.xaml </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			UnityContainer container = new UnityContainer();
			container.LoadConfiguration("DesktopView");
			MainWindowViewModel viewModel = new MainWindowViewModel();
			ILogger logger = new CompositeLogger(new List<ILogger>(container.ResolveAll<ILogger>()) { viewModel });
			IInterrogator interrogator = new StreamingInterrogator(logger);
			viewModel.Interrogator = interrogator;
			MainWindow mainWindow = new MainWindow { DataContext = viewModel };
			mainWindow.Show();
		}
	}
}
