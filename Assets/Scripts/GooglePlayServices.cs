using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class GooglePlayServices : MonoBehaviour {
    public static GooglePlayServices instance;
    PlayGamesClientConfiguration config;
    void Start() {
        instance = this;
        config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.Activate();

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success) { }
                else { }

            });
        }
	}
	
	public void SignIn()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success) { }
                else { }

            });
        }
    }

    public void IncrementalAchievement(string achievementID, int incrementAmount)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        achievementID, incrementAmount, (bool success) => {
        });
    }

    public void UnlockableAchievement(string achievementID,float progress)
    {
        Social.ReportProgress(achievementID, progress, (bool success) => {
            // handle success or failure
        });
    }

    public void LeaderboardScore(string leaderboardID, int score)
    {
        Social.ReportScore(score, leaderboardID, (bool success) =>
        {
        });
    }
    public void IncreaseEvent(string eventID,uint amount)
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(eventID, amount);
    }

    public void ShowAchievementUI()
    {

        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboardUI()
    {

        Social.ShowLeaderboardUI();
    }
}
