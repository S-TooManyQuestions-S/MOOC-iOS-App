using System;
using System.Linq;
using Foundation;
using MOOC.DataLibrary;
using UIKit;
using Xamarin.Essentials;

namespace MOOC.JSONOptions
{
    public class BackGround
    {
        public bool IsWifiConnected()
        {
            var profiles = Connectivity.ConnectionProfiles;
            return profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Ethernet);
        }

        public BackGround()
        {
            //Выполнение задач на бэкграунде при закрытии приложения (ТОЛЬКО ЕСЛИ ЕСТЬ ИНТЕРНЕТ)
            NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidEnterBackgroundNotification,
            (NSNotification noti) => {
                if(IsWifiConnected())
                JsonMethods.JsonWriter(GetInformation.important);
            });
        }
    }
}
