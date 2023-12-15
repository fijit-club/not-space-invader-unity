using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour
{

    public int score;
    public int coins;
    public static int Coins;
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinText;
    
    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 

    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    Camera mainCamera;

    [SerializeField] private Transform enemyParent;

    private int _currentWaveNumber;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float speedIncrementAmount;
    [SerializeField] private float maxBulletSpeed;

    public void StopInvoke()
    {
        CancelInvoke(nameof(IncreaseScore));
    }
    
    public void ResetSpeed()
    {
        bulletSpeed = 10f;
        score = 0;
        Coins = 0;
        scoreText.text = "Score: " + score;
    }
    
    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnWave));
    }

    private void IncreaseScore()
    {
        score++;
        coins = Coins;
        coinText.text = "Coins: " + Coins;
        scoreText.text = "Score: " + score;
    }
    
    public void StartWaves()
    {
        InvokeRepeating(nameof(IncreaseScore), .1f, .1f);
        
        _currentWaveNumber = 0;
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        // for (int i = waveNumber; i<enemyWaves.Length; i++) 
        // {
        //     StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        // }
        
        InvokeRepeating(nameof(SpawnWave), .1f, 5f);
        StartCoroutine(PlanetsCreation());
    }

    private void SpawnWave()
    {
        int i = Random.Range(0, enemyWaves.Length);
        
        GameObject wave = Instantiate(enemyWaves[i].wave, enemyParent);
        Wave waveComp = wave.GetComponent<Wave>();
        waveComp.parent = enemyParent;
        waveComp.enemySpeed = bulletSpeed;
        
        if (bulletSpeed < maxBulletSpeed)
            bulletSpeed += speedIncrementAmount;
    }
    
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
        {
            GameObject wave = Instantiate(Wave, enemyParent);
            wave.GetComponent<Wave>().parent = enemyParent;
            _currentWaveNumber++;
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
