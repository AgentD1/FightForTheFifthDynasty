using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public MovementScript moveScript;

    public float defaultWalkDelay = 0.6f;
    public float walkDelay = 0.6f;
    public float walkDistance = 0.5f;

    bool goingRight = true;
    bool goingLeft = false;
    bool goingUp = false;
    bool goingDown = false;
    bool notRight = false;
    bool finished = false;

    Vector2 directionFacing;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovementScript>();
        moveScript.up = 2;
        moveScript.down = 0;
        moveScript.right = 4;
        moveScript.left = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (moveScript.up == 0 && moveScript.down == 0)
        {
            moveScript.up = 2;
            moveScript.down = 2;
        }*/

        if (moveScript.right == 0)
        {
            goingRight = false;
        }
        if (moveScript.up == 0)
        {
            goingUp = false;
        }

        if (finished == true && moveScript.up == 0)
        {
            goingRight = true;
            goingLeft = false;
            goingUp = false;
            goingDown = false;
            notRight = false;
            finished = false;

            moveScript.up = 2;
            moveScript.right = 4;
        }

        if (goingUp == false && notRight == false && goingRight == false)
        {
            notRight = true;
            goingDown = true;
            moveScript.down = 4;
        }
        else if (goingDown == true && moveScript.down == 0)
        {
            if (goingLeft == false)
            {
                moveScript.left = 4;
            }

            goingLeft = true;

            if (moveScript.left == 0 && finished == false) 
            {
                moveScript.up = 2;
                finished = true;
            }
        }




    }
}
