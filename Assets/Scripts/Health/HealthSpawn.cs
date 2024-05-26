using UnityEngine;

public class HealthSpawn : MonoBehaviour
{
    [Header("Coordinates")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float spawnY;

    [Header("Object")]
    [SerializeField] private GameObject heart;
    [SerializeField] private float spawnTime;
    private float spawnTimeCounter;

    private void Awake()
    {
        spawnTimeCounter = spawnTime;
    }

    private void Update()
    {
        spawnTimeCounter -= Time.deltaTime;

        if (spawnTimeCounter <= 0)
        {
            Spawn();
            spawnTimeCounter = spawnTime;
        }
    }

    private void Spawn()
    {
        float spawnX = Random.Range(minX, maxX);
        GameObject currHeartObject = GameObject.Instantiate(heart, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        currHeartObject.name = "Health";
    }
}
