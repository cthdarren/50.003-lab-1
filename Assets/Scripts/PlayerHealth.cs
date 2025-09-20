using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthUI;
    private GameManager gameManager;
    public float hp;
    public bool isDead = false;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Update()
    {
        if (!isDead) { 
            if (hp <= 0) {
                isDead = true;
                healthUI.color = Color.black;
                gameManager.GameOver();
            }
        }
        healthUI.color = Color.white;
    }

    public void TakeDamage()
    {
        hp -= 1;
    }
}
