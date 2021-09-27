using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite facingLeft;
    public Sprite facingRight;
    public Sprite facingUp;
    public Sprite facingDown;

    public float defaultWalkDelay = 0.2f;
    public float walkDelay = 0.2f;
    public int u = 1;
    public float walkDistance = 0.5f;

    Vector2 directionFacing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        walkDelay -= Time.deltaTime;

        if (walkDelay <= 0)
        {
            if (u == 1)
            {
                spriteRenderer.sprite = facingUp;
                directionFacing = Vector2.up * walkDistance;
                TryMoveInDirectionFacing();

                spriteRenderer.sprite = facingUp;
                directionFacing = Vector2.up * walkDistance;
                TryMoveInDirectionFacing();

                u = 0;
            }
            else if (u != 1)
            {
                spriteRenderer.sprite = facingDown;
                directionFacing = Vector2.down * walkDistance;
                TryMoveInDirectionFacing();

                spriteRenderer.sprite = facingDown;
                directionFacing = Vector2.down * walkDistance;
                TryMoveInDirectionFacing();

                u = 1;
            }
        }
        void TryMoveInDirectionFacing()
        {

                transform.Translate(directionFacing);
                walkDelay = defaultWalkDelay;

        }
    }
}
