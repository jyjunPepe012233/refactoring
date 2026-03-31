using UnityEngine;

[CreateAssetMenu(menuName = "Refactoring/Building")]
public class BuildingSO : ScriptableObject
{
	[SerializeField]
	private GameObject _prefab;
	public GameObject Prefab => _prefab;
	
	[Header("Placement")]
	
	[SerializeField]
	private Vector2 _size = Vector2.zero;
	public Vector2 Size => _size;

	[SerializeField]
	private Vector2 _pivot = new Vector2(0.5f, 0.5f);
	public Vector2 Pivot => _pivot;
	
}
