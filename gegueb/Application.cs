namespace Gegueb {

	using System;
	using System.Runtime.InteropServices;
	using GLib;
	using Egueb.Dom;

	public class Application {

		private static MainLoop loop = new MainLoop();
		//
		// Disables creation of instances.
		//
		private Application ()
		{
		}

		[DllImport("libgegueb.dll")]
		static extern void gegueb_init ();

		[DllImport("libgegueb.dll")]
		static extern void gegueb_shutdown ();

		[DllImport("libgegueb.dll")]
		static extern IntPtr gegueb_window_new(IntPtr doc, int x, int y, int w, int h);

		public static void Init ()
		{
			gegueb_init();
		}

		public static void Run ()
		{
			loop.Run();
		}

		public static void Quit ()
		{
			loop.Quit();
		}

		public Window New (Egueb.Dom.Document doc, int x, int y, int w, int h)
		{
			IntPtr wPtr = gegueb_window_new (doc.Raw, x, y, w, h);
			if (wPtr != null)
			{
				Window win = new Window (wPtr, false);
				return win;
			}

			return null;
		}
	}
}
