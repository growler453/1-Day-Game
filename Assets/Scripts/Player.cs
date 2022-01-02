using UnityEngine;
using UnityEngine.UI;

public class Player : Target
{
    [SerializeField] Image cursor;
    [SerializeField] Bullet bullet;
    private AudioSource targetAudio;

    private Vector3 moveDir;
    private float speed = 3.5f;

    private float angle;
    private Vector3 dir;

    private float shotDelay = 1f;
    private float delayAmount = 0.1f;

    Vector3 mousePos;

    private void Start()
    {
        targetAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Mouse Position
        mousePos = Input.mousePosition;
        cursor.transform.position = mousePos;

        // Move Direction
        moveDir = Camera.main.ScreenToWorldPoint(mousePos);
        moveDir.z = 0f;

        // Rotation
        dir = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // Movement
        float distance = Vector2.Distance(moveDir, transform.position);
        if (distance > 20) distance = 20;

        if (distance > 2.5f)
            transform.position = Vector2.MoveTowards(transform.position, moveDir, speed * (distance/2) * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && shotDelay <= 0)
            Fire();

        if (shotDelay > 0) shotDelay -= Time.deltaTime;
    }

    protected override void Death()
    {
        GameManager.instance.Respawn();
    }

    private void Fire()
    {
        targetAudio.Play();
        Instantiate(bullet, transform.position, transform.rotation);
        shotDelay = delayAmount; // use gamemanager method to change gun behaviors and shots fired
    }

    public void ChangeShotBehavior()
    {
        // fill in using gamemanager
    }
}
