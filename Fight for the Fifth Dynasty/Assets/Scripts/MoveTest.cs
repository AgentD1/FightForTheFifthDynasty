using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public MovementScript moveScript;

    public Sprite facingLeft;
    public Sprite facingRight;
    public Sprite facingUp;
    public Sprite facingDown;

    public float defaultWalkDelay = 0.2f;
    public float walkDelay = 0.2f;
    public int u = 3;
    public float walkDistance = 0.5f;

    Vector2 directionFacing;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        walkDelay -= Time.deltaTime;

        if (walkDelay <= 0)
        {
            if (u == 3)
            {
                moveScript.MoveUp();
                moveScript.TryMoveInDirectionFacing();
                u = 2;
            }
            else if (u == 2)
            {
                moveScript.MoveUp();
                moveScript.TryMoveInDirectionFacing();
                u = 1;
            }
            else if (u == 1)
            {
                moveScript.MoveDown();
                moveScript.TryMoveInDirectionFacing();
                u = 0;
            }
            else if (u == 0)
            {
                moveScript.MoveDown();
                moveScript.TryMoveInDirectionFacing();
                u = 3;
            }
        }
    }
}
