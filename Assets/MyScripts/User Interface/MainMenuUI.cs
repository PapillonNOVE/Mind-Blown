using Constants;
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
        _goToCategoriesButton.onClick.AddListener(OpenCategories);
        _goToSettingsButton.onClick.AddListener(OpenSettings);
        //btn_Menu_Store.onClick.AddListener(UIManager.Instance.ShowStorePanel);
        _goToUserProfileButton.onClick.AddListener(OpenUserProfile);
        //btn_Menu_RateUs.onClick.AddListener(RateUs);
        _goToSendQuestionButton.onClick.AddListener(OpenQuestionSend);
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
            TransitionManager.Instance.TransitionAnimation(UIManager.Instance.ShowGamePanel);
        }
        else
        {
           UIManager.Instance.ShowCategoriesPanel();
        }
    }

    private void OpenCategories() 
    {
        UIManager.Instance.ShowCategoriesPanel();
    }

	#region Kullanılmıyor

	private void OpenSettings() 
    {
        UIManager.Instance.ShowSettingsPanel();
    }

    private void OpenQuestionSend()
    {
        UIManager.Instance.ShowSendQuestionPanel();
    }

    private void OpenUserProfile() 
    {
        BottomNavigationBarManager.Instance.ShowUserNavigation();
    }

	#endregion

	private void RateUs() { }

    private void GetRank() { }
}
