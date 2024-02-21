using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    
    public static GameManager m_Instance = null;
    [SerializeField]
    private TextMeshProUGUI m_CoinText;
    PlayerScript script;
    
    [SerializeField]
    private TextMeshProUGUI m_StageNumText;
    
    [SerializeField]
    private GameObject m_NextStagePanel;

    [SerializeField]
    private GameObject m_RetryPanel;

    [SerializeField]
    private GameObject m_StartPanel;

    [SerializeField]
    private TextMeshProUGUI m_StagePanelText;

    [SerializeField]
    private TextMeshProUGUI m_HPButtonText;

    [SerializeField]
    private TextMeshProUGUI m_RecButtonText;

    [SerializeField]
    private TextMeshProUGUI m_AtkButtonText;

    [SerializeField]
    private TextMeshProUGUI m_LifeCountText;

    [SerializeField]
    private GameObject m_SettingPanel;

    [SerializeField]
    private GameObject m_SoundUI;

    [SerializeField]
    private GameObject m_ReturnUI;

    private bool m_IsMute = false;

    private int m_HpPrice = 100;
    private int m_RecPrice = 1000;
    private int m_AtkPrice = 500;

    void Awake()
    {
        if(null == m_Instance)
        {
            m_Instance = this;
        }
    }


    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    //private string _adUnitId = "ca-app-pub-1801559800737932/6096023497";
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";//test
    //#elif UNITY_IPHONE
    //  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string _adUnitId = "unused";
#endif

    private RewardedAd _rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " +
                    //               "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : "
                //          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                script.m_Score += 3000;
            });
        }
    }

    void Start()
    {
        //Ad
        LoadRewardedAd();

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });


        script = GameObject.Find("Player").GetComponent<PlayerScript>();
        m_HPButtonText.text = "HP+ \n" + m_HpPrice;
        m_RecButtonText.text = "Rec+ \n" + m_RecPrice;
        m_AtkButtonText.text = "Atk+ \n" + m_AtkPrice;
        m_LifeCountText.text = "x " + script.m_LifeCount;
        m_StartPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ShowCoin();
        NextStage();
        if (m_RecPrice <= script.m_Score)
            GameObject.Find("RecoveryButton").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("RecoveryButton").GetComponent<Button>().interactable = false;

        if(m_HpPrice <= script.m_Score)
            GameObject.Find("HPButton").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("HPButton").GetComponent<Button>().interactable = false;

        if(m_AtkPrice <= script.m_Score)
            GameObject.Find("AttackButton").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("AttackButton").GetComponent<Button>().interactable = false;

        m_LifeCountText.text = "x " + script.m_LifeCount;
    }

    public void ShowCoin()
    {
        m_CoinText.text = "Coin : " + script.m_Score;
    }

    public void StageClear()
    {

        Time.timeScale = 0f;
    }
    
    public void NextStage()
    {
        m_StageNumText.text = "Stage " + script.m_stageNum;
    }

    public void ShowNextStagePanel(bool showpanel)
    {
        m_StagePanelText.text = "Stage " + script.m_stageNum;
        m_NextStagePanel.SetActive(showpanel);
    }

    public void ShowRetryPanel(bool show)
    {
        m_RetryPanel.SetActive(show);
    }

    public void HPUp()
    {
        if(m_HpPrice <= script.m_Score)
        {
            m_HpPrice += 100;
            m_HPButtonText.text = "HP+ \n" + m_HpPrice;
            script.m_Score -= 100;
            script.m_HP += 100;
            script.m_MaxHP += 100;
        }
    }

    public void RecoveryUp()
    {
        if (m_RecPrice <= script.m_Score)
        {
            m_RecPrice += 1000;
            m_RecButtonText.text = "Rec+ \n" + m_RecPrice;
            script.m_Score -= 1000;
            script.m_Recovery += 0.0001f;
        }
    }

    public void AttackUp()
    {
        if (m_AtkPrice <= script.m_Score)
        {
            m_AtkPrice += 500;
            m_AtkButtonText.text = "Atk+ \n" + m_AtkPrice;
            script.m_Score -= 500;
            script.m_Attack += 1;
        }
    }

    public void Retry()
    {
        GameManager.m_Instance.ShowRetryPanel(false);
        script.m_Speed = 2f;
        script.m_LifeCount = 3;
        m_HpPrice = 100;
        m_HPButtonText.text = "HP+ \n" + m_HpPrice;
        m_RecPrice = 1000;
        m_RecButtonText.text = "Rec+ \n" + m_RecPrice;
        m_AtkPrice = 500;
        m_AtkButtonText.text = "Atk+ \n" + m_AtkPrice;
        //GameObject.Find("Level").GetComponent<LevelScript>().SpawnMonster();
        //GameManager.m_Instance.GetComponent<AudioSource>().Play();
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            ShowRewardedAd();
            LoadRewardedAd();
            GameManager.m_Instance.ShowRetryPanel(false);
            script.m_Speed = 2f;
            script.m_LifeCount = 3;
            m_HpPrice = 100;
            m_HPButtonText.text = "HP+ \n" + m_HpPrice;
            m_RecPrice = 1000;
            m_RecButtonText.text = "Rec+ \n" + m_RecPrice;
            m_AtkPrice = 500;
            m_AtkButtonText.text = "Atk+ \n" + m_AtkPrice;
            GameObject.Find("Level").GetComponent<LevelScript>().SpawnMonster();
            GameManager.m_Instance.GetComponent<AudioSource>().Play();
        }

    }

    public void StartGame()
    {
        script.m_gameStart = true;
        GameObject.Find("Level").GetComponent<LevelScript>().SpawnMonster();
        m_StartPanel.SetActive(false);
        m_NextStagePanel.SetActive(true);
    }

    public void ShowSetting()
    {
        m_SettingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToGame()
    {
        m_SettingPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MuteSound()
    {
        if(m_IsMute)
        {
            m_IsMute = false;
            this.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            m_IsMute = true;
            ColorBlock colorblock = m_SoundUI.GetComponent<Button>().colors;
            colorblock.normalColor = new Color(0f, 0f, 0f,1f);
            m_SoundUI.GetComponent<Button>().colors = colorblock;
            this.GetComponent<AudioSource>().mute = true;
        }
    }

    
}
