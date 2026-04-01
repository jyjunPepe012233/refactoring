using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionListItemUI : MonoBehaviour
{
	[SerializeField] private Vector2 _defaultIconSize = new Vector2(16, 16);
	
	[Header("UGUI")]
	[SerializeField] private RectTransform _root;
	[SerializeField] private Image _icon;
	[SerializeField] private Button _button;

	private BuildingSO _buildingSo;

	private Vector2 _originalRectSize;
	
	void Awake()
	{
		_originalRectSize = _root.rect.size;
		_button.onClick.AddListener(OnClicked);
	}

	void OnDestroy()
	{
		_button.onClick.RemoveListener(OnClicked);
	}
	
	void OnClicked()
	{
		PlayerBuildEventBus.SelectBuilding?.Invoke(_buildingSo);
	}
	
	public void SetBuilding(BuildingSO buildingSo)
	{
		_buildingSo = buildingSo;

		_icon.sprite = buildingSo.Icon;
		
		// _defaultIconSize와 _originalRectSize를 바탕으로 아이콘의 크기를 조절
		Vector2 size = new Vector2(
			size.x = buildingSo.Icon.rect.width / _defaultIconSize.x * _originalRectSize.x,
			size.y = buildingSo.Icon.rect.height / _defaultIconSize.y * _originalRectSize.y
		);
		
		_root.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
		_root.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
	}
}
