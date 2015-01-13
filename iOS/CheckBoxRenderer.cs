using System;
using System.ComponentModel;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs.iOS.Controls;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]

namespace MobileRecruiter.iOS
{
    using Xamarin.Forms.Labs.iOS;

    /// <summary>
    /// The check box renderer for iOS.
    /// </summary>
    public class CheckBoxRenderer : ViewRenderer<CheckBox, CheckBoxView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);

            BackgroundColor = Element.BackgroundColor.ToUIColor();

            if (Control == null)
            {
                var checkBox = new CheckBoxView(Bounds);
                checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;

                SetNativeControl(checkBox);
            }

            UpdateFont();


            Control.LineBreakMode = UILineBreakMode.CharacterWrap;
            Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
            Control.CheckedTitle = string.IsNullOrEmpty(e.NewElement.CheckedText) ? e.NewElement.DefaultText : e.NewElement.CheckedText;
            Control.UncheckedTitle = string.IsNullOrEmpty(e.NewElement.UncheckedText) ? e.NewElement.DefaultText : e.NewElement.UncheckedText;
            Control.Checked = e.NewElement.Checked;
            Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Normal);
            Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Selected);
        }

        private void ResizeText()
        {
            var text = this.Element.Checked ? string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText :
                string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;

            var bounds = this.Control.Bounds;

            var width = this.Control.TitleLabel.Bounds.Width;

            var height = text.StringHeight(this.Control.Font, width);

            var minHeight = string.Empty.StringHeight(this.Control.Font, width);

            var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero);

            var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven);

            if (supportedLines != requiredLines)
            {
                bounds.Height += (float)(minHeight * (requiredLines - supportedLines));
                this.Control.Bounds = bounds;
                this.Element.HeightRequest = bounds.Height;
            }
        }

        public override void Draw(System.Drawing.RectangleF rect)
        {
            base.Draw(rect);
            this.ResizeText();
        }

        private void UpdateFont()
        {
            if (string.IsNullOrEmpty(Element.FontName))
            {
                return;
            }

            var font = UIFont.FromName(Element.FontName, (Element.FontSize > 0) ? (float)Element.FontSize : 12.0f);

            if (font != null)
            {
                Control.Font = font;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "TextColor":
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Normal);
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Selected);
                    break;
                case "CheckedText":
                    Control.CheckedTitle = string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText;
                    break;
                case "UncheckedText":
                    Control.UncheckedTitle = string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;
                    break;
                case "FontSize":
                    UpdateFont();
                    break;
                case "FontName":
                    UpdateFont();
                    break;
                case "Element":
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    return;
            }
        }
    }
}
