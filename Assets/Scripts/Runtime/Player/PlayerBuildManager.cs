using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class GhostBuildingPool
{
	[SerializeField] private float ghostAlpha = 0.5f;
	
	private readonly Dictionary<BuildingSO, Building> _pool = new();

	public Building Take(BuildingSO buildingSo)
	{
		if (!_pool.TryGetValue(buildingSo, out var building))
		{
			building = InstantiateGhost(buildingSo);
			_pool.Add(buildingSo, building);
		}

		// TODO: 이후에 Building 클래스에서 제공하는 함수로 Ghost 상태를 설정하는 것이 좋음
		building.gameObject.SetActive(true);
		return building;
	}

	public void Return(Building building)
	{
		foreach (var kvp in _pool)
		{
			if (kvp.Value == building)
			{
				building.gameObject.SetActive(false);
			}
		}
		
		Debug.LogWarning($"[GhostBuildingPool] {building.gameObject.name}이 풀에 등록된 적이 없어 반환할 수 없습니다.");
	}

	Building InstantiateGhost(BuildingSO buildingSo)
	{
		GameObject ghost = Object.Instantiate(buildingSo.Prefab);
		
		if (ghost != null && ghost.TryGetComponent<Building>(out var building))
		{
			Color c = building.SpriteRenderer.color;
			c.a = ghostAlpha;
			building.SpriteRenderer.color = c;
			
			return building;
		}
		
		throw new Exception("[GhostBuildingPool] 빌딩 프리팹에 Building 컴포넌트가 없습니다. Ghost로 사용할 수 없습니다.");
	}
}

public class PlayerBuildManager : MonoBehaviour
{
	[SerializeField] private GhostBuildingPool _ghostBuildingPool = new GhostBuildingPool();
	
	private PlayerCameraService _cameraService;
	private WorldGridService _worldGridService;

	private bool _isBuildMode;
	private BuildingSO _currentBuildingSo;
	private Building _currentGhostBuilding;
	
	private void Awake()
	{
		PlayerBuildEventBus.SelectBuilding += OnSelectBuilding;
		PlayerBuildEventBus.UnselectBuilding += OnUnselectBuilding;
		PlayerBuildEventBus.Build += OnBuild;
	}

	private void OnDestroy()
	{
		PlayerBuildEventBus.Build -= OnBuild;
	}
	
	private void Start()
	{
		_cameraService = PlayerCameraService.Instance;
		_worldGridService = WorldGridService.Instance;
	}

	private void Update()
	{
		if (_isBuildMode)
		{
			Vector2 mouseWorldPos = _cameraService.GetMouseWorldPosition();
			Vector2 cellPos2D = _worldGridService.WorldToTileCell(mouseWorldPos); // Vector3Int -> Vector3 -> Vector2
			Vector2 buildingCellPos = cellPos2D + _currentGhostBuilding.LocalPivotPosition;

			_currentGhostBuilding.transform.position = buildingCellPos;
		}
	}

	private void OnSelectBuilding(BuildingSO buildingSo)
	{
		_isBuildMode = true;
		_currentBuildingSo = buildingSo;
		
		ChangeGhostBuilding();
	}

	private void OnUnselectBuilding()
	{
		_isBuildMode = false;
		_currentBuildingSo = null;
		
		ReleaseGhostBuilding();
	}

	private void ChangeGhostBuilding()
	{
		if (_currentGhostBuilding != null)
		{
			_ghostBuildingPool.Return(_currentGhostBuilding);
			_currentGhostBuilding = null;
		}

		_currentGhostBuilding = _ghostBuildingPool.Take(_currentBuildingSo);
	}

	private void ReleaseGhostBuilding()
	{
		if (_currentGhostBuilding != null)
		{
			_ghostBuildingPool.Return(_currentGhostBuilding);
			_currentGhostBuilding = null;
		}
	}

	private void OnBuild(BuildingSO buildingSo)
	{
		
	}
}
