using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float defaultWalkDelay = 0.05f;
	public float walkDelay = 0.05f;
    public float walkDistance = 0.125f;

    public Sprite facingLeft;
	public Sprite facingRight;
	public Sprite facingUp;
	public Sprite facingDown;

	Vector2 directionFacing;

	public static float playerX;
	public static float playerY;
    public float targetX;
    public float targetY;
    public float enemyX;
    public float enemyY;
	public static List<GameObject> playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		targetX = Player.playerX;
        targetY = Player.playerY;

        enemyX = transform.position.x;
		enemyY = transform.position.y;

        walkDelay -= Time.deltaTime;

        if (walkDelay <= 0) {
			if (targetY > enemyY) {
				Debug.Log("Up");
				spriteRenderer.sprite = facingUp;
				directionFacing = Vector2.up * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (targetY < enemyY) {
				Debug.Log("Down");
				spriteRenderer.sprite = facingDown;
				directionFacing = Vector2.down * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (targetX < enemyX) {
				Debug.Log("Left");
				spriteRenderer.sprite = facingLeft;
				directionFacing = Vector2.left * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (targetX > enemyX) {
				Debug.Log("Right");
				spriteRenderer.sprite = facingRight;
				directionFacing = Vector2.right * walkDistance;
				TryMoveInDirectionFacing();
			}

            walkDelay = defaultWalkDelay;
		}
    }

    void TryMoveInDirectionFacing() {
			transform.Translate(directionFacing);
	}
}
