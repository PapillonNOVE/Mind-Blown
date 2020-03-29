using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonCode
{
    Option1,
    Option2,
    Option3,
    Option4
}

public class ButtonSetter : MonoBehaviour//, IPointerClickHandler
{
    public bool isTrueOption;

    // Components
    private Button _buttonOption;
    private TextMeshProUGUI _textOption;
    private Image _imageButton;

    [SerializeField] private ButtonCode buttonCode;

    private void OnEnable()
    {
        SetReferences();
        Register();
        PrepareButton();
    }

    private void OnDisable()
    {
        //ActionManager.Instance.PrepareOptionButton -= PrepareButton;
    }

    private void SetReferences()
    {
        _buttonOption = GetComponent<Button>();
        _textOption = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _imageButton = GetComponent<Image>();
    }

    private void Register()
    {
        ActionManager.Instance.UpdateOptionButton += UpdateButton;
    }

    private void PrepareButton()
    {
        _buttonOption.onClick.AddListener(() => ActionManager.Instance.ControlAnswer(isTrueOption,_buttonOption));
    }

    private void UpdateButton(string _OptionText, ButtonCode _ButtonCode, bool _IsTrueOption = false)
    {
        if (_ButtonCode == buttonCode)
        {
            _imageButton.color = Color.white;
            _textOption.SetText(_OptionText);
            isTrueOption = _IsTrueOption;
        }
        else
        {
            return;
        }
    }
}
