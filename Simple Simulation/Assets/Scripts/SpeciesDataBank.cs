using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesDataBank
{
    public int population;
    public int maxPop;
    public Vector2[] positions;
    public Animal behaviour;
    public GameObject speciesPrefab;
    public GameObject[] animalList;
    public SpeciesDataBank(int maxpop, GameObject myPrefab)
    {
        maxPop = maxpop;
        positions = new Vector2[maxPop];
        GameObject speciesPrefab = myPrefab; // Assign What it looks like
        GameObject[] animalList = new GameObject[maxPop]; // Create array to store animals
    }
    public Vector2[] generateSpawn(int spawnPop, int mapWidth, int mapHeight, int seed)
    {
        System.Random prng = new System.Random(seed);
        

        int initPop = population;
        while (population - initPop < spawnPop)
        {
            int x = prng.Next(-mapWidth/2, mapWidth/2);
            int z = prng.Next(-mapHeight/2, mapHeight/2);
            
            Debug.Log(x + " " + z);

            positions[population] = new Vector2(x, z);

            //animalList[population] = Instantiate(speciesPrefab) as GameObject;
            //animalList[population].transform.position = new Vector3(x + 0.5f, 30.0f, z + 0.5f);

            population++;
            Debug.Log(x + " " + z);
        }
        return positions;


    }
    public void setPositions(Vector2[] pos)
    {
        for(int i = 0; i < pos.Length; i++)
        {
            positions[i] = pos[i];
        }
        Debug.Log(positions);
    }

}
