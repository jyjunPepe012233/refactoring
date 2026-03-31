using UnityEngine;

public class PlayerCameraService : MonoBehaviour
{
	public static PlayerCameraService Instance { get; private set; }
	
	[SerializeField]
	private Camera _camera;

	public void Awake()
	{
		Instance = this;
		
		if (!_camera)
		{
			Debug.LogError("카메라가 할당되지 않았음: " + gameObject.name);
		}
	}
	
	public void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}

	public Vector2 GetMouseWorldPosition()
	{
		Vector2 mousePos = Input.mousePosition;
		return _camera.ScreenToWorldPoint(mousePos);
	}
}
