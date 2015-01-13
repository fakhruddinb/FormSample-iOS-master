using System;
using Android.Widget;

namespace FormSample.Droid
{
	using System.ComponentModel;

	using global::Android.Graphics;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.Android;

	using NativeCheckBox = CheckBox;
	[assembly: ExportRenderer(typeof(Xamarin.Forms.Labs.Controls.CheckBox), typeof(CheckBoxRenderer))]
	public class CheckBoxRenderer : ViewRenderer<Xamarin.Forms.Labs.Controls.CheckBox, NativeCheckBox>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Labs.Controls.CheckBox> e)
		{
			base.OnElementChanged(e);

			if (this.Control == null)
			{
				var checkBox = new NativeCheckBox(this.Context);
				checkBox.CheckedChange += checkBox_CheckedChange;

				this.SetNativeControl(checkBox);
			}

			Control.Text = e.NewElement.Text;
			Control.Checked = e.NewElement.Checked;
			Control.SetTextColor(e.NewElement.TextColor.ToAndroid());

			if (e.NewElement.FontSize > 0)
			{
				Control.TextSize = (float)e.NewElement.FontSize;
			}

			if (!string.IsNullOrEmpty(e.NewElement.FontName))
			{
				Control.Typeface = TrySetFont(e.NewElement.FontName);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
			case "Checked":
				Control.Text = Element.Text;
				Control.Checked = Element.Checked;
				break;
			case "TextColor":
				Control.SetTextColor(Element.TextColor.ToAndroid());
				break;
			case "FontName":
				if (!string.IsNullOrEmpty(Element.FontName))
				{
					Control.Typeface = TrySetFont(Element.FontName);
				}
				break;
			case "FontSize":
				if (Element.FontSize > 0)
				{
					Control.TextSize = (float)Element.FontSize;
				}
				break;
			case "CheckedText":
			case "UncheckedText":
				Control.Text = Element.Text;
				break;
			default:
				System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
				break;
			}
		}

		void checkBox_CheckedChange(object sender, Android.Widget.CompoundButton.CheckedChangeEventArgs e)
		{
			this.Element.Checked = e.IsChecked;
		}

		private Typeface TrySetFont(string fontName)
		{
			Typeface tf = Typeface.Default;
			try
			{
				tf = Typeface.CreateFromAsset(Context.Assets, fontName);
				return tf;
			}
			catch (Exception ex)
			{
				Console.Write("not found in assets {0}", ex);
				try
				{
					tf = Typeface.CreateFromFile(fontName);
					return tf;
				}
				catch (Exception ex1)
				{
					Console.Write(ex1);
					return Typeface.Default;
				}
			}
		}

	}

}