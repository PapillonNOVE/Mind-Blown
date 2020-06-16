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

public class OptionButton : MonoBehaviour//, IPointerClickHandler
{
    public bool isTrueOption;

    [Header("Component")]
    public Button _optionButton;
    public TextMeshProUGUI _optionText;
    public RawImage _optionBackgroundImage;
    public RawImage _chooseIconImage;
    
    [Header("Option Background")]
    [SerializeField] private Texture2D _optionDefaultBackground;
    [SerializeField] private Texture2D _optionWrongBackground;
    [SerializeField] private Texture2D _optionCorrectBackground;

    [Header("Choose Icon")]
    [SerializeField] private Texture2D _defaultOptionIcon;
    [SerializeField] private Texture2D _choosenOptionIcon;
    [SerializeField] private Texture2D _correctOptionIcon;

    public ButtonCode _buttonCode;

    private void OnEnable()
    {
        PrepareButton();
    }

    //private void OnDisable()
    //{
    //    Unsubscribe();
    //}

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ActionManager.Instance.UpdateOptionButton += UpdateButton;
    }

    //private void Unsubscribe() 
    //{
    //    ActionManager.Instance.PrepareOptionButton -= PrepareButton;
    //}

    private void PrepareButton()
    {
        _optionButton.onClick.AddListener(() => ActionManager.Instance.ControlAnswer(isTrueOption, this));
    }

    private void UpdateButton(string optionText, ButtonCode buttonCode, bool isCorrectOption = false)
    {
        if (buttonCode == _buttonCode)
        {
            _optionBackgroundImage.texture = _optionDefaultBackground;
            _chooseIconImage.texture = _defaultOptionIcon;
            _optionText.SetText(optionText);
            isTrueOption = isCorrectOption;
        }
        else
        {
            return;
        }
    }

    public void WrongOption() 
    {
        _optionBackgroundImage.texture = _optionWrongBackground;
        _chooseIconImage.texture = _choosenOptionIcon;
    }

    public void CorrectOption()
    {
        _optionBackgroundImage.texture = _optionCorrectBackground;
        _chooseIconImage.texture = _choosenOptionIcon;
    }

    public void ShowCorrectOption() 
    {
        _optionBackgroundImage.texture = _optionCorrectBackground;
        _chooseIconImage.texture = _correctOptionIcon;
    }
}
