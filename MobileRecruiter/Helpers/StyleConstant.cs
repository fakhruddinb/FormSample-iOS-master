using System;
using FormSample.Helpers;
using Xamarin.Forms;

namespace MobileRecruiter
{
	public class StyleConstant
	{
		public static Font GenerelLabelAndButtonText { 
			get
			{ 
				if (Utility.DEVICEHEIGHT > 1000) {
					return Font.OfSize (Utility.FontName , Utility.GenerelFontSize).WithAttributes(FontAttributes.Bold);
				}
				return Font.OfSize (Utility.FontName, NamedSize.Medium).WithAttributes(FontAttributes.Bold);
			} 
		}

		public static Font GlobalFont { 
			get
			{ 
				if (Utility.DEVICEHEIGHT > 1000) {
					return Font.OfSize (Utility.FontName, Utility.TabletFontSize).WithAttributes(FontAttributes.Bold);
				}
				return Font.OfSize (Utility.FontName, NamedSize.Medium).WithAttributes(FontAttributes.Bold);
			} 
		}

		public static Font ListItemFontStyle { 
			get
			{ 
				if (Utility.DEVICEHEIGHT > 1000) {
					return Font.OfSize (Utility.FontName, Utility.GenerelFontSize);
				}
				return Font.OfSize (Utility.FontName, NamedSize.Medium);
			} 
		}

	}
}

