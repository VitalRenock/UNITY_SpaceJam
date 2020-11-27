using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Probe : MonoBehaviour
{
	public ThrustProps Thrust;

	Gravity Gravity;
	Coroutine RoutineThrust;

	private void Awake()
	{
		Gravity = GetComponent<Gravity>();
		Thrust.Power = Ship.I.Thrust.Power;
		Thrust.Time = Ship.I.Thrust.Time;

		// Reset Timer
		Thrust.Timer.StartTime = Time.time;
		Thrust.Timer.CurrentTime = Thrust.Timer.StartTime;
	}

	private void Start()
	{
		//RoutineThrust = StartCoroutine(Thrusting());
	}

	private void FixedUpdate()
	{
		if (Thrust.Timer.CurrentTime <= Thrust.Timer.StartTime + Thrust.Time)
			Thrusting();
	}

	void Thrusting()
	{
		// Maj Speed
		Thrust.Speed += Thrust.Power * Time.fixedDeltaTime;

		// Maj Direction
		Thrust.Direction = (Vector2)transform.up;

		// Maj Velocity
		Thrust.Velocity = Thrust.Direction * Thrust.Speed;

		// Apply Velocity
		Gravity.Velocity += Thrust.Velocity;

		Thrust.Timer.CurrentTime += Time.fixedDeltaTime;
	}

	//IEnumerator Thrusting()
	//{
	//	Debug.Log("=> Start Thrusting");

	//	// Reset Timer
	//	Thrust.Timer.StartTime = Time.time;
	//	Thrust.Timer.CurrentTime = Thrust.Timer.StartTime;

	//	while (Thrust.Timer.CurrentTime <= Thrust.Timer.StartTime + Thrust.Time)
	//	{
	//		// Maj Speed
	//		Thrust.Speed += Thrust.Power * Time.deltaTime;

	//		// Maj Direction
	//		Thrust.Direction = (Vector2)transform.up;

	//		// Maj Velocity
	//		Thrust.Velocity = Thrust.Direction * Thrust.Speed;

	//		// Apply Velocity
	//		Gravity.Velocity += Thrust.Velocity;

	//		// Maj Timer
	//		Thrust.Timer.CurrentTime += Time.deltaTime;
	//		yield return null;
	//	}

	//	RoutineThrust = null;
	//	Debug.Log("=> End Thrusting");
	//}
}
