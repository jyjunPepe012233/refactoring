using System.Collections.Generic;
using UnityEngine;

public class BuildingSettingProvider : MonoBehaviour, IBuildingSettingProvider
{
	[SerializeField]
	private BuildingSO[] _settings;

	public IReadOnlyList<BuildingSO> Settings => _settings;
}
