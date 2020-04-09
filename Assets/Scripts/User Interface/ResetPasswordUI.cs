using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;
using ConstantKeeper;
using DG.Tweening;

public class ResetPasswordUI : Singleton<ResetPasswordUI>
{
    [Header("InputField")]
    [SerializeField] TMP_InputField inputField_Email;
    
    [Header("Button")]
    [SerializeField] Button btn_GoToSignIn;
    [SerializeField] Button btn_ResetPassword;

    [Header("RectTransform")]
    [SerializeField] private RectTransform panel_Parent;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    void OnClickAddListener()
    {
        btn_GoToSignIn.onClick.AddListener(GoToSignIn);
        btn_ResetPassword.onClick.AddListener(ResetPassword);
    }

    private void ResetPassword() 
    {
        ActionManager.Instance.ResetPasswordWithEmail(inputField_Email.text, ResetPasswordWithEmailSuccessful, ResetPasswordWithEmailFailed);
    }

    private void ResetPasswordWithEmailSuccessful()
    {
      //  NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordSuccessful, AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordSuccessfulDetails);
    }

    private void ResetPasswordWithEmailFailed()
    {
        //NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordFailed, AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordFailedDetails);
    }

    private void GoToSignIn() 
    {
        UIManager.Instance.ShowSignInPanel();

       /* UIManager.Instance.PanelOpener();

        Sequence panelSeq = DOTween.Sequence();
        panelSeq.Append(panel_Parent.DOAnchorPosX(0, 0.3f))
                .Append(panel_Parent.DOAnchorPosY(0, 0.3f))
                .OnComplete(() => UIManager.Instance.ShowSignInPanel());*/
    }
}
