using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerRoom : MonoBehaviour
{
	PlayerController playerController;


	private void OnTriggerEnter(UnityEngine.Collider in_other)
	{
		if (in_other.tag == "Player")
			AkSoundEngine.SetState("", "");
	}
}
