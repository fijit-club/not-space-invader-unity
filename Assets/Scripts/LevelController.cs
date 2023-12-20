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
    public static int Score;
    
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

    public int wavingIndex;
    
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float speedIncrementAmount;
    [SerializeField] private float maxBulletSpeed;
    [Space(10)]
    
    [Header("Circular Boss - Bullet Speed")]
    [SerializeField] private float initialCircularBossBulletSpeed;
    [SerializeField] private float circularBossBulletSpeedIncrement;
    [SerializeField] private float circularBossBulletMaxSpeed;
    [Space(10)]
    
    [Header("Circular Boss - Bullet Spawn Delay")]
    [SerializeField] private float circularBossBulletDelayInit;
    [SerializeField] private float circularBossBulletDelayDecrement;
    [SerializeField] private float circularBossBulletDelayMin;
    [Space(10)]
    
    [Header("Normal Boss - Bullet Speed")]
    [SerializeField] private float initBossBulletSpeed;
    [SerializeField] private float incrementBossBulletSpeed;
    [SerializeField] private float maxBossBulletSpeed;
    [Space(10)]
    
    [Header("Normal Boss - Bullet Spawn Delay")]
    [SerializeField] private float initBossBulletDelay;
    [SerializeField] private float decrementBossBulletDelay;
    [SerializeField] private float minBossBulletDelay;
    

    public void StopInvoke()
    {
        CancelInvoke(nameof(IncreaseScore));
    }
    
    public void ResetSpeed()
    {
        bulletSpeed = 10f;
        score = 0;
        Coins = 0;
        Score = 0;
        scoreText.text = "Score: " + score;
    }
    
    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnWave));
    }

    private void IncreaseScore()
    {
        score = Score;
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
        
        WaveData.CircularBossBulletSpeed = initialCircularBossBulletSpeed;
        WaveData.CircularBossBulletDelay = circularBossBulletDelayInit;
        WaveData.NormalBossBulletSpeed = initBossBulletSpeed;
        WaveData.NormalBossBulletDelay = initBossBulletDelay;
        StartSpawning();
        
        StartCoroutine(PlanetsCreation());
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnWave), .1f, 5f);
        
    }
    
    private void SpawnWave()
    {
        int i = 0;
        if (WaveData.WaveNumber > 5)
        {
            i = Random.Range(0, enemyWaves.Length);
        
            if (bulletSpeed < maxBulletSpeed)
                bulletSpeed += speedIncrementAmount;
        }
        else
            i = Random.Range(0, enemyWaves.Length - 3);
        
        GameObject wave = Instantiate(enemyWaves[i].wave, enemyParent);

        WaveData.WaveNumber++;
        
        if (i == enemyWaves.Length - 1 || i == enemyWaves.Length - 2 || i == enemyWaves.Length - 3)
        {
            wavingIndex = 0;

            var enemyMain = enemyWaves[i].wave.GetComponent<EnemyMain>();
            
            if (enemyMain)
                enemyMain.projectileParent = enemyParent;

            if (i == enemyWaves.Length - 2)
            {
                if (WaveData.CircularBossBulletSpeed < circularBossBulletMaxSpeed)
                    WaveData.CircularBossBulletSpeed += circularBossBulletSpeedIncrement;
                if (WaveData.CircularBossBulletDelay > circularBossBulletDelayMin)
                    WaveData.CircularBossBulletDelay -= circularBossBulletDelayDecrement;
            }

            if (i == enemyWaves.Length - 3 || i == enemyWaves.Length - 1)
            {
                if (WaveData.NormalBossBulletSpeed < maxBossBulletSpeed)
                    WaveData.NormalBossBulletSpeed += incrementBossBulletSpeed;
                if (WaveData.NormalBossBulletDelay > minBossBulletDelay)
                    WaveData.NormalBossBulletDelay -= decrementBossBulletDelay;
            }
            
            CancelInvoke(nameof(SpawnWave));
            
            return;
        }
        
        Wave waveComp = wave.GetComponent<Wave>();
        waveComp.parent = enemyParent;
        waveComp.enemySpeed = bulletSpeed;
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
