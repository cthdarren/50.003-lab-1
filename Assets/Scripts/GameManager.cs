using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager: MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject player;
    public PlayerHealth playerhealth;
    public GameObject boss;
    public GameObject oneheart;
    public GameObject twoheart;
    public GameObject threeheart;
    public TextMeshProUGUI scoreText;
    public float gameTimer;
    public void Awake()
    {
        gameTimer = Time.time;
        playerhealth = player.GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        switch (playerhealth.hp)
        {
            case 0:
                oneheart.SetActive(false);
                twoheart.SetActive(false);
                threeheart.SetActive(false);
                break;
            case 1:
                twoheart.SetActive(false);
                threeheart.SetActive(false);
                break;

            case 2:
                threeheart.SetActive(false);
                break;
        }
    }
    public void GameOver()
    {
        Debug.Log("Gameover");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        Debug.Log("Win");
        float score = Mathf.Abs(300 - (Time.time - gameTimer))/300 * 100000;
        score = Mathf.Round(score);
        scoreText.text = "Congratulations!\nYou win. \nYour Score:" + score.ToString();  
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame(int input)
    {
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("YIPPEEE");
        if (other.CompareTag("Player"))
        {
            Win();
        }
        
    }
}
