using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerVar player;
    private PlayerMov move;
    public float maxHealth = 10;
    public float health;
    public Image healthBar;

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
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            player.isDeath = true;
            move.allowMove = false;
            move.stamina = 0;
        }
    }
}
