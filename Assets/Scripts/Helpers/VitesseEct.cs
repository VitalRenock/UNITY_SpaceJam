using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseEct : MonoBehaviour
{
	public Transform ObjectA;
	public Transform ObjectB;

	public Vector2 PositionA;
	public Vector2 PositionB;

	public Vector2 Direction;
	public float Distance;
	public float DistanceMagnitude;

	public float Speed;

	public Vector2 Velocity;

	public Vector2 Displacement;


	private void Update()
	{
		PositionA = ObjectA.position;
		PositionB = ObjectB.position;


		#region Calcule Direction

		Direction = PositionB - PositionA;

		#endregion


		#region Calcule Distance

		// Mathf.Pow = Élévation à la puissance.
		// Mathf.Sqrt = Racine carré.

		DistanceMagnitude = Mathf.Sqrt(Mathf.Pow(Direction.x, 2) + Mathf.Pow(Direction.y, 2));
		// ==
		Distance = Direction.magnitude;

		#endregion


		#region Calcule Velocity

		Velocity = Direction.normalized * Speed;

		#endregion


		#region Calcule Displacement

		Displacement = Velocity * Time.deltaTime;

		transform.Translate((Vector3)Displacement);         // == transform.position = (Vector2)transform.position + Displacement;

		#endregion
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere((Vector3)Direction, 0.1f);
	}
}
