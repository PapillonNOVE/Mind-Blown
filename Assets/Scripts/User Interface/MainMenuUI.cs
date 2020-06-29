using ConstantKeeper;
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
    [SerializeField] Button _playButton;
    [SerializeField] Button _goToCategoriesButton;
    [SerializeField] Button _goToSettingsButton;
    //[SerializeField] Button btn_Store;
    //[SerializeField] Button btn_RateUs;
    [SerializeField] Button _goToUserProfileButton;
    [SerializeField] Button _goToSendQuestionButton;


    private void Start()
    {
        OnClickAddListener();
      //  LoadData();
    }

    private void OnClickAddListener() 
    {
        _playButton.onClick.AddListener(Play);
        _goToCategoriesButton.onClick.AddListener(UIManager.Instance.ShowCategoriesPanel);
        _goToSettingsButton.onClick.AddListener(UIManager.Instance.ShowSettingsPanel);
        //btn_Menu_Store.onClick.AddListener(UIManager.Instance.ShowStorePanel);
        _goToUserProfileButton.onClick.AddListener(UserProfile);
        //btn_Menu_RateUs.onClick.AddListener(RateUs);
        _goToSendQuestionButton.onClick.AddListener(UIManager.Instance.ShowSendQuestionPanel);
    }

    //private void LoadData() 
    //{
        //txt_Papcoin.SetText(CurrentUserProfileKeeper.Papcoin.ToString());
        //txt_Gem.SetText(CurrentUserProfileKeeper.Gem.ToString());
    //}

    private void Play() 
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.CATEGORY_SELECTED))
        {
           UIManager.Instance.ShowGamePanel();
        }
        else
        {
           UIManager.Instance.ShowCategoriesPanel();
        }
    }

    private void RateUs() { }

    private void GetRank() { }

    private void UserProfile() 
    {
        BottomNavigationBarManager.Instance.ShowUserNavigation();
    }
}
