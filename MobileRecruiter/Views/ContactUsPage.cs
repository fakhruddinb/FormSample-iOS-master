﻿using FormSample.Helpers;
using System;
using MobileRecruiter;

namespace FormSample.Views
{
	using Xamarin.Forms;
	using FormSample;

	public class ContactUsPage : ContentPage
	{
		Image phoneNumberImage,agencyImage,contactMapImage,googleImage,linkedinImage;
	
		public ContactUsPage()
		{
			ToolbarItems.Add(new ToolbarItem("logo","logo_72x72.png",()=>
				{
					DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.CHURCHILKNIGHTURL);},
				ToolbarItemOrder.Primary
				,0));

//			double width = 350;
//			double height = 150;
			double width= Utility.DEVICEWIDTH;
			double height = (Utility.DEVICEHEIGHT*21)/ 100;
			double widthGoogleandLinkedIn = (Utility.DEVICEWIDTH*50)/100;

			phoneNumberImage = new Image (){
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			agencyImage = new Image (){
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			contactMapImage = new Image (){ 
				WidthRequest = width,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			googleImage = new Image (){ 
				WidthRequest = widthGoogleandLinkedIn,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			linkedinImage = new Image (){
				WidthRequest = widthGoogleandLinkedIn,
				HeightRequest = height,
				Aspect = Aspect.AspectFill
			};

			var layout = this.AssignValues ();
			this.Content = layout;
		}

		public StackLayout AssignValues()
		{
			Label lblTitle = new Label{Text = "Contact us",
				BackgroundColor = Color.FromHex("#000000"),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				Font = StyleConstant.GlobalFont
			};

			Label label = new Label() { Text = "To speak with a member of our dedicated team:",Font= StyleConstant.GenerelLabelAndButtonText };

			var grid = new Grid
			{
				RowSpacing = 10,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					//new RowDefinition { Height = GridLength.Auto },
					//new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
				}
				};

			var gridGoogleAndLinkedIn = new Grid
			{ 
				RowSpacing = 10,
				RowDefinitions =
				{

					new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
				},
				ColumnDefinitions=
				{
					new ColumnDefinition{Width = new GridLength(1,GridUnitType.Star)},
					new ColumnDefinition{Width = new GridLength(1,GridUnitType.Star)},
				}
			};

			Button callPhoneNo = new Button
			{
				Text = Utility.PHONENO,
				TextColor = Color.Black,
				BackgroundColor = new Color(255, 255, 255, 0.5),// Color.Transparent,
				VerticalOptions = LayoutOptions.End,
				Font= StyleConstant.GenerelLabelAndButtonText
			};

			callPhoneNo.Clicked += delegate {
				DependencyService.Get<FormSample.Helpers.Utility.IDeviceService>().Call(Utility.PHONENO);
			};

			Button agencyEmail = new Button{Text= Utility.EMAIL,TextColor = Color.Black,BackgroundColor = new Color(255, 255, 255, 0.5),
				VerticalOptions = LayoutOptions.End,Font= StyleConstant.GenerelLabelAndButtonText};

			agencyEmail.Clicked += delegate {
				DependencyService.Get<FormSample.Helpers.Utility.IEmailService>().OpenEmail(Utility.EMAIL);
			};

			Button mapText = new Button{Text="Map:EN6 1AG",TextColor = Color.Black,BackgroundColor = new Color(255, 255, 255, 0.5),
				VerticalOptions = LayoutOptions.End,Font= StyleConstant.GenerelLabelAndButtonText};

			mapText.Clicked += delegate {
				DependencyService.Get<FormSample.Helpers.Utility.IMapService>().OpenMap();
			};

			Button googleText = new Button {Text = "Follow us on Google+", TextColor = Color.Black, BackgroundColor = new Color (255, 255, 255, 0.5),
				VerticalOptions = LayoutOptions.End,
				Font= StyleConstant.GenerelLabelAndButtonText
			};

			googleText.Clicked+= delegate {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.GOOGLEPLUSURL);
			};

			Button linkdinText = new Button {Text = "Follow us on Linkedin", TextColor = Color.Black, BackgroundColor = new Color (255, 255, 255, 0.5),
				VerticalOptions = LayoutOptions.End,
				Font= StyleConstant.GenerelLabelAndButtonText
			};

			linkdinText.Clicked += delegate {
				DependencyService.Get<FormSample.Helpers.Utility.IUrlService>().OpenUrl(Utility.LINKEDINURL);
			};

			grid.Children.Add (phoneNumberImage, 0, 0);
			grid.Children.Add (callPhoneNo, 0, 0);
			grid.Children.Add (agencyImage, 0, 1);
			grid.Children.Add (agencyEmail, 0, 1);
			grid.Children.Add (contactMapImage, 0, 2);
			grid.Children.Add (mapText, 0, 2);
//			grid.Children.Add (googleImage, 0, 3);
//			grid.Children.Add (googleText, 0, 3);

			gridGoogleAndLinkedIn.Children.Add (googleImage, 0, 0);
			gridGoogleAndLinkedIn.Children.Add (googleText, 0, 0);

			gridGoogleAndLinkedIn.Children.Add (linkedinImage, 1, 0);
			gridGoogleAndLinkedIn.Children.Add (linkdinText, 1, 0);

			var labelStakeLayout = new StackLayout ()
			{
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0),
				Children = {lblTitle},
				Orientation = StackOrientation.Vertical

			};

			var labelBeforeGridLayout = new StackLayout (){ 
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {label}
			};

			var gridLayout = new StackLayout () {
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {grid,gridGoogleAndLinkedIn},

			};

			var controlStakeLayout = new StackLayout (){
				Padding = new Thickness(Device.OnPlatform(5, 5, 5),0 , Device.OnPlatform(5, 5, 5), 0), //new Thickness(5,0,5,0),
				VerticalOptions = LayoutOptions.FillAndExpand, 
				HorizontalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Children = {new ScrollView{ Content = gridLayout,IsClippedToBounds = true}},
			};

			var layout = new StackLayout
			{
				Children = { labelStakeLayout,labelBeforeGridLayout,controlStakeLayout},
				Orientation = StackOrientation.Vertical
			};
			return new StackLayout { Children = {layout}};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			phoneNumberImage.Source = ImageSource.FromResource("MobileRecruiter.Images.ContactPhoneNumber.jpg");
			agencyImage.Source = ImageSource.FromResource("MobileRecruiter.Images.ContactAgency.jpg");
			contactMapImage.Source = ImageSource.FromResource("MobileRecruiter.Images.ContactMap.jpg");
			googleImage.Source = ImageSource.FromResource("MobileRecruiter.Images.Google.png");
			linkedinImage.Source = ImageSource.FromResource("MobileRecruiter.Images.LinkedIn.png");
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing ();
			phoneNumberImage.Source = null;
			agencyImage.Source = null;
			contactMapImage.Source = null;
			googleImage.Source = null;
			linkedinImage.Source = null;
			GC.Collect ();
		}
	}
}
