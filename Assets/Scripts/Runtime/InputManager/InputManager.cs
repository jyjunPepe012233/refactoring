using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void Initialize()
	{
		Instance = new GameObject("InputManager").AddComponent<InputManager>();
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			PlayerInteractionEventBus.Interact.Invoke();
		}
	}
}
