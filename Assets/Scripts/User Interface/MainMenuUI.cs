using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    //[Header("Text")]
    //[SerializeField] TextMeshProUGUI txt_Header;
    //[SerializeField] TextMeshProUGUI txt_Papcoin;
    //[SerializeField] TextMeshProUGUI txt_Gem;
    
    [Header("Button")]
    [SerializeField] Button button_Play;
    [SerializeField] Button button_Settings;
    //[SerializeField] Button btn_Store;
    //[SerializeField] Button btn_RateUs;
    [SerializeField] Button button_UserProfile;
    [SerializeField] Button button_SendQuestion;
    [SerializeField] Button button_ApprovePendingQuestion;


    private void OnEnable()
    {
        OnClickAddListener();
      //  LoadData();
    }

    private void OnClickAddListener() 
    {
        //button_Play.onClick.AddListener(QuickGame);
        button_Settings.onClick.AddListener(UIManager.Instance.ShowSettingsPanel);
        //btn_Menu_Store.onClick.AddListener(UIManager.Instance.ShowStorePanel);
        button_UserProfile.onClick.AddListener(UserProfile);
        //btn_Menu_RateUs.onClick.AddListener(RateUs);
        button_SendQuestion.onClick.AddListener(UIManager.Instance.ShowSendQuestionPanel);
        button_ApprovePendingQuestion.onClick.AddListener(UIManager.Instance.ShowAprrovePendingQuestionsPanel);
    }

    //private void LoadData() 
    //{
        //txt_Papcoin.SetText(CurrentUserProfileKeeper.Papcoin.ToString());
        //txt_Gem.SetText(CurrentUserProfileKeeper.Gem.ToString());
    //}

    private void QuickGame() 
    {
        Debug.Log("hiraishin");
        ActionManager.Instance.QuickGame();
    }

    private void RateUs() { }

    private void GetRank() { }

    private void UserProfile() 
    {
        StartCoroutine(ActionManager.Instance.GetCurrentUserProfile());
    }
}
