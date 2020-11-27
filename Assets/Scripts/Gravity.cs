using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Gravity : MonoBehaviour
{
	[TitleGroup("Properties")]
	public float ConstantG = 5; // En Newton UnityUnits²/kg²
	public float Masse = 1;
	//public double ConstantG = 6f * 10e-11; // En Newton UnityUnits²/kg²
	//public double Masse = 6f * 10e24;

	[TitleGroup("Thrust on Start")]
	public bool ThrustOnstart = false;

	public Vector3 ThrustDirection;
	public float ThrustPower;
	public float ThrustTime;

	[TitleGroup("Options")]
	public bool IsAttractor = true;
	public bool IsAttracted = true;
	public bool UpdateGravities = false;

	[TitleGroup("Debug")][ReadOnly]
	public Vector2 Velocity;
	[ReadOnly]
	public Gravity[] Gravities;

	private void Start()
	{
		Gravities = FindObjectsOfType<Gravity>();

		if (ThrustOnstart)
			StartCoroutine(StartingThrust());
	}

	private void FixedUpdate()
	{
		if (UpdateGravities)
			Gravities = FindObjectsOfType<Gravity>();

		if (IsAttractor)
		{
			foreach (Gravity attractable in Gravities)
				if (attractable != this)
						Attract(attractable);
		}

		if (IsAttracted)
			transform.Translate(Velocity * Time.fixedDeltaTime, Space.World);
	}

	void Attract(Gravity attractable)
	{
		// Calcule de la direction.
		Vector2 direction = (Vector2)transform.position - (Vector2)attractable.transform.position;

		// Calcule de la distance. En Unity units
		float distance = direction.magnitude; // Retourne la longueur d'un vecteur.

		// Calcule de la force d'attraction. en Newton
		float weight = ConstantG * ((attractable.Masse * Masse) / (distance * distance)); // F = ((masseA * masseB) / distance(AB)²) * G
		//Debug.Log(gameObject.name + " = " + weight);

		// Calcule de la force finale. en Newton + Direction
		Vector2 gravityForce = direction.normalized * weight;

		if (attractable.IsAttracted)
			attractable.Velocity += gravityForce;
	}

	IEnumerator StartingThrust()
	{
		Debug.Log("=> Start StartingThrust");

		float startTime = Time.time;

		while (Time.time < startTime + ThrustTime)
		{
			Velocity += (Vector2)ThrustDirection * ThrustPower;

			yield return null;
		}

		Debug.Log("=> End StartingThrust");
	}
}
