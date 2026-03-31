using UnityEngine;

public class PlayerCharacterService : MonoBehaviour
{
	public static PlayerCharacterService Instance { get; private set; }
	
	[SerializeField]
	private Transform _character;

	public Vector2 GetPosition()
	{
		return _character.position;
	}
}
