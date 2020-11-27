using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;


public class GameManager : Singleton<GameManager>
{
	[TabGroup("Inputs Axis")]
	public string ZoomAxis = "Mouse ScrollWheel";

	[TabGroup("Inputs Keyboard")]
	public KeyCode LaunchProbeKey;
	[TabGroup("Inputs Keyboard")]
	public KeyCode ReloadLevelKey;

	private void Update()
	{
		#region Reload Scene

		if (Input.GetKeyDown(ReloadLevelKey))
			SceneManager.LoadScene("MainScene");

		#endregion
	}
}
