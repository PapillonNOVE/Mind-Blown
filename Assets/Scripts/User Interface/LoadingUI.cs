using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
	[SerializeField] private GameObject mainCanvas;

	[SerializeField] private RectTransform _rectTransform_Point;
	//[SerializeField] private RectTransform rectTransform_Point2;
	//[SerializeField] private RectTransform rectTransform_Point3;
	//[SerializeField] private RectTransform rectTransform_Point4;
	[SerializeField] private RectTransform _rectTransform_PointParent;
	[SerializeField] private GameObject _pointParent;
	[SerializeField] private GameObject _rootParent;

	[SerializeField] private float _timer;
	//[SerializeField] private List<RectTransform> points;
	//[SerializeField] private List<float> pointPosX;
	
	private Vector2 _maxParentSize;
	private Vector2 _minParentSize;

	[SerializeField] private int _progressionTarget;
	public static int progression;

	public static bool isFirebaseInitialized;
	public static bool isAuthControlled;
	public static bool isDatabaseReferencesCreated;
	public static bool isUserProfileReady;
	public static bool isCorrectPanelSelected;


	private bool isFirstTime = true;

	private void Start()
	{
		float square = Mathf.Pow(_rectTransform_Point.sizeDelta.x, 2f) + Mathf.Pow(_rectTransform_Point.sizeDelta.y, 2f);

		_minParentSize.x = Mathf.Sqrt(square);
		_minParentSize.y = Mathf.Sqrt(square);

		_maxParentSize = _rectTransform_PointParent.sizeDelta;

		StartCoroutine(PointMover());
	}

	//private void OnEnable()
	//{
	//	ActionManager.Instance.LoadingPanelSelfDestruction += SelfDestructtion;
	//}

	//private void OnDestroy()
	//{
	//	ActionManager.Instance.LoadingPanelSelfDestruction -= SelfDestructtion;
	//}

	private void Update()
	{
		if (isFirstTime)
		{
			if (isFirebaseInitialized && isDatabaseReferencesCreated && isUserProfileReady && isCorrectPanelSelected || isAuthControlled)
			{
				TransitionManager.Instance.TransitionAnimTrigger(SelfDestruction);
				Debug.LogError("Çalışıyor!!!");
				isFirstTime = false;
			}
		}
	}

	private IEnumerator PointMover()
	{
		Sequence sequence = DOTween.Sequence();

		float lastRotZ = _rectTransform_PointParent.rotation.eulerAngles.z;
		//Debug.Log(lastRotZ);
		//lastRotZ += 90;

		//	rectTransform_PointParent.Rotate(new Vector3(0, 0, lastRotZ + 90));

		sequence.Append(_rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360))//.OnStepComplete(() => Debug.Log(lastRotZ))
				.Join(_rectTransform_PointParent.DOSizeDelta(_minParentSize, 1f))
				.Append(_rectTransform_PointParent.DOSizeDelta(_maxParentSize, 1f))
				.Join(_rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360));//.OnComplete(() => Debug.Log(lastRotZ));



		yield return sequence.WaitForCompletion();
		StartCoroutine(PointMover());
	}

	private void SelfDestruction()
	{
		Destroy(gameObject);
	}
}
