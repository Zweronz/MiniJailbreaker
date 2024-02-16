using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TerrainConfig : ScriptableObject
{
	public List<AreaCfg> cmAreaList;

	public List<TerrainCfg> cmTerrainList;
}
