using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector2 pos;
    private float speed = 8.0f;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        pos.y = Mathf.Lerp(transform.position.y, player.position.y, speed * Time.deltaTime);
        pos.x = Mathf.Lerp(transform.position.x, player.position.x, speed * Time.deltaTime);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
