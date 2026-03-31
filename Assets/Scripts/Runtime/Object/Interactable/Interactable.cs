using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
	public void OnInteracted()
	{
		Debug.Log(gameObject.name + " was interacted with!");
	}
}
