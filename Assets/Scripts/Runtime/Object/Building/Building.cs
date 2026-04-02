using UnityEngine;

public class Building : MonoBehaviour
{
	[SerializeField]
	private BuildingSO _buildingSO;
	public BuildingSO BuildingSO => _buildingSO;
	
	[Header("Components")]
	[SerializeField] private SpriteRenderer _spriteRenderer;
	public SpriteRenderer SpriteRenderer => _spriteRenderer;

	public Vector2 LocalPivotPosition => new Vector2(
		(_buildingSO.Pivot.x - 0.5f) * _buildingSO.Size.x,
		(_buildingSO.Pivot.y - 0.5f) * _buildingSO.Size.y
	);
	
	public Vector2 PivotPosition
	{
		get
		{
			Vector2 value = transform.position;
			return value + new Vector2(
				(_buildingSO.Pivot.x - 0.5f) * _buildingSO.Size.x,
				(_buildingSO.Pivot.y - 0.5f) * _buildingSO.Size.y
			);
		}
	}
	
#if UNITY_EDITOR
	public void OnDrawGizmosSelected() 
	{
		if (_buildingSO == null) return;

		Vector2 size = _buildingSO.Size;
		Vector2 pivot = _buildingSO.Pivot;

		// 월드 기준 중심 계산
		Vector2 worldPos = transform.position;

		// pivot 기준 offset 계산 (0~1 → -0.5~0.5로 변환)
		Vector2 pivotOffset = new Vector2(
			(pivot.x - 0.5f) * size.x,
			(pivot.y - 0.5f) * size.y
		);

		Vector2 center = worldPos;

		// 1. Size 영역 (박스)
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(center, new Vector3(size.x, size.y, 0));

		// 2. Pivot 위치
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(worldPos + pivotOffset, 0.1f);

		// 3. 중심점 (디버깅용)
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(center, 0.07f);

		// 텍스트 표시
		UnityEditor.Handles.Label(worldPos + Vector2.up * 0.5f, $"Pivot ({pivot.x:F2}, {pivot.y:F2})");
		UnityEditor.Handles.Label(center + Vector2.down * 0.5f, $"Center");
	}
#endif
}
