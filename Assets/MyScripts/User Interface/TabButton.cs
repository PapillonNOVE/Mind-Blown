using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class TabButton : MonoBehaviour, IPointerClickHandler
{
	[Header("Icon Texture")]
	[SerializeField] private Texture2D _activeTabIcon;
	[SerializeField] private Texture2D _passiveTabIcon;

	[Header("Icon")]
	[SerializeField] private RawImage _icon;

	[Header("RectTransform")]
	public RectTransform parentRectTransform;
	public Transform parentTransform;

	public void SetActiveIcon()
	{
		_icon.texture = _activeTabIcon;
	}

	public void SetPassiveIcon() 
	{
		_icon.texture = _passiveTabIcon;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		BottomNavigationBarManager.Instance.OnTabSelected(this);
	}
}
