using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyGenerator : MonoBehaviour
{
    public GameObject bunnyPrefab;
    public int startBunnyCount;
    public float spawnTime;
    public int spawnWidth;
    public int spawnHeight;
    public int spawnSeed;
    int currentBunny = 0;

    // Start is called before the first frame update
    void Start()
    {
        //spawnBunny();
        SpeciesDataBank bunnyData = new SpeciesDataBank(3000, bunnyPrefab);
        Vector2[] positions = bunnyData.generateSpawn(startBunnyCount, spawnWidth, spawnHeight, spawnSeed);
        StartCoroutine(generateBunnyPrefabs(positions));
        //StartCoroutine(initialBunnyGenerator());
    }

    private void spawnBunny(Vector2 position)
    {
        GameObject a = Instantiate(bunnyPrefab) as GameObject;
        a.transform.position = new Vector3(position.x + 0.5f, 30.0f, position.y + 0.5f);
    }

    IEnumerator generateBunnyPrefabs(Vector2[] positions)
    {
        while (currentBunny<startBunnyCount){
            yield return new WaitForSeconds(spawnTime);
            spawnBunny(positions[currentBunny]);
            currentBunny++;
        }
    }

    IEnumerator initialBunnyGenerator()
    {
        while (currentBunny<startBunnyCount){
            yield return new WaitForSeconds(spawnTime);
            spawnBunny(Vector2.zero);
            currentBunny++;
        }
    }
}
