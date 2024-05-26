using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private GameObject player;
    [SerializeField] private float timeSpawn;
    private float timeSpawnCounter;

    [SerializeField] private float timeIncreaseLevel;
    private float timeLevelCounter;

    private float spawnX;
    private float spawnY;

    private void Awake()
    {
        timeSpawnCounter = timeSpawn;
        timeLevelCounter = timeIncreaseLevel;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawnCounter -= Time.deltaTime;
        timeLevelCounter -= Time.deltaTime;

        // If it's time to spawn => spawn
        if (timeSpawnCounter <= 0)
        {
            // Object focus on player position to spawn
            spawnY = player.transform.position.y;
            Spawn();
            timeSpawnCounter = timeSpawn;
        }

        // If it's time to increase hard level
        if (timeLevelCounter <= 0 && timeSpawn >= 0.5f)
        {
            timeSpawn -= 0.1f;
            timeLevelCounter = timeIncreaseLevel;
        }
    }

    void Spawn()
    {
        // Randowm side spawn
        spawnX = Random.Range(0, 2) == 0 ?
            Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1 :
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1;

        // Add game object
        int monsterType = Random.Range(0, monsterPrefabs.Length);
        GameObject minion = GameObject.Instantiate(monsterPrefabs[monsterType], new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        minion.name = monsterType == 0 ? "Monster_1" : "Monster_2";
        minion.GetComponent<MonsterMovement>().Initialize(spawnX);
    }
}
