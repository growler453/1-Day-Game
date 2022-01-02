using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    public int damage;
    public float knockback;

    private void Awake() => bulletRb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        float speed = 8f;
        bulletRb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Damage dmg = new Damage
        {
            dmgAmount = damage
        };

        collision.gameObject.SendMessage("ReceiveDamage", dmg);

        if (collision.attachedRigidbody != null)
            collision.attachedRigidbody.AddForce(transform.up * knockback);

        Destroy(gameObject);
    }
}
