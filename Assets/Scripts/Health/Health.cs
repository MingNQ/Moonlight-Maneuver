using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;

    UIManager uiManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>(); // Find object hold UIManager Script
    }

    public void TakeDamge(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        anim.SetTrigger("hurt");
        if (currentHealth <= 0)
        {
            // Player dead
            if (!dead)
            {
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                uiManager.GameOver();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

}
