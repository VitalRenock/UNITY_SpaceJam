using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class CameraManager : Singleton<CameraManager>
{
	public Camera MainCamera;

	public float SpeedTranslation = 1f;
	public TrailRenderer trailTest;

	[Title("Zoom")]
	[ReadOnly]
	public float ZoomCamera;
	public float MinimumZoom;
	public float MaximumZoom;
	public float ZoomOnStart;
	[Range(0.01f, 100)]
	public float ZoomMultiplicator;

	private void Start()
	{
		// Set Zoom on start.
		MainCamera.orthographicSize = ZoomOnStart;
		ZoomCamera = MainCamera.orthographicSize;
		trailTest.widthMultiplier = ZoomCamera / 20f;
	}

	private void Update()
	{
		// Update Zoom.
		if (Input.GetAxis(GameManager.I.ZoomAxis) != 0)
		{
			ZoomCamera += -Input.GetAxis("Mouse ScrollWheel") * ZoomMultiplicator;
			trailTest.widthMultiplier = ZoomCamera / 20f;

			if (ZoomCamera < MinimumZoom)
				ZoomCamera = MinimumZoom;
			if (ZoomCamera > MaximumZoom)
				ZoomCamera = MaximumZoom;
		}
		MainCamera.orthographicSize = ZoomCamera;

		if (Input.GetMouseButton(2))
			MainCamera.transform.Translate(new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * ZoomCamera * Time.deltaTime);

	}
}
