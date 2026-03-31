using UnityEngine;

public class PlayerCharacterService : MonoBehaviour
{
	public static PlayerCharacterService Instance { get; private set; }
	
	[SerializeField]
	private Transform _character;

	public void Awake()
	{
		Instance = this;
	}

	public void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}

	public Vector2 GetPosition()
	{
		return _character.position;
	}
}
