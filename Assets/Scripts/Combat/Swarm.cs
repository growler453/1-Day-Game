using UnityEngine;

public class Swarm : Target
{
    private AudioSource targetAudio;
    [SerializeField] EnemyBullet bullet;
    private float speed = 3f;
    private Transform player;
    private Vector2 dir;
    private float angle;
    private float shotTimer = 1;
    private float shotDelay = 2f;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        targetAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance > 5)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        dir = player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (distance < 8 && shotTimer <= 0) Shoot();

        if (shotTimer > 0) shotTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Damage dmg = new Damage
            {
                dmgAmount = 25
            };

            collision.gameObject.SendMessage("ReceiveDamage", dmg);
            if (collision.rigidbody != null)
                collision.rigidbody.AddForce(transform.up * 10);
        }
    }

    private void Shoot()
    {
        targetAudio.Play();
        Instantiate(bullet, transform.position, transform.rotation);
        shotTimer = shotDelay;
    }

    protected override void Death()
    {
        GameManager.instance.AddScore(50);
        GameManager.instance.UpdateEnemyCount();
        base.Death();
    }
}
