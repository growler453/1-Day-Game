using UnityEngine;

public class Asteroid : Target
{
    private SpriteRenderer sprite;
    public Sprite[] sprites;
    public GameObject[] rocks;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[Random.Range(0, sprites.Length - 1)];
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
            if (collision.rigidbody != null)
                collision.rigidbody.AddForce(transform.up * 10);
            Destroy(gameObject);
        }
    }

    protected override void Death()
    {
        foreach (GameObject rock in rocks)
        {
            Instantiate(rock, transform.position, transform.rotation);
        }
        GameManager.instance.AddScore(30);
        base.Death();
    }
}
