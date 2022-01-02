using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        sceneIndex = 1;

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private int sceneIndex;
    private int enemiesLeft;
    private int score;
    private float multiplier;
    [SerializeField] Text scoreText;
    [SerializeField] Text enemyCountText;
    [SerializeField] Text victoryText;
    [SerializeField] Text winText;
    [SerializeField] GameObject UI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Main")
                LoadMenu();
            else
                Application.Quit();
        }

        if (multiplier > 0) multiplier -= Time.deltaTime;
    }

    public void AddScore(int amount)
    {
        score += amount * (Mathf.RoundToInt(multiplier) + 1);
        if (multiplier < 2) multiplier += 1;
        scoreText.text = score.ToString();
    }

    public void SetEnemyCount(int amount)
    {
        enemiesLeft = amount;
        enemyCountText.text = enemiesLeft.ToString();
    }

    public void UpdateEnemyCount()
    {
        enemiesLeft -= 1;
        enemyCountText.text = enemiesLeft.ToString();
        if (enemiesLeft <= 0 && sceneIndex < 4)
            StartCoroutine(Victory()); // call upgrade method and show menu to choose and then load level
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level " + sceneIndex);
        sceneIndex += 1;
        UI.SetActive(true);
        Cursor.visible = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main");
        sceneIndex -= 1;
        UI.SetActive(false);
        winText.gameObject.SetActive(false);
        victoryText.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    public IEnumerator Victory()
    {
        AddScore(500);
        victoryText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        victoryText.gameObject.SetActive(false);
        LoadLevel();
    }

    public void EndGame()
    {
        AddScore(500);
        winText.gameObject.SetActive(true);
        sceneIndex = 2;
    }
}
