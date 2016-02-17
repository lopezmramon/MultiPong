using UnityEngine;
using System.Collections;

public class AdvertisingScript : MonoBehaviour {

	void Start () {
        AdBuddizBinding.SetAndroidPublisherKey("9a631856-3099-4429-bbb3-1dc599223ca7");
        AdBuddizBinding.CacheAds();
        AdBuddizBinding.RewardedVideo.Fetch();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentState == GameManager.GameState.Victory |
            GameManager.instance.currentState == GameManager.GameState.Defeat) {
            
        }
	}

    public void ShowInterstitialAd()
    {
        if(AdBuddizBinding.IsReadyToShowAd())
        AdBuddizBinding.ShowAd();
    }
    public void ShowVideoAd()
    {
        if (AdBuddizBinding.RewardedVideo.IsReadyToShow())
        {
            AdBuddizBinding.RewardedVideo.Show();
        }
    }
}
