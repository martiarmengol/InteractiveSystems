using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; // 1

    [HideInInspector]
    public int sheepSaved; // 2

    [HideInInspector]
    public int sheepDropped; // 3

    public int sheepDroppedBeforeGameOver; // 4
    public SheepSpawner sheepSpawner; // 5

    private int currentSpawnLevel = 0;

    public float spawnIntervalLevel1 = 1.5f;
    public float spawnIntervalLevel2 = 1.0f;
    public float spawnIntervalLevel3 = 0.7f;


    void Awake()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        UpdateSpawnRate();

    }

    private void UpdateSpawnRate()
    {
        int saved = sheepSaved;

        if (saved >= 10 && currentSpawnLevel < 1)
        {
            sheepSpawner.UpdateSpawnInterval(spawnIntervalLevel1);
            currentSpawnLevel = 1;
        }
        else if (saved >= 20 && currentSpawnLevel < 2)
        {
            sheepSpawner.UpdateSpawnInterval(spawnIntervalLevel2);
            currentSpawnLevel = 2;
        }
        else if (saved >= 30 && currentSpawnLevel < 3)
        {
            sheepSpawner.UpdateSpawnInterval(spawnIntervalLevel3);
            currentSpawnLevel = 3;
        }
    }


    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
    }
    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // 1
        sheepSpawner.DestroyAllSheep(); // 2
        UIManager.Instance.ShowGameOverWindow();
    }
    public void DroppedSheep()
    {
        sheepDropped++; // 1
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver) // 2
        {
            GameOver();
        }
    }

}
