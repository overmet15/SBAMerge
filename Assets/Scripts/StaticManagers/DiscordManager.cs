using System;
using Discord;
using UnityEngine;

public static class DiscordManager
{
#if !UNITY_ANDROID
    public static Discord.Discord discord; 
    private static long startTime;
    public static void Init()
    {
        discord = new Discord.Discord(1235538385595269130, (long)CreateFlags.NoRequireDiscord);
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
    public static void Update()
    {
        discord.RunCallbacks();
    }
#endif
    public static void UpdateActivity(string details, string state, string largeImage = "largedefault", string smallImage = "smalldefault", string largeText = "", string smallText = "")
    {
#if !UNITY_ANDROID
        var activityManager = discord.GetActivityManager();
        var activity = new Activity
        {
            Details = details,
            State = state,
            Assets =
            {
                LargeImage = largeImage,
                LargeText = largeText,
                SmallImage = smallImage,
                SmallText = smallText,
            },
            Timestamps =
            {
                Start = startTime
            }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res != Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
        });
#endif
    }
}
