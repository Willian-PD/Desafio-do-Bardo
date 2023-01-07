using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public GameObject titleDisplay;
    public GameObject livesSprite;
    public Text liveText;
    public Text boostText;
    public Text scoreText;
    public int score;

    // Atualiza a contagem atual da vida do player
    public void UpdateLives(float currentLives)
    {
        liveText.text = "Vidas do player: x" + currentLives;
    }

    // Atualiza a contagem atual do boost do player
    public void UpdateBoost(float currentBoost)
    {
        boostText.text = "Boost: x" + currentBoost;
    }

    // Atualiza a contagem atual do score do player
    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseScore()
    {
        score -= 1;
        scoreText.text = "Score: " + score;
    }

    public void InitScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
