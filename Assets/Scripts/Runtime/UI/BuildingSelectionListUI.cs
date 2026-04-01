using UnityEngine;

public class BuildingSelectionListUI : MonoBehaviour
{
	[SerializeField] private InterfaceRef<IBuildingSettingProvider> _buildingSettingProvider;
	
	[Header("List")]
	[SerializeField] private BuildingSelectionListItemUI _itemPrefab;
	[SerializeField] private Transform _content;
	
	private void Start()
	{
		foreach (BuildingSO buildingSo in _buildingSettingProvider.Value.Settings)
		{
			BuildingSelectionListItemUI item = Instantiate(_itemPrefab, _content);
			item.SetBuilding(buildingSo);
		}
	}
}
