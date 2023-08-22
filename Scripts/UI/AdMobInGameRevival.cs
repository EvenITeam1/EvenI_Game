using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdMobInGameRevival : MonoBehaviour
{
    private RewardedAd rewardedAd;
    [SerializeField] GameObject BeforeAdPanel;
    [SerializeField] GameObject AdFailedPanel;
    [SerializeField] Button AdRevivalButton;
    public UnityEvent AdRewardEvent;

    const string adUnitId = "ca-app-pub-7368363947894477/4246739561";

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //???? ???
        });

        LoadRewardedAd();
    }

    public void LoadRewardedAd() //???? ?ех? ???
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

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
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

    public void NoAdCheck()
    {
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.IsNoAdActivated)
            GetReward();
        else
            BeforeAdPanel.SetActive(true);
    }

    public void ShowAd() //???? ????
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                GetReward();
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            AdFailedPanel.SetActive(true);
            LoadRewardedAd();
        }
    }

    public void GetReward()
    {
        AdRevivalButton.interactable = false;
        AdRewardEvent.Invoke();
    }

    private void RegisterReloadHandler(RewardedAd ad) //???? ??ех?
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }
}