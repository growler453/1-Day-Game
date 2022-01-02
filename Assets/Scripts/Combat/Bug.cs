using UnityEngine;

public class Bug : Target
{
    private float speed = 5f;
    private Transform player;
    private Vector2 dir;
    private float angle;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        dir = player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
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
        }
    }

    protected override void Death()
    {
        GameManager.instance.AddScore(25);
        GameManager.instance.UpdateEnemyCount();
        base.Death();
    }
}
