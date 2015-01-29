using System;
using FormSample.Helpers;
using Xamarin.Forms;

namespace MobileRecruiter
{
	public class StyleConstant
	{
		public static Font HomePageButtonsText { 
			get
			{ 
				if (Utility.DEVICEHEIGHT > 1000) {
					return Font.OfSize ("Roboto", 25).WithAttributes(FontAttributes.Bold);
				}
				return Font.OfSize ("Roboto", NamedSize.Medium).WithAttributes(FontAttributes.Bold);
			} 
		}

		public static Font GlobalFont { 
			get
			{ 
				if (Utility.DEVICEHEIGHT > 1000) {
					return Font.OfSize ("Roboto", 30).WithAttributes(FontAttributes.Bold);
				}
				return Font.OfSize ("Roboto", NamedSize.Medium).WithAttributes(FontAttributes.Bold);
			} 
		}
	}
}

