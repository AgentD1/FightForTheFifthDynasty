using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite facingLeft;
    public Sprite facingRight;
    public Sprite facingUp;
    public Sprite facingDown;
    public MovementScript moveScript;

    public float defaultWalkDelay = 0.6f;
    public float walkDelay = 0.6f;
    public float walkDistance = 0.5f;

    public int up = 0;
    public int down = 0;
    public int left = 0;
    public int right = 0;

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
            if (up >= 1)
            {
                MoveUp();
                up = up - 1;
            }

            else if (down >= 1)
            {
                MoveDown();
                down = down - 1;
            }

            else if (left >= 1)
            {
                MoveLeft();
                left = left - 1;
            }

            else if (right >= 1)
            {
                MoveRight();
                right = right - 1;
            }

            walkDelay = defaultWalkDelay;
        }
    }

    public void MoveUp()
    {
        if (up > 0)
        {
            spriteRenderer.sprite = facingUp;
            directionFacing = Vector2.up * walkDistance;
            transform.Translate(directionFacing);
        }
    }

    public void MoveDown()
    {
        if (down > 0)
        {
            spriteRenderer.sprite = facingDown;
            directionFacing = Vector2.down * walkDistance;
            transform.Translate(directionFacing);
        }
    }

    public void MoveLeft()
    {
            if (left > 0)
            {
                spriteRenderer.sprite = facingLeft;
                directionFacing = Vector2.left * walkDistance;
                transform.Translate(directionFacing);
            }
    }

    public void MoveRight()
    {
        if (right > 0)
        {
                    spriteRenderer.sprite = facingRight;
                    directionFacing = Vector2.right * walkDistance;
                    transform.Translate(directionFacing);
        }
    }

    public void TryMoveInDirectionFacing()
    {
        if (!Physics2D.CircleCast((Vector2)transform.position + directionFacing, 0.25f, directionFacing, 0.25f))
        {
            transform.Translate(directionFacing);
            walkDelay = defaultWalkDelay;
        }
    }
}
