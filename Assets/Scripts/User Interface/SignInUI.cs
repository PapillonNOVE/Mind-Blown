using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EasyMobile;
using ConstantKeeper;

public class SignInUI : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color color_Green;
    [SerializeField] private Color color_Red;

    [Header("InputField")]
    [SerializeField] private TMP_InputField inputField_Email;
    [SerializeField] private TMP_InputField inputField_Password;
    
    [Header("Button")]
    [SerializeField] private Button btn_GoToMainMenu;
    [SerializeField] private Button btn_SignIn;
    [SerializeField] private Button btn_ResetPassword;
    [SerializeField] private Button btn_GoToSignUp;

    [Header("RectTransform")]
    [SerializeField] private RectTransform panel_Parent;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    private void OnClickAddListener()
    {
        btn_GoToMainMenu.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
        btn_ResetPassword.onClick.AddListener(GoToResetPassword);
        btn_SignIn.onClick.AddListener(SignIn);
        btn_GoToSignUp.onClick.AddListener(GoToSignUp);
    }

    private void SignIn()
    {
        string email = inputField_Email.textComponent.text.Replace("\u200B", "");
        string password = inputField_Password.textComponent.text.Replace("\u200B", "");

        StartCoroutine(ActionManager.Instance.SignInWithEmailPassword(email, password, SignInEmailPasswordSuccessful, SignInWithEmailPasswordFailed));
    }

    private void SignInEmailPasswordSuccessful()
    {
        // NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignInPaths.SignInSuccessful, AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails);
     //   NativeUI.ShowToast($"{AuthenticationsDebugs.SignInPaths.SignInSuccessful} {AuthenticationsDebugs.SignInPaths.SignInSuccessfulDetails}");
    }

    private void SignInWithEmailPasswordFailed()
    {
      //  NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignInPaths.SignInFailed, AuthenticationsDebugs.SignInPaths.SignInFailedDetails);
    }

    private void GoToSignUp()
    {
        UIManager.Instance.ShowSignUpPanel();

        //UIManager.Instance.PanelOpener();

        /* Sequence panelSeq = DOTween.Sequence();
         panelSeq.Append(panel_Parent.DOAnchorPosX(1080, 0.3f))
                 .Append(panel_Parent.DOAnchorPosY(0, 0.3f))
                 .OnComplete(()=> UIManager.Instance.ShowSignUpPanel());*/
    }

    private void GoToResetPassword()
    {
        UIManager.Instance.ShowResetPasswordPanel();

        //UIManager.Instance.PanelOpener();

       /* Sequence panelSeq = DOTween.Sequence();
        panelSeq.Append(panel_Parent.DOAnchorPosX(0, 0.3f))
                .Append(panel_Parent.DOAnchorPosY(-1920, 0.3f))
                .OnComplete(() => UIManager.Instance.ShowResetPasswordPanel());*/
    }
}
