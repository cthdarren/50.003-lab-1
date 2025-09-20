using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float score;
    public TextMeshProUGUI scoreText; 

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Congratulations! You win. \nYour score:" + score.ToString();  
    }
}
