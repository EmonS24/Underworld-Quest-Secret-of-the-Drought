using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerVar player;
    private PlayerMov move;
    public float maxHealth;
    public float health;
    public Image healthBar;

    public bool isDamaged { get; private set; }

    void Start()
    {
        player = GetComponent<PlayerVar>();
        move = GetComponent<PlayerMov>();
        health = maxHealth;
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }

        if (isDamaged)
        {
            isDamaged = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        isDamaged = true;  
        
        if (health <= 0)
        {
            health = 0;
            player.isDeath = true;
            move.allowMove = false;
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Respawn()
    {
        health = maxHealth;
    }
}
