using UnityEngine;

public class Rock : Target
{
    private Rigidbody2D rockRb;

    private void Start()
    {
        rockRb = GetComponent<Rigidbody2D>();
        Vector2 randMove = new Vector2(Random.Range(10, 20), Random.Range(10, 20));
        rockRb.AddForce(randMove);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Damage dmg = new Damage
            {
                dmgAmount = 20
            };

            collision.gameObject.SendMessage("ReceiveDamage", dmg);
            Destroy(gameObject);
        }
    }

    protected override void Death()
    {
        GameManager.instance.AddScore(10);
        base.Death();
    }
}
