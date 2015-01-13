using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormSample.Droid;
using FormSample.Views;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace FormSample.Droid
{
	using Android.Text;
	using Android.Views.InputMethods;

	public class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// Control.ImeOptions = ImeAction.Next;
				Control.InputType = InputTypes.TextFlagNoSuggestions | InputTypes.TextVariationVisiblePassword;
				//Control.SetBackgroundColor(global::Android.Graphics.Color.DarkGray);
			}
		}
	}
}

