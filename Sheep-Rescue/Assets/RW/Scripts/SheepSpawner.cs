using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 

    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;

    private Coroutine spawnCoroutine;

    private List<GameObject> sheepList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpawnInterval(float newInterval)
    {
        timeBetweenSpawns = newInterval;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        if (canSpawn)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }


    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; 
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); 
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this); 
    }
    private IEnumerator SpawnRoutine() 
    {
        while (canSpawn) 
        {
            SpawnSheep(); 
            yield return new WaitForSeconds(timeBetweenSpawns); 
        }
    }
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) 
        {
            Destroy(sheep); 
        }

        sheepList.Clear();
    }



}
