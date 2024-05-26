using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;

    [SerializeField] private float damage;

    private Rigidbody2D body;

    AudioManager audioManager;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Initialize(float spawnSide)
    {
        if (spawnSide < -0.01f)
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else if (spawnSide > 0.01)
        {
            direction = Vector2.left;
            transform.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(direction.x * speed, body.velocity.y);

        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * Camera.main.aspect + 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioManager.PlaySound(audioManager.hurt);
            collision.GetComponent<Health>().TakeDamge(damage);
            Destroy(gameObject);
        }
    }
}
