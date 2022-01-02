using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    private Vector2 spawnPos;
    public float spawnDelay = 1.5f;
    private int xBound = 80;
    private int yBound = 65;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        GameManager.instance.SetEnemyCount(enemies.Length);
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            yield return new WaitForSeconds(spawnDelay);
            spawnPos = new Vector2(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound));
            float distance = Vector2.Distance(GameObject.Find("Player").transform.position, spawnPos);
            if (distance < 4) spawnPos = new Vector2(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound));
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
