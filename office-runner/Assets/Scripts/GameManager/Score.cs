using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float score = 0.0f;     // game score

    // Difficulty variables
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 40;


    private PlayerMotor playerM;
    public bool isDead = false;
    
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
