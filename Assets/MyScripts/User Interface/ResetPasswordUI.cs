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
    [SerializeField] private TMP_InputField _emailInputField;

    [Header("Button")]
    [SerializeField] private Button _goToSignInButton;
    [SerializeField] private Button _resetPasswordButton;

    [Header("RectTransform")]
    [SerializeField] private RectTransform panel_Parent;

    private void Start()
    {
        OnClickAddListener();
    }

    private void OnClickAddListener()
    {
        _goToSignInButton.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
        _resetPasswordButton.onClick.AddListener(ResetPassword);
    }

    private void ResetPassword()
    {
        StartCoroutine(EventManager.Instance.ResetPasswordWithEmail(_emailInputField.text, ResetPasswordWithEmailSuccessful, ResetPasswordWithEmailFailed));
    }

    private void ResetPasswordWithEmailSuccessful()
    {
        NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordSuccessful, AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordSuccessfulDetails);
        BottomNavigationBarManager.Instance.ShowUserNavigation();
    }

    private void ResetPasswordWithEmailFailed()
    {
        NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordFailed, AuthenticationsDebugs.ResetPasswordPaths.ResetPasswordFailedDetails);
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
