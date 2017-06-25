using API.Context;
using DesktopView.Resources;
using DesktopView.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DesktopView.Commands;

namespace DesktopView.ViewModels
{
	/// <summary>ViewModel for displaying Tweet details.</summary>
	internal class TweetViewModel : ViewModelBase
	{
		#region Non-Public Fields

		/// <summary>The tweet.</summary>
		private TweetContext tweet;

		/// <summary>The user profile image.</summary>
		private ImageSource userImage;

		/// <summary>The user profile banner image.</summary>
		private ImageSource userBanner;

		#endregion Non-Public Fields

		#region Public Properties

		/// <summary>Gets the Close command.</summary>
		public ICommand CloseCommand { get; }

		/// <summary>Gets the user profile banner image.</summary>
		public ImageSource UserBanner
		{
			get{
				if (userBanner == null) {
					userBanner = UIResources.No_user_banner.ToBitmapSource();
				}
				return userBanner;
			}

			private set{
				if (userBanner != value) {
					userBanner = value;
					OnPropertyChanged(() => UserBanner);
				}
			}
		}

		/// <summary>Gets the user profile image.</summary>
		public ImageSource UserImage
		{
			get {
				if (userImage == null) {
					UserImage = UIResources.No_user_image.ToBitmapSource();
				}
				return userImage;
			}

			private set {
				if (userImage != value) {
					userImage = value;
					OnPropertyChanged(() => UserImage);
				}
			}
		}

		/// <summary>Gets or sets the tweet context.</summary>
		public TweetContext Tweet
		{
			get { return tweet; }
			set
			{
				if (tweet != value)
				{
					tweet = value;
					RetrieveImages();
					OnPropertyChanged(() => Tweet);
				}
			}
		}

		#endregion Public Properties

		#region Public Constructors

		/// <summary>The main constructor.</summary>
		public TweetViewModel()
		{
			CloseCommand = new RelayCommand<Window>(window => window.Close(), canExecute => true);
		}

		#endregion

		#region Non-Public Methods

		/// <summary>Asynchronously downloads the required user images from twitter.</summary>
		private async void RetrieveImages()
		{
			//Todo: Needs to be tidied up , currently a bit messy.

			List<Task<byte[]>> tasks = new List<Task<byte[]>>(); 

			bool profileImageAvailable = !string.IsNullOrEmpty(tweet.User.ProfileImageUrl);
			bool profileBannerAvailable = !string.IsNullOrEmpty(tweet.User.ProfileBackgroundImageUrl);

			if (profileImageAvailable)
				tasks.Add(RetrieveImageTask(tweet.User.ProfileImageUrl));

			if (profileBannerAvailable)
				tasks.Add(RetrieveImageTask(tweet.User.ProfileBackgroundImageUrl));

			await Task.WhenAll(tasks);
			
			if (profileImageAvailable){
				byte[] userImageBytes = await tasks[0];
				UserImage = userImageBytes.ByteToImageSource();
			}

			if (profileBannerAvailable){
				byte[] userProfileBannerBytes = await tasks[1];
				UserBanner = userProfileBannerBytes.ByteToImageSource();
			}
		}

		/// <summary>Creates a retrieve image from web task.</summary>
		/// <param name="imageUrl">The image url.</param>
		/// <returns>The task.</returns>
		private Task<byte[]> RetrieveImageTask(string imageUrl)
		{
			return Task.Run(() =>
			{
				Uri imageUri = new Uri(imageUrl);
				byte[] result = new WebClient().DownloadData(imageUri);
				return result;
			});
		}

		#endregion Non-Public Methods
	}
}