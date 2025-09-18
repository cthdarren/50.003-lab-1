using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject player;
    public GameObject boss;
    public void GameOver()
    {
        Debug.Log("Gameover");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        Debug.Log("Win");
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
