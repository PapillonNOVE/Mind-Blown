using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondChanceUI : MonoBehaviour
{
	[Header("Countdown")]
	[Range(0f, 5f)]
	[SerializeField] private float m_AcceptableTimeLimit;
	[SerializeField] private float m_AcceptableTimeCountdown;

	[Header("Buttons")]
	[SerializeField] private Button m_AcceptButton;
	[SerializeField] private Button m_DenyButton;

	private void Start()
	{
		InitOnClickListener();

		Subscribe();
	}

	private void OnDisable()
	{
		Init();
	}

	private void OnApplicationQuit()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	#region Init

	private void Init()
	{
		m_AcceptableTimeCountdown = m_AcceptableTimeLimit;

		m_AcceptButton.interactable = true;
		m_AcceptButton.image.fillAmount = 1;
	}

	private void InitOnClickListener()
	{
		m_AcceptButton.onClick.AddListener(Accept);
		m_DenyButton.onClick.AddListener(Deny);
	}

	#endregion

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		//EventManager.Instance.OpenSecondChancePanel += SetActiveSelf;
	}

	private void Unsubscribe()
	{
		//EventManager.Instance.OpenSecondChancePanel -= SetActiveSelf;
	}

	#endregion

	private void Update()
	{
		CountDownForAccept();
	}

	private void CountDownForAccept()
	{
		if (m_AcceptableTimeCountdown <= 0)
		{
			m_AcceptButton.interactable = false;

			Deny();

			return;
		}

		m_AcceptableTimeCountdown -= Time.deltaTime;

		m_AcceptButton.image.fillAmount = m_AcceptableTimeCountdown / m_AcceptableTimeLimit;
	}

	public void SetActiveSelf() 
	{
		gameObject.SetActive(true);
	}

	public void SetPassiveSelf() 
	{
		gameObject.SetActive(false);
	}

	private void Accept()
	{
		AdManager.Instance.ShowRewardedAds();
		SetPassiveSelf();
	}

	private void Deny()
	{
		EventManager.Instance.GameOverTrigger?.Invoke(GameOverType.WrongAnswer);
		SetPassiveSelf();
	}
}
