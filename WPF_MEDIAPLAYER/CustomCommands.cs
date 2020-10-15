using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_MEDIAPLAYER
{
    public static class CustomCommands
    {
		public static readonly RoutedUICommand Play = new RoutedUICommand
			(
				"Play",
				"Play media.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.Space, ModifierKeys.None)
				}
			);

		public static readonly RoutedUICommand Stop = new RoutedUICommand
			(
				"Stop",
				"Stop media.",
				typeof(CustomCommands)
			);

		public static readonly RoutedUICommand Fullscreen = new RoutedUICommand
			(
				"Fullscreen",
				"Enable/disable fullscreen mode.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.F12, ModifierKeys.None)
				}
			);

		public static readonly RoutedUICommand Open = new RoutedUICommand
			(
				"Open",
				"Select file you wish to be opened.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.O, ModifierKeys.Control)
				}
			);

		public static readonly RoutedUICommand Close = new RoutedUICommand
			(
				"Close",
				"Close current file.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.W, ModifierKeys.Control)
				}
			);

		public static readonly RoutedUICommand Exit = new RoutedUICommand
			(
				"Exit",
				"Close the application.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.Q, ModifierKeys.Control)
				}
			);

		public static readonly RoutedUICommand Help = new RoutedUICommand
			(
				"Help",
				"View help page.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.F1, ModifierKeys.None)
				}
			);
		public static readonly RoutedUICommand VideoClip10Backward = new RoutedUICommand
			(
				"10 seconds forward",
				"Go 10 seconds forward.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.Left, ModifierKeys.None)
				}
			);
		public static readonly RoutedUICommand VideoClip10Forward = new RoutedUICommand
			(
				"10 seconds backward",
				"Go 10 seconds backward.",
				typeof(CustomCommands),
				new InputGestureCollection()
				{
					new KeyGesture(Key.Right, ModifierKeys.None)
				}
			);
	}
}
