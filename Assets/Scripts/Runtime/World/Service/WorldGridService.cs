using UnityEngine;

public class WorldGridService : MonoBehaviour
{
	public static WorldGridService Instance { get; private set; }
	
	[SerializeField]
	private Grid _grid;
	public Grid Grid => _grid;

	public void Awake()
	{
		Instance = this;
		
		if (!_grid)
		{
			Debug.LogError("그리드가 할당되지 않았음: " + gameObject.name);
		}
	}

	public void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}

	public Vector2 WorldToTileCell(Vector2 vector)
	{
		return Grid.WorldToCell(vector) + (_grid.cellSize / 2);
	}
}
