namespace Gegueb {

	using System;
	using Egueb.Dom;

	public class Application {

		//
		// Disables creation of instances.
		//
		private Application ()
		{
		}

		[DllImport("libglib-2.0-0.dll")]
		static extern void g_main_loop_run ();

		[DllImport("libglib-2.0-0.dll")]
		static extern void g_main_loop_quit ();

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
			g_main_loop_run(loop);
		}

		public static void Quit ()
		{
			g_main_loop_quit(loop);
		}

		public Window New (Egueb.Dom.Document doc, int x, int y, int w, int h)
		{
			IntPtr wPtr = gegueb_window_new (doc.Raw, x, y, w, h);
			if (wPtr != null)
			{
				Window win = new Window (wPtr);
				return win;
			}

			return null;
		}
	}
}
