using UnityEngine;

public static class WorldObject2DUtil
{
	private static readonly Collider2D[] hitsByWorldPosition = new Collider2D[10];

	public static T GetComponentByWorldPosition<T>(Vector2 worldPosition) where T : class
	{
		return GetComponentByWorldPosition<T>(worldPosition, false);
	}
	
	public static T GetComponentByWorldPosition<T>(Vector2 worldPosition, bool includeParent) where T : class
	{
		var size = Physics2D.OverlapPointNonAlloc(worldPosition, hitsByWorldPosition);
		
		if (size <= 0)
		{
			return null;
		}
		
		if (includeParent)
		{
			// 콜라이더에 직접 붙어있는 컴포넌트와 부모 및 조상 오브젝트에 붙어있는 컴포넌트까지 모두 검색
			foreach (var hit in hitsByWorldPosition)
			{
				T target = hit.GetComponentInParent<T>(true);
				if (target != null)
				{
					return target;
				}
			}
		}
		else
		{
			// 콜라이더에 직접 붙어있는 컴포넌트만 검색
			foreach (var hit in hitsByWorldPosition)
			{
				T target = hit.GetComponent<T>();
				if (target != null)
				{
					return target;
				}
			}
		}

		return null;
	}
}
