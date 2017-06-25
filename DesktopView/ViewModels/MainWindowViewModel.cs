using API.Context;
using API.Interrogation;
using API.Logging;
using Data.Collections;
using DesktopView.Commands;
using DesktopView.Resources;
using DesktopView.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopView.ViewModels
{
	/// <summary>ViewModel for the Main application window.</summary>
	internal class MainWindowViewModel : ViewModelBase, ILogger
	{
		#region Non-Public Fields

		/// <summary>The search text.</summary>
		private string searchText;

		/// <summary>The connection state.</summary>
		private bool connected;

		/// <summary>The filter setting.</summary>
		private FilterBy filterBy;

		/// <summary>The interrogator.</summary>
		private IInterrogator interrogator;

		/// <summary>The <see cref="LogMessage"/> collection.</summary>
		private readonly ObservableCollection<LogMessage> logMessages;

		#endregion Non-Public Fields

		#region Public Properties

		/// <summary>Gets or sets the current connection state.</summary>
		public bool Connected
		{
			get { return connected; }
			set {
				if (connected != value) {
					connected = value;
					OnPropertyChanged(() => Connected);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="IInterrogator"/> instance.</summary>
		public IInterrogator Interrogator
		{
			get { return interrogator; }
			set {
				if (interrogator != value) {
					interrogator = value;
					interrogator.SetFilter(filter: FilterBy);
					Content = interrogator.Tweets;
					OnPropertyChanged(() => Interrogator);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="FilterBy"/> setting.</summary>
		public FilterBy FilterBy
		{
			get { return filterBy; }
			set {
				if (filterBy != value) {
					filterBy = value;
					interrogator.SetFilter(filterBy);
					OnPropertyChanged(() => FilterBy);
				}
			}
		}

		/// <summary>Gets or sets the keyword search text.</summary>
		public string SearchText
		{
			get { return searchText; }
			set {
				if (searchText != value){
					searchText = value;
					OnPropertyChanged(() => SearchText);
				}
			}
		}

		/// <summary>Gets the application icon.</summary>
		public ImageSource WindowIcon => UIResources.MainIcon.ToImageSource();

		/// <summary>Gets view of the <see cref="LogMessage"/> logs.</summary>
		public ICollectionView LogMessagesView { get; }

		/// <summary>Gets the <see cref="TweetContext"/> content.</summary>
		public ObservableCollection<TweetContext> Content { get; private set; }

		/// <summary>Gets the Search command.</summary>
		public ICommand SearchCommand { get; }

		/// <summary>Gets the Connect/Disconnect command.</summary>
		public ICommand ConnectDisconnectCommand { get; }

		#endregion Public Properties

		#region Public Constructors

		/// <summary>The main constructor.</summary>
		public MainWindowViewModel()
		{
			// Initialize commands
			SearchCommand = new RelayCommand<object>(command => interrogator.Search(SearchText), canExecute => !string.IsNullOrEmpty(SearchText) && Connected);
			ConnectDisconnectCommand = new RelayCommand<object>(command => ConnectDisconnect(), canExecute => true);

			// Setup logger view.
			logMessages = new LimitedObservableCollection<LogMessage>(sizeLimit: 15);
			LogMessagesView = CollectionViewSource.GetDefaultView(logMessages);
			LogMessagesView.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Descending));
		}

		#endregion Public Constructors

		#region Public Methods

		/// <summary>Logs a message to the view.</summary>
		/// <param name="message">The log message.</param>
		public void LogMessage(LogMessage message)
		{
			logMessages.Add(message);
		}

		#endregion

		#region Non-Public Methods

		/// <summary>Connects or disconnects the interrogator depending on the connection state.</summary>
		private void ConnectDisconnect()
		{
			if (interrogator.Connected) {
				interrogator.Disconnect();
			} else {
				interrogator.Connect();
			}

			// We need this backing property to update some binded UI components.
			Connected = interrogator.Connected;
		}

		#endregion Non-Public Methods
	}
}