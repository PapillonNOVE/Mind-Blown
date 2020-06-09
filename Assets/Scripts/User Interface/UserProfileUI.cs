using ConstantKeeper;
using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UserProfileUI : MonoBehaviour
{
    [Header("Text")]
    //[SerializeField] private TextMeshProUGUI text_Cup;
    [SerializeField] private TextMeshProUGUI text_Username;
    [SerializeField] private TextMeshProUGUI text_Rank;
    [SerializeField] private TextMeshProUGUI text_Level;
    //[SerializeField] private TextMeshProUGUI text_HighScore;
    //[SerializeField] private TextMeshProUGUI txt_User_SignUpDate;
    //[SerializeField] private TextMeshProUGUI txt_User_LastSeen;
    [SerializeField] private TextMeshProUGUI text_TotalPlayTime;
    //[SerializeField] private TextMeshProUGUI txt_User_TotalMatches;
    //[SerializeField] private TextMeshProUGUI txt_User_CompletedMathces;
    //[SerializeField] private TextMeshProUGUI txt_User_AbandonedMathces;
    [SerializeField] private TextMeshProUGUI text_CorrectAnswers;
    [SerializeField] private TextMeshProUGUI text_WrongAnswers;
    //[SerializeField] private TextMeshProUGUI text_Wins;
    //[SerializeField] private TextMeshProUGUI text_Losses;
    //[SerializeField] private TextMeshProUGUI txt_User_WinningStreak;
    [Header("Button")]
    [SerializeField] private Button button_GoToMainMenu;
    [SerializeField] private Button button_SignOut;


    private void Start()
    {
        OnClickAddListener();
        // ActionManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;

       
    }

    private void OnEnable()
    {
        GetCurrentUserProfile();
    }

    private void OnDisable()
    {
      //  ActionManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
    }

    private void OnClickAddListener()
    {
        button_GoToMainMenu.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
        button_SignOut.onClick.AddListener(SignOut);
    }

    private void GetCurrentUserProfile()
    {
        Debug.Log("Username " + CurrentUserProfileKeeper.Username);

        text_Username.SetText(CurrentUserProfileKeeper.Username);
     //   txt_User_SignUpDate.SetText(CurrentUserProfileKeeper.SignUpDate.ToString());

        //if (bool.Parse(CurrentUserProfileKeeper.SignInStatus.ToString()))
        //{
            //txt_User_LastSeen.SetText("ONLINE");//LocalizationKeeper.Online);
        //}
        //else
        //{
            //txt_User_LastSeen.SetText(CurrentUserProfileKeeper.LastSeen.ToString());
        //}
        
        text_Level.SetText(CurrentUserProfileKeeper.Level.ToString());
        //text_Cup.SetText(CurrentUserProfileKeeper.Cup.ToString());
        text_Rank.SetText(CurrentUserProfileKeeper.Rank);
        text_TotalPlayTime.SetText(CurrentUserProfileKeeper.TotalPlayTime.ToString());
        //txt_User_TotalMatches.SetText(CurrentUserProfileKeeper.TotalMatches.ToString());
        //txt_User_CompletedMathces.SetText(CurrentUserProfileKeeper.CompletedMatches.ToString());
        //txt_User_AbandonedMathces.SetText(CurrentUserProfileKeeper.AbandonedMatches.ToString());
        text_CorrectAnswers.SetText(CurrentUserProfileKeeper.CorrectAnswers.ToString());
        text_WrongAnswers.SetText(CurrentUserProfileKeeper.WrongAnswers.ToString());
        //txt_User_WinningStreak.SetText(CurrentUserProfileKeeper.WinningStreak.ToString());
    }

    private void SignOut() 
    {
        ActionManager.Instance.SignOut(SignOutSuccessful, SignOutFailed);
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
