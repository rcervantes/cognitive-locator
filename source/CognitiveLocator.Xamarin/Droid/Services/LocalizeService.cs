﻿using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using CognitiveLocator.Droid.Services;
using CognitiveLocator.Interfaces;

[assembly:Dependency(typeof(LocalizeService))]
namespace CognitiveLocator.Droid.Services
{
    public class LocalizeService : ILocalize
    {
        public void SetLocale(string culture)
        {
            CultureInfo ci = new CultureInfo(culture);
            Resx.AppResources.Culture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("CurrentCulture set: " + ci.DisplayName);
        }
    }
}