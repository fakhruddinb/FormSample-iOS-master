using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;
using FormSample.Droid;


[assembly: ExportRenderer(typeof(Button), typeof(ButtonFixRenderer))]
namespace FormSample.Droid
{
	public class ButtonFixRenderer : ButtonRenderer
	{
		public override void ChildDrawableStateChanged(View child)
		{
			base.ChildDrawableStateChanged(child);
			Control.Text = Control.Text;
		}
	}
}

