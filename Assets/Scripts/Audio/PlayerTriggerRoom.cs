using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerRoom : MonoBehaviour
{
	public AK.Wwise.State gameplay;
	private void OnTriggerEnter(Collider in_other)
	{
		if (in_other.tag == "Player")
			AkSoundEngine.SetState("Enviro", "Castle");
			gameplay.SetValue();

	
	}
}
