using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerRoom : MonoBehaviour
{
	public AK.Wwise.State gameplay;
	private void OnTriggerEnter(Collider in_other)
	{
		var isPlayer = in_other.GetComponent<PlayerController>();
		if (isPlayer)
			AkSoundEngine.SetState("Enviro", "Castle");
			gameplay.SetValue();

	
	}
}
