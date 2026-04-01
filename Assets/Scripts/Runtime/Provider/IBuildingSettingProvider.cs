using System.Collections.Generic;

public interface IBuildingSettingProvider
{
	IReadOnlyList<BuildingSO> Settings { get; }
}
