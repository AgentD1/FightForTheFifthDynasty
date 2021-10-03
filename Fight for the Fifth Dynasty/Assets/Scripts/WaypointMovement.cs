using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour {
	public List<Vector2> waypoints;
	public float moveDelay;
	public float waypointWaitDelay;
	public bool loopBackwards;

	int waypointListIndex;
	float timeRemainingToAction;
	List<Vector2> localWaypoints;

	public void Start() {
		localWaypoints = new List<Vector2>();
		foreach (Vector2 waypoint in waypoints) {
			localWaypoints.Add(new Vector2(transform.position.x + waypoint.x, transform.position.y + waypoint.y));
		}
	}

	public void Update() {
		timeRemainingToAction -= Time.deltaTime;
		if (timeRemainingToAction <= 0) {
			Vector2 currentTarget = localWaypoints[waypointListIndex];
			if ((Vector2)transform.position == currentTarget) {
				if (waypointListIndex == localWaypoints.Count - 1) {
					if (loopBackwards) {
						localWaypoints.Reverse();
					}
					waypointListIndex = 0;
				} else {
					waypointListIndex++;
				}

				currentTarget = localWaypoints[waypointListIndex];

				if (waypointWaitDelay != 0) {
					timeRemainingToAction = waypointWaitDelay;
					return;
				}
			}

			if (currentTarget.x < transform.position.x) {
				transform.Translate(-1, 0, 0);
			} else if (currentTarget.x > transform.position.x) {
				transform.Translate(1, 0, 0);
			} else if (currentTarget.y < transform.position.y) {
				transform.Translate(0, -1, 0);
			} else if (currentTarget.y > transform.position.y) {
				transform.Translate(0, 1, 0);
			}

			timeRemainingToAction = moveDelay;
		}
	}
}
