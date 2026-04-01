using System;

public static class PlayerBuildEventBus
{
	public static Action<BuildingSO> SelectBuilding;
	public static Action UnselectBuilding;
	public static Action<BuildingSO> Build;
}
