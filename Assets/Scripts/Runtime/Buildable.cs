using UnityEngine;

public class Buildable : MonoBehaviour, IPlaceable
{
	[SerializeField]
	private BuildingSettingSO _setting;

	public Vector2 Size => _setting?.Size ?? Vector2.one;

	public Vector2 Pivot => _setting?.Pivot ?? new Vector2(0.5f, 0.5f);

	public void Awake()
	{
		if (!_setting)
		{
			Debug.LogError("Building Setting이 할당되지 않음: " + gameObject.name);
		}
	}
}
