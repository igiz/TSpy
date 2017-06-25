using System;

namespace Data.Logging
{
	public static class ConsoleExtensions
	{
		#region Private Types

		private class ConsoleForegroundColor : IDisposable
		{
			private ConsoleColor original;

			public ConsoleForegroundColor(ConsoleColor color)
			{
				Console.ForegroundColor = color;
			}

			public void Dispose()
			{
				Console.ForegroundColor = original;
			}
		}

		#endregion

		/// <summary>Writes a line to Console with a given foreground color.</summary>
		/// <param name="text">The text to write.</param>
		/// <param name="color">The color.</param>
		public static void WriteLine(string text, ConsoleColor color)
		{
			using (new ConsoleForegroundColor(color)) {
				Console.WriteLine(text);
			}
		}
	}
}
