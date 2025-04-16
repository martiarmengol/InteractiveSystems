using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public GameObject hayBalePrefab; 
    public Transform haySpawnpoint; 
    public float shootInterval; 
    private float shootTimer; 
    public float movementSpeed;
    public float horizontalBoundary = 22;
    public Transform modelParent;

    private int currentShootLevel = 0; // Tracks how many times we've increased rate

    public float shootIntervalLevel1 = 0.75f;
    public float shootIntervalLevel2 = 0.5f;
    public float shootIntervalLevel3 = 0.35f;


    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadModel();

    }
    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); 

        switch (GameSettings.hayMachineColor) 
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
        UpdateShootingRate();
    }
    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) 
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) 
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();

    }
    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime; 

        if (shootTimer <= 0 && Input.GetKeyDown(KeyCode.Space)) 
        {
            shootTimer = shootInterval; 
            ShootHay(); 
        }
    }

    private void UpdateShootingRate()
    {
        int saved = GameStateManager.Instance.sheepSaved;

        if (saved >= 5 && currentShootLevel < 1)
        {
            shootInterval = shootIntervalLevel1;
            currentShootLevel = 1;
        }
        else if (saved >= 10 && currentShootLevel < 2)
        {
            shootInterval = shootIntervalLevel2;
            currentShootLevel = 2;
        }
        else if (saved >= 15 && currentShootLevel < 3)
        {
            shootInterval = shootIntervalLevel3;
            currentShootLevel = 3;
        }
    }


}
