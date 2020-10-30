using Constants;
using EasyMobile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfileUI : MonoBehaviour
{
    [Header("Text")]
    //[SerializeField] private TextMeshProUGUI text_Cup;
    [SerializeField] private TextMeshProUGUI _usernameText;
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _requiredExperienceText;
    //[SerializeField] private TextMeshProUGUI text_HighScore;
    //[SerializeField] private TextMeshProUGUI txt_User_SignUpDate;
    //[SerializeField] private TextMeshProUGUI txt_User_LastSeen;
    [SerializeField] private TextMeshProUGUI _totalPlayTimeText;
    //[SerializeField] private TextMeshProUGUI txt_User_TotalMatches;
    //[SerializeField] private TextMeshProUGUI txt_User_CompletedMathces;
    //[SerializeField] private TextMeshProUGUI txt_User_AbandonedMathces;
    [SerializeField] private TextMeshProUGUI _correctAnswersText;
    [SerializeField] private TextMeshProUGUI _wrongAnswersText;
    //[SerializeField] private TextMeshProUGUI text_Wins;
    //[SerializeField] private TextMeshProUGUI text_Losses;
    //[SerializeField] private TextMeshProUGUI txt_User_WinningStreak;

    [Header("Slider")]
    [SerializeField] private Slider _experienceBar;

    [Header("Button")]
    [SerializeField] private Button _goToMainMenuButton;
    [SerializeField] private Button _signOutButton;


    private void Start()
    {
        OnClickAddListener();
        // ActionManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;

       
    }

    private void OnEnable()
    {
        SetCurrentUserProfileUI();
    }

    private void OnDisable()
    {
      //  ActionManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
    }

    private void OnClickAddListener()
    {
        _goToMainMenuButton.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
        _signOutButton.onClick.AddListener(SignOut);
    }

    private void SetCurrentUserProfileUI()
    {
        //Debug.Log("Username " + CurrentUserProfileKeeper.Username);

        _usernameText.SetText(CurrentUserProfileKeeper.Username);
        //   txt_User_SignUpDate.SetText(CurrentUserProfileKeeper.SignUpDate.ToString());

        //if (bool.Parse(CurrentUserProfileKeeper.SignInStatus.ToString()))
        //{
        //txt_User_LastSeen.SetText("ONLINE");//LocalizationKeeper.Online);
        //}
        //else
        //{
        //txt_User_LastSeen.SetText(CurrentUserProfileKeeper.LastSeen.ToString());
        //}
        _experienceText.SetText(CurrentUserProfileKeeper.Experience.ToString());
        _requiredExperienceText.SetText(CurrentUserProfileKeeper.RequiredExperience.ToString());
        _levelText.SetText(CurrentUserProfileKeeper.Level.ToString());
        //text_Cup.SetText(CurrentUserProfileKeeper.Cup.ToString());
        _rankText.SetText(CurrentUserProfileKeeper.Rank);
        _totalPlayTimeText.SetText(CurrentUserProfileKeeper.TotalPlayTime.ToString());
        //txt_User_TotalMatches.SetText(CurrentUserProfileKeeper.TotalMatches.ToString());
        //txt_User_CompletedMathces.SetText(CurrentUserProfileKeeper.CompletedMatches.ToString());
        //txt_User_AbandonedMathces.SetText(CurrentUserProfileKeeper.AbandonedMatches.ToString());
        _correctAnswersText.SetText(CurrentUserProfileKeeper.CorrectAnswers.ToString());
        _wrongAnswersText.SetText(CurrentUserProfileKeeper.WrongAnswers.ToString());
        //txt_User_WinningStreak.SetText(CurrentUserProfileKeeper.WinningStreak.ToString());

        _experienceBar.maxValue = CurrentUserProfileKeeper.RequiredExperience;
        _experienceBar.value = CurrentUserProfileKeeper.Experience;
    }

    private void SignOut() 
    {
        EventManager.Instance.SignOut(SignOutSuccessful, SignOutFailed);
    }

    private void SignOutSuccessful()
    {
        //NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignInPaths.SignInSuccessful, AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails);
        NativeUI.ShowToast($"{AuthenticationsDebugs.SignOutPaths.SignOutSuccessful} \n {AuthenticationsDebugs.SignOutPaths.SignOutSuccessfulDetails}");
    }

    private void SignOutFailed()
    {
        //NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignInPaths.SignInFailed, AuthenticationsDebugs.SignInPaths.SignInFailedDetails);
        NativeUI.ShowToast($"{AuthenticationsDebugs.SignOutPaths.SignOutFailed} \n {AuthenticationsDebugs.SignOutPaths.SignOutFailedDetails}");
    }
}
