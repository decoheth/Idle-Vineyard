using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    RewardedAd rewardedAd;
    public string _adUnitId;
    public GameObject GM;
    public GameObject SM;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
        
        // Load Saved Data
        SaveData data = SM.GetComponent<SaveManager>().LoadGame();
        // Create afk popup once ads initialized
        DateTime savedExitTime = JsonUtility.FromJson<JsonDateTime>(data.savedExitTime);
        GM.GetComponent<GameManager>().AfkGold(savedExitTime);
    }

  public void LoadRewardedAd()
  {
      // Clean up the old ad before loading a new one.
      if (rewardedAd != null)
      {
            rewardedAd.Destroy();
            rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();
      adRequest.Keywords.Add("unity-admob-sample");

      // send the request to load the ad.
      RewardedAd.Load(_adUnitId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              rewardedAd = ad;
          });
  }


    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // Reward the user.
                GM.GetComponent<GameManager>().ClaimBoostedAfkGold();
            });
        }
    }



private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                        "with error : " + error);
        };
    }


    // Serialize DateTime
    [Serializable]
    struct JsonDateTime {
        public long value;
        public static implicit operator DateTime(JsonDateTime jdt) {
            return DateTime.FromFileTime(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt) {
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTime();
            return jdt;
        }
    }
}