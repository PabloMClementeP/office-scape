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
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        controller.Move(Vector3.forward * speed * Time.deltaTime);

        MoveXPos();

        CheckInputs();

        controller.Move(moveCharacter * Time.deltaTime);
    }



    private void MoveXPos()
    {
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



    private void CheckInputs()
    {
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
}
