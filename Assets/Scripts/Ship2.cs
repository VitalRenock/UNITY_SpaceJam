using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Ship2 : Singleton<Ship2>
{
	[Title("Thrust")]
	public bool ThrustOnstart = false;
	[ReadOnly]
	public bool InThrust = false;
	public ThrustProps Thrust;

	[Title("RCS")]
	public float AngSpeed;
	public bool AutoRotate = false;

	[Title("Probes")][AssetsOnly]
	public GameObject ProbePrefab;

	Coroutine RoutineTimer;
	Gravity Gravity;

	private void Start()
	{
		Gravity = GetComponent<Gravity>();

		if (ThrustOnstart)
			StartThrust();
	}

	private void Update()
	{
		#region Rotation

		float angle = 0;
		if (Input.GetKey(KeyCode.Q))
			angle += 1;
		if (Input.GetKey(KeyCode.D))
			angle += -1;

		angle *= AngSpeed * Time.deltaTime;

		transform.Rotate(Vector3.forward, angle);

		#endregion

		#region Start Thrust

		if (Input.GetKeyDown(KeyCode.Z))
			StartThrust();

		#endregion

		#region Launching Probe

		if (Input.GetKeyDown(GameManager.I.LaunchProbeKey))
			Instantiate(ProbePrefab, transform.position, transform.localRotation);

		#endregion
	}

	void StartThrust()
	{
		if (!InThrust)
		{
			InThrust = true;

			// Activate Gravity Effect.
			Gravity.IsAttracted = true;

			// Reset Timer.
			Thrust.Timer.StartTime = Time.time;
			Thrust.Timer.CurrentTime = Thrust.Timer.StartTime;
		}
		else
			Debug.Log("Thrust en cours!");
	}

	private void FixedUpdate()
	{
		if (InThrust)
			if (Thrust.Timer.CurrentTime <= Thrust.Timer.StartTime + Thrust.Time)
			{
				// Maj Timer
				Thrust.Timer.CurrentTime += Time.fixedDeltaTime;

				ThrustingShip();
			}
			else if (Thrust.Timer.CurrentTime > Thrust.Timer.StartTime + Thrust.Time)
				InThrust = false;
	}

	void ThrustingShip()
	{
		// Auto Rotate Options
		if (AutoRotate)
		{
			float angle = 1;
			angle *= AngSpeed * Time.fixedDeltaTime;
			transform.Rotate(Vector3.forward, angle);
		}

		// Maj Speed
		Thrust.Speed += Thrust.Power * Time.fixedDeltaTime;

		// Maj Direction
		Thrust.Direction = (Vector2)transform.up;

		// Maj Velocity
		Thrust.Velocity = Thrust.Direction * Thrust.Speed;

		// Apply Velocity
		Gravity.Velocity += Thrust.Velocity;
	}
}

[System.Serializable]
public class ThrustProps
{
	[TabGroup("Thrust")]
	public float Power;
	[TabGroup("Thrust")]
	public float Time;
	[Space(20)]
	[TabGroup("Thrust")][ReadOnly]
	public Vector2 Direction;
	[TabGroup("Thrust")][ReadOnly]
	public float Speed;
	[TabGroup("Thrust")][ReadOnly]
	public Vector2 Velocity;
	[TabGroup("Thrust")][ReadOnly]
	public TimerProps Timer;
}

[System.Serializable]
public class TimerProps
{
	[TabGroup("Timer")][ReadOnly]
	public float StartTime;
	[TabGroup("Timer")][ReadOnly]
	public float CurrentTime;
}