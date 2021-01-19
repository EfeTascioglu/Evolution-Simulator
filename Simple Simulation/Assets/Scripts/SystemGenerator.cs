using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGenerator : MonoBehaviour
{
    public int mapChunkSize;
    // Start is called before the first frame update
    void Start()
    {
        Senario SimulationSetting = new Senario();
    }
}

public class Tile
{
    public enum TerrainType {water, land};
    public TerrainType tileTerrainType;
    public float altitude;
    public float slope;

    public Tile(float height, float angle, int waterElevation)
    {
        if ( height > waterElevation)
        {
            tileTerrainType = TerrainType.land;
            altitude = height - waterElevation;
            slope = angle;
        }
        else
        {
            tileTerrainType = TerrainType.water;
            altitude = height - waterElevation;
        }
    }
}

public class Senario
{
    
    
    
    
    
    //MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, MeshHeightCurve, levelOfDetail);
}