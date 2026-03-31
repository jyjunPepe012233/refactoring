using UnityEngine;

[CreateAssetMenu(menuName = "Refactoring/Building")]
public class BuildingSettingSO : ScriptableObject
{
	[SerializeField]
	private Buildable _prefab;
	public Buildable Prefab => _prefab;
	
	[Header("Placement")]
	
	[SerializeField]
	private Vector2 _size;
	public Vector2 Size => _size;

	[SerializeField]
	private Vector2 _pivot;
	public Vector2 Pivot => _pivot;
	
}
