using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI txt_Header;
    [SerializeField] TextMeshProUGUI txt_Papcoin;
    [SerializeField] TextMeshProUGUI txt_Gem;
    
    [Header("Button")]
    [SerializeField] Button btn_Play;
    [SerializeField] Button btn_Settings;
    //[SerializeField] Button btn_Store;
    //[SerializeField] Button btn_RateUs;
    [SerializeField] Button btn_UserProfile;
    [SerializeField] Button btn_SendQuestion;
    [SerializeField] Button btn_ApprovePendingQuestion;


    private void OnEnable()
    {
        OnClickAddListener();
      //  LoadData();
    }

    private void OnClickAddListener() 
    {
        btn_Play.onClick.AddListener(QuickGame);
        btn_Settings.onClick.AddListener(UIManager.Instance.ShowSettingsPanel);
        //btn_Menu_Store.onClick.AddListener(UIManager.Instance.ShowStorePanel);
        btn_UserProfile.onClick.AddListener(UIManager.Instance.ShowUserProfilePanel);
        //btn_Menu_User.onClick.AddListener(() => ActionManager.Instance.CallCurrentUserProfile());
        //btn_Menu_RateUs.onClick.AddListener(RateUs);
        btn_SendQuestion.onClick.AddListener(UIManager.Instance.ShowSendQuestionPanel);
        btn_ApprovePendingQuestion.onClick.AddListener(UIManager.Instance.ShowAprrovePendingQuestionsPanel);
    }

    private void LoadData() 
    {
        txt_Papcoin.SetText(CurrentUserProfileKeeper.Papcoin.ToString());
        txt_Gem.SetText(CurrentUserProfileKeeper.Gem.ToString());
    }

    private void QuickGame() 
    {
        Debug.Log("hiraishin");
        ActionManager.Instance.QuickGame();
    }

    private void RateUs() { }

    private void GetRank() { }
}
