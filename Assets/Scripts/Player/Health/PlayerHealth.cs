using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerVar player;
    private PlayerMov move;
    public float maxHealth;
    public float health;
    public Image healthBar;

    // Indikator untuk memeriksa apakah pemain baru saja menerima damage
    public bool isDamaged { get; private set; }

    void Start()
    {
        player = GetComponent<PlayerVar>();
        move = GetComponent<PlayerMov>();
        health = maxHealth;
    }

    void Update()
    {
        // Memperbarui health bar
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }

        // Reset indikator isDamaged setelah setiap frame
        if (isDamaged)
        {
            isDamaged = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        isDamaged = true;  // Menandakan bahwa pemain baru saja menerima damage
        
        if (health <= 0)
        {
            health = 0;
            player.isDeath = true;
            move.allowMove = false;
            move.stamina = 0;
        }
    }

    public void AddHealth(float _value)
    {
        health = Mathf.Clamp(health + _value, 0, maxHealth);
    }

    public void Respawn()
    {
        AddHealth(maxHealth);
        player.isDeath = false;
        move.allowMove = true;
        move.stamina = move.maxStamina;
    }
}
