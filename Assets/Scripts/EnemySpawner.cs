using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 1f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();


    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    public static EnemySpawner main;

    public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }


    private void Start()
    {
        StartCoroutine(StartWave());

        StartCoroutine(ShowWavePanel(1));  // Show the panel for 1 second





    }


    // Update is called once per frame
    void Update()
    {
      

        if (!isSpawning)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;
        foreach (Text text in LevelScript.main.InfoPanel.GetComponentsInChildren<Text>())
        {
            
            if (text.name == "EnemiesAlive")
            {
                text.text = "Enemies Alive: " + enemiesAlive;
            }
            else if (text.name == "EnemiesToSpawn")
            {
                text.text = "Enemies Left to Spawn: " + enemiesLeftToSpawn;
            }


        }


        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn >0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;

        currentWave++;
        StartCoroutine(ShowWavePanel(1));  // Show and hide the panel when the wave ends

        EnemyMovement.MoveSpeed += 0.2f;
        enemiesPerSecond += 0.2f;

        StartCoroutine(StartWave());
    }


    private void EnemyDestroyed()
    {
        enemiesAlive--;
     
    }

    private void SpawnEnemy()
    {
        //check if gamejobject is not destroyed before spawning

        if (enemyPrefabs.Length == 0)
        {
            return;
        }

        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        
        
        GameObject Enemyy = Instantiate(prefabToSpawn, LevelScript.main.startPoint.position, Quaternion.identity);
        enemies.Add(Enemyy);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

    }
    private IEnumerator ShowWavePanel(float duration)
    {
        LevelScript.main.WavePanel.SetActive(true);
        LevelScript.main.WavePanel.GetComponentInChildren<Text>().text = "Wave: " + currentWave;
        yield return new WaitForSeconds(duration);  // Wait for the specified duration
        LevelScript.main.WavePanel.SetActive(false);
    }

    private int EnemiesPerWave()
    {
        int result = Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        return result;
    }

    
}
