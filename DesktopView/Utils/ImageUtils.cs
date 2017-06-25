using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopView.Utils
{
	/// <summary>A collection of extension methods relating to manipulation/creation of images and icons.</summary>
	internal static class ImageUtils
	{
		#region Extension Methods

		/// <summary>Converts an <see cref="byte[]"/> array to <see cref="ImageSource"/>.</summary>
		/// <param name="imageData">The image data.</param>
		/// <returns>The image.</returns>
		public static ImageSource ByteToImageSource(this byte[] imageData)
		{
			BitmapImage biImg = new BitmapImage();
			MemoryStream ms = new MemoryStream(imageData);
			biImg.BeginInit();
			biImg.StreamSource = ms;
			biImg.EndInit();

			return biImg;
		}

		/// <summary>Converts an <see cref="Icon"/> to <see cref="ImageSource"/>.</summary>
		/// <param name="icon">The icon.</param>
		/// <returns>The image.</returns>
		public static ImageSource ToImageSource(this Icon icon)
		{
			if (icon == null)
				throw new ArgumentNullException("icon");

			ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
				icon.Handle,
				Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());

			return imageSource;
		}

		/// <summary>Converts <see cref="Bitmap"/> to an instance of <see cref="BitmapSource"/>.</summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <returns>The image.</returns>
		public static BitmapSource ToBitmapSource(this Bitmap bitmap)
		{
			if (bitmap == null)
				throw new ArgumentNullException("bitmap");

			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

			BitmapData bitmapData = bitmap.LockBits(
				rect,
				ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			BitmapSource bitmapSource;

			try
			{
				int size = (rect.Width * rect.Height) * 4;

				bitmapSource = BitmapSource.Create(
					bitmap.Width,
					bitmap.Height,
					bitmap.HorizontalResolution,
					bitmap.VerticalResolution,
					PixelFormats.Bgra32,
					null,
					bitmapData.Scan0,
					size,
					bitmapData.Stride);
			}
			finally
			{
				bitmap.UnlockBits(bitmapData);
			}

			return bitmapSource;
		}

		#endregion Extension Methods
	}
}