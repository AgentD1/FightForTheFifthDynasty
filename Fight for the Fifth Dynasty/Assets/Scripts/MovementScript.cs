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

    public float defaultWalkDelay = 0.2f;
    public float walkDelay = 0.2f;
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
        
    }

    public void MoveUp()
    {
        spriteRenderer.sprite = facingUp;
        directionFacing = Vector2.up * walkDistance;
        moveScript.TryMoveInDirectionFacing();
    }

    public void MoveDown()
    {
        spriteRenderer.sprite = facingDown;
        directionFacing = Vector2.down * walkDistance;
        moveScript.TryMoveInDirectionFacing();
    }

    public void MoveLeft()
    {
        spriteRenderer.sprite = facingLeft;
        directionFacing = Vector2.left * walkDistance;
        moveScript.TryMoveInDirectionFacing();
    }

    public void MoveRight()
    {
        spriteRenderer.sprite = facingRight;
        directionFacing = Vector2.right * walkDistance;
        moveScript.TryMoveInDirectionFacing();
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