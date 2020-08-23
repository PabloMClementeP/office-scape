using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score = 0.0f;     // game score

    // Difficulty variables
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 40;


    private PlayerMotor playerM;
    public bool isDead = false;

    // Text on gui Score - Lvl and NxtLvl
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI nxtLvlText;


    void Start()
    {
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        isDead = false;
    }


    void Update()
    {
        if (isDead)
            return;

        if (score >= scoreToNextLevel)
            LevelUp();

        // 
        score += Time.deltaTime * difficultyLevel;
        Debug.Log("Level: " + difficultyLevel + " -- Score: " + (int)score + " -- NextLvl: " + scoreToNextLevel);

        // print score and lvl on Gui
        scoreText.text = "Score: " + ((int)score).ToString();
        lvlText.text = "Level: " + difficultyLevel.ToString();
        nxtLvlText.text = "Next Lvl to: " + scoreToNextLevel.ToString();
        
    }

    private void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        playerM.SetSpeed(difficultyLevel);

    }
}
