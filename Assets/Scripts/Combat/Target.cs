using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;
    public int maxHealth;
    [SerializeField] GameObject deathParticles;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        health -= dmg.dmgAmount;

        ShowHit();

        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    protected virtual void ShowHit()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
    }

    protected virtual void Death()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
