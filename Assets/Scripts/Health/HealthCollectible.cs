using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private float existTime;
    private float existTimeCounter;

    AudioManager audioManager;

    private void Awake()
    {
        existTimeCounter = existTime;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        existTimeCounter -= Time.deltaTime;
        
        if (existTimeCounter < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            audioManager.PlaySound(audioManager.collectHeart);
            collision.gameObject.GetComponent<Health>().AddHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
