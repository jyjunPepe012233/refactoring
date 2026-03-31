using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
	private WorldGridService _worldGridService;
	private PlayerCharacterService _characterService;
	private PlayerCameraService _cameraService;

	[SerializeField]
	private float _interactionRange = 3;
	
	private IInteractable _currentTarget;

	public void Awake()
	{
		PlayerInteractionEventBus.Interact += OnInteract;
	}
	
	public void Start()
	{
		_worldGridService = WorldGridService.Instance;
		_characterService = PlayerCharacterService.Instance;
		_cameraService = PlayerCameraService.Instance;
	}
	
	public void OnDestroy()
	{
		PlayerInteractionEventBus.Interact -= OnInteract;
	}

	private void OnInteract()
	{
		if (_currentTarget == null)
		{
			return;
		}

		_currentTarget.OnInteracted();
	}

	public void Update()
	{
		_currentTarget = ComputeCurrentTarget();
		Debug.Log(_currentTarget);
	}

	private IInteractable ComputeCurrentTarget()
	{
		Vector2 targetPos = _cameraService.GetMouseWorldPosition();
		Vector3Int gridTargetPos = _worldGridService.Grid.WorldToCell(targetPos);
		Vector2 gridTargetPos2D = new Vector2(gridTargetPos.x, gridTargetPos.y);
		
		Vector2 characterPos = _characterService.GetPosition();

		float distance = Vector2.Distance(gridTargetPos2D, characterPos);
		if (distance > _interactionRange)
		{
			return null;
		}

		IInteractable interactable = WorldObject2DUtil.GetComponentByWorldPosition<IInteractable>(gridTargetPos2D, true);
		if (interactable == null)
		{
			return null;
		}

		return interactable;
	}
}
