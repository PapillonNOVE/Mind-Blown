using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnimationController : MonoBehaviour
{
	[Header("Question Positions")]
	[Range(0f, 1f)]
	[SerializeField] private float m_AnimDelay;

	[Space(5)]
	[Range(0f, 3f)]
	[SerializeField] private float m_FadeInDuration;

	[Space(5)]
	[Range(0f, 3f)]
	[SerializeField] private float m_FadeOutDuration;

	[Header("Rect Transforms")]
	[SerializeField] private RectTransform m_QuestionRectTransform;

	[Space(10)]
	[SerializeField] private List<RectTransform> m_OptionLayoutElements;

	[Header("Question Positions")]
	[SerializeField] private Vector2 m_QuestionTextStartSize;

	[Header("Questions Sizes")]
	[SerializeField] private Vector2 m_OptionsStartSize;

	private void Awake()
	{
		Subscribe();
		Init();
	}

	private void OnDestroy()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.QuestionFadeInAnim += QuestionFadeInAnim;
		EventManager.Instance.QuestionFadeOutAnim += QuestionFadeOutAnim;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.QuestionFadeInAnim -= QuestionFadeInAnim;
		EventManager.Instance.QuestionFadeOutAnim -= QuestionFadeOutAnim;
	}

	#endregion

	private void Init()
	{
		m_QuestionTextStartSize = m_QuestionRectTransform.localScale;

		m_OptionsStartSize = m_OptionLayoutElements[0].localScale;
	}

	private void QuestionFadeOutAnim()
	{
		Sequence questionFadeOutAnimSeq = DOTween.Sequence();

		questionFadeOutAnimSeq.Append(m_QuestionRectTransform.DOScale(Vector2.zero, m_FadeOutDuration));
		//questionFadeOutAnimSeq.Join(m_QuestionRectTransform.GetComponent<TextMeshProUGUI>().DOFade(0f, m_FadeOutDuration));

		foreach (RectTransform optionRectTransform in m_OptionLayoutElements)
		{
			questionFadeOutAnimSeq.Join(optionRectTransform.DOScale(Vector2.zero, m_FadeOutDuration));
		}

		questionFadeOutAnimSeq.OnComplete(() => StartCoroutine(EventManager.Instance.GetQuestion?.Invoke()));
	}

	private void QuestionFadeInAnim()
	{
		Sequence questionFadeInAnimSeq = DOTween.Sequence();

		questionFadeInAnimSeq.Append(m_QuestionRectTransform.DOScale(m_QuestionTextStartSize, m_FadeInDuration));
		//questionFadeInAnimSeq.Join(m_QuestionRectTransform.GetComponent<TextMeshProUGUI>().DOFade(1f, m_FadeInDuration));

		foreach (RectTransform optionRectTransform in m_OptionLayoutElements)
		{
			questionFadeInAnimSeq.Join(optionRectTransform.DOScale(m_OptionsStartSize, m_FadeInDuration));
		}
	}
}
