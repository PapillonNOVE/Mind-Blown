using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : Singleton<LoadingUI>
{
	[SerializeField] private GameObject mainCanvas;

	[SerializeField] private RectTransform rectTransform_Point;
	//[SerializeField] private RectTransform rectTransform_Point2;
	//[SerializeField] private RectTransform rectTransform_Point3;
	//[SerializeField] private RectTransform rectTransform_Point4;
	[SerializeField] private RectTransform rectTransform_PointParent;
	[SerializeField] private GameObject pointParent;
	[SerializeField] private GameObject rootParent;

	[SerializeField] private float _timer;
	//[SerializeField] private List<RectTransform> points;
	//[SerializeField] private List<float> pointPosX;

	Vector2 maxParentSize;
	Vector2 minParentSize;

	private void Start()
	{
		float square = Mathf.Pow(rectTransform_Point.sizeDelta.x, 2f) + Mathf.Pow(rectTransform_Point.sizeDelta.y, 2f);

		minParentSize.x = Mathf.Sqrt(square);
		minParentSize.y = Mathf.Sqrt(square);

		maxParentSize = rectTransform_PointParent.sizeDelta;

		StartCoroutine(PointMover());
	}

	private void OnEnable()
	{
		ActionManager.Instance.LoadingPanelSelfDestruction += SelfDestructtion;
	}

	private void OnDestroy()
	{
		ActionManager.Instance.LoadingPanelSelfDestruction -= SelfDestructtion;
	}

	private void Update()
	{
		//Debug.LogError(rectTransform_PointParent.rotation.eulerAngles.z);
	}

	private IEnumerator PointMover()
	{
		Sequence sequence = DOTween.Sequence();

		float lastRotZ = rectTransform_PointParent.rotation.eulerAngles.z;
		//Debug.Log(lastRotZ);
		//lastRotZ += 90;

		//	rectTransform_PointParent.Rotate(new Vector3(0, 0, lastRotZ + 90));

		sequence.Append(rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360))//.OnStepComplete(() => Debug.Log(lastRotZ))
				.Join(rectTransform_PointParent.DOSizeDelta(minParentSize, 1f))
				.Append(rectTransform_PointParent.DOSizeDelta(maxParentSize, 1f))
				.Join(rectTransform_PointParent.DORotate(new Vector3(0, 0, lastRotZ - 450f), 1f, RotateMode.FastBeyond360));//.OnComplete(() => Debug.Log(lastRotZ));



		yield return sequence.WaitForCompletion();
		StartCoroutine(PointMover());
	}

	private void SelfDestructtion(float _Timer)
	{
		mainCanvas.SetActive(true);
		Destroy(rootParent, _timer);
	}
}
