using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
	[SerializeField] private GameObject mainCanvas;

	[Header("RectTransforms")]
	[SerializeField] private RectTransform _rectTransform_Point;
	[SerializeField] private RectTransform _rectTransform_PointParent;
	
	[Header("Parent Objects")]
	[SerializeField] private GameObject _pointParent;
	[SerializeField] private GameObject _rootParent;


	[Header("Timers")]
	[Range(0f,10f)]
	[SerializeField] private float delayTimer;

	[Space(10)]
	[Range(0f,10f)]
	[SerializeField] private float m_CheckProgressionTimer;
	[Space(5)]
	[Range(0f,10f)]
	[SerializeField] private float m_CheckProgressionTimerLimit;
	
	private Vector2 _maxParentSize;
	private Vector2 _minParentSize;

	public static bool S_IsFirebaseInitialized;
	public static bool S_IsAuthControlled;
	public static bool S_IsDatabaseReferencesCreated;
	public static bool S_IsUserProfileReady;
	public static bool S_IsCorrectPanelSelected;


	private bool isFirstTime = true;

	private void Start()
	{
		float square = Mathf.Pow(_rectTransform_Point.sizeDelta.x, 2f) + Mathf.Pow(_rectTransform_Point.sizeDelta.y, 2f);

		_minParentSize.x = Mathf.Sqrt(square);
		_minParentSize.y = Mathf.Sqrt(square);

		_maxParentSize = _rectTransform_PointParent.sizeDelta;

		StartCoroutine(PointMover());

		m_CheckProgressionTimer = m_CheckProgressionTimerLimit;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			Debug.Log(GeneralControls.IsConnectedInternet() + " Internet Bağlantısı");
			Debug.Log(S_IsFirebaseInitialized + " Firebase Kurulumu");
			Debug.Log(S_IsDatabaseReferencesCreated + " Veritabanı Referansı");
			Debug.Log(S_IsUserProfileReady + " Kullanıcı Profili");
			Debug.Log(S_IsCorrectPanelSelected + " Doğru Panel");
			Debug.Log(S_IsAuthControlled + " Auth Kontrolü");
		}

		m_CheckProgressionTimer -= Time.deltaTime;

		if (m_CheckProgressionTimer <= 0)
		{
			if (isFirstTime)
			{
				if (GeneralControls.IsConnectedInternet() && S_IsFirebaseInitialized && S_IsDatabaseReferencesCreated && S_IsUserProfileReady)
				{
					Debug.Log("E tamam daha ne 1");
					if (S_IsCorrectPanelSelected || S_IsAuthControlled)
					{
						//delayTimer -= Time.deltaTime;

						Debug.Log("E tamam daha ne 2");
						//if (delayTimer <= 0)
						{
							TransitionManager.Instance.TransitionAnimation(SelfDestruction);
							Debug.Log("E tamam daha ne 3");
							isFirstTime = false;
							return;
						}
					}
				}

				m_CheckProgressionTimer = m_CheckProgressionTimerLimit;
			}
		}
	}

	private IEnumerator PointMover()
	{
		Sequence seq = DOTween.Sequence();

		float lastRotZ = _rectTransform_PointParent.rotation.eulerAngles.z;
		//Debug.Log(lastRotZ);
		//lastRotZ += 90;

		//	rectTransform_PointParent.Rotate(new Vector3(0, 0, lastRotZ + 90));

		seq.Append(_rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360));//.OnStepComplete(() => Debug.Log(lastRotZ))
		seq.Join(_rectTransform_PointParent.DOSizeDelta(_minParentSize, 1f));

		seq.Append(_rectTransform_PointParent.DOSizeDelta(_maxParentSize, 1f));
		seq.Join(_rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360));//.OnComplete(() => Debug.Log(lastRotZ));

		yield return seq.WaitForCompletion();
		StartCoroutine(PointMover());
	}

	private void SelfDestruction()
	{
		Destroy(gameObject);
	}
}
