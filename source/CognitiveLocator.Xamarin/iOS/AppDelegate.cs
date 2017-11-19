﻿using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Facebook.CoreKit;

namespace CognitiveLocator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static MobileServiceClient MobileClient = null;
     
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //create the client instance, using the mobile app backend URL.
            AppDelegate.MobileClient = new MobileServiceClient(Settings.FunctionURL);

            Profile.EnableUpdatesOnAccessTokenChange(true);
            Facebook.CoreKit.Settings.AppID = Settings.FacebookAppId;
            Facebook.CoreKit.Settings.DisplayName = Settings.FacebookAppName;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            AppEvents.ActivateApp();
        }
    }
}