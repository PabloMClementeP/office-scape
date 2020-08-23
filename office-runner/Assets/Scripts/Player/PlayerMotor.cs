using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    CharacterController controller;

    private int line = 1;           // current line
    private int targetLine = 1;     // next line
    private bool canMove = true;    // if can move next line
    private Vector3 moveCharacter = Vector3.zero;

    private Vector3 moveVector;

    public float speed = 8.0f;

    // Animation camera 
    private float animationDuration = 2.0f;
    private float startTime;

    private bool isDead = false;    // is player is dead check

    private Score score;




    void Start()
    {
        controller = GetComponent<CharacterController>();
        score = GameObject.FindObjectOfType<Score>();
        startTime = Time.time;
    }

    
    void Update()
    {

        // the player cannot be moved until the camera animation is finished
        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        // is player dead exit update
        if (isDead)
            return;


        CheckInputs();

        MoveXPos();

        moveCharacter.z = speed;

        controller.Move(moveCharacter * Time.deltaTime);

        CheckCollision();
    }


    #region Methods called in Update

    private void CheckInputs()
    {
        /*
         * Check if key left or right is dawn
         * and can move in x axis
         */
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove && line > 0)
        {
            targetLine--;
            canMove = false;
            moveCharacter.x = -5;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && canMove && line < 2)
        {
            targetLine++;
            canMove = false;
            moveCharacter.x = 5;
        }
    }


    private void MoveXPos()
    {
        /*
         * setup x axis in moveCharacter 
         */
        Vector3 pos = gameObject.transform.position;
        if (!line.Equals(targetLine))
        {
            if (targetLine == 0 && pos.x < -2.5)
            {
                gameObject.transform.position = new Vector3(-2.5f, pos.y, pos.z);
                line = targetLine;
                canMove = true;
                moveCharacter.x = 0;
            }
            else if (targetLine == 1 && (pos.x > 0 || pos.x < 0))
            {
                if (line == 0 & pos.x > 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    canMove = true;
                    moveCharacter.x = 0;
                }
                else if (line == 2 & pos.x < 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    canMove = true;
                    moveCharacter.x = 0;
                }
            }
            else if (targetLine == 2 && (pos.x > 2))
            {
                gameObject.transform.position = new Vector3(2f, pos.y, pos.z);
                line = targetLine;
                canMove = true;
                moveCharacter.x = 0;
            }
        }
    }


    private void CheckCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        score.isDead = true;
        Debug.Log("Esta muerto!!");
    }
    #endregion

    // change the speed by adding a passed value as a parameter
    public void SetSpeed(float modifier)
    {
        speed += modifier;
    }
}
