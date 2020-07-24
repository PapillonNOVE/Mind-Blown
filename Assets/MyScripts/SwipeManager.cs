using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
	Vertical,
	Horizontal
}

public class SwipeManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	[Header("Aspect")]
	[SerializeField] private Direction _direction;

	[Header("Vertical Settings")]
	[Range(0f, 100f)]
	[SerializeField] private float _verticalDragPercentage;
	[Range(0f, 100f)]
	[SerializeField] private float _verticalDeviationPercentage;

	[Header("Vertical Settings")]
	[Range(0f, 100f)]
	[SerializeField] private float _horizontalDragPercentage;
	[Range(0f, 100f)]
	[SerializeField] private float _horizontalDeviationPercentage;

	[Header("Device Property")]
	[SerializeField] private float deviceHeight;
	[SerializeField] private float deviceWidth;

	[Header("Line")]
	[SerializeField] private Vector2 dragLineBegin;
	//[SerializeField] private Vector2 dragLineEnd;

	private bool isLineEnoughSize = false;

	[SerializeField] private float minVerticalDrag;
	[SerializeField] private float minVerticalDeviation;

	[SerializeField] private float minHorizontalDrag;
	[SerializeField] private float minHorizontalDeviation;

	[Header("Tab")]
	[SerializeField] private int tabIndex;

	void Start()
	{
		SetValues();
	}

	private void SetValues()
	{
		deviceHeight = Screen.height;
		deviceWidth = Screen.width;

		minVerticalDrag = deviceHeight / 100 * _verticalDragPercentage;
		minVerticalDeviation = deviceWidth / 100 * _verticalDeviationPercentage;

		minHorizontalDrag = deviceWidth / 100 * _horizontalDragPercentage;
		minHorizontalDeviation = deviceHeight / 100 * _horizontalDeviationPercentage;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isLineEnoughSize = false;
		//dragLineBegin = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//if (_direction == Direction.Vertical)
		//{
		//	VerticalLine(eventData.position);
		//}
		//else
		//{
		//	HorizontalLine(eventData.position);
		//}
	}

	private void VerticalLine(Vector2 dragLineEnd)
	{
		float dragDistance = Mathf.Abs(dragLineBegin.y - dragLineEnd.y);
		float deviationDistance = Mathf.Abs(dragLineBegin.x - dragLineEnd.x);

		bool isUpper = dragLineBegin.y > dragLineEnd.y;

		if (deviationDistance < minVerticalDeviation && dragDistance >= minVerticalDrag)
		{
			if (isUpper && tabIndex <= 2)
			{
				tabIndex++;
			}
			else if (!isUpper && tabIndex >= 0)
			{
				tabIndex--;
			}

			// Call method here
		}
	}

	private void HorizontalLine(Vector2 dragLineEnd)
	{
		float dragDistance = Mathf.Abs(dragLineBegin.x - dragLineEnd.x);
		float deviationDistance = Mathf.Abs(dragLineBegin.y - dragLineEnd.y);

		bool isNext = dragLineBegin.x > dragLineEnd.x;

		if (deviationDistance < minHorizontalDeviation && dragDistance >= minHorizontalDrag)
		{
			if (isNext && tabIndex < 3)
			{
				tabIndex++;
				BottomNavigationBarManager.Instance.SlidePanel((Tabs)tabIndex);
			}
			else if (!isNext && tabIndex > 0)
			{
				tabIndex--;
				BottomNavigationBarManager.Instance.SlidePanel((Tabs)tabIndex);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//Debug.Log(eventData.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
	}


	private void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			dragLineBegin = Input.mousePosition;
			Debug.Log("of course maam");
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (_direction == Direction.Vertical)
			{
				VerticalLine(Input.mousePosition);
			}
			else
			{
				HorizontalLine(Input.mousePosition);
			}
		}
	}
}
