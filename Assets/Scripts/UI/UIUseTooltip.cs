using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUseTooltip : MonoBehaviour
{
    public Text useText;
    public bool playerTwo = false;
    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }
    // Update is called once per frame
    void Update()
    {
        if (getTargetPlayer().currentInteractable != null)
        {
            useText.color = Color.white;
            switch(getTargetPlayer().currentInteractable.interactionType)
            {
                case InteractionType.Generic:
                    useText.text = "E: Use";
                    break;
                case InteractionType.PickUp:
                    useText.text = "E: Pick Up";
                    break;
                case InteractionType.Collect:
                    useText.text = "E: Collect";
                    break;
                case InteractionType.Open:
                    useText.text = "E: Open";
                    break;
                case InteractionType.Torch:
                    useText.text = "E: Take Torch";
                    break;
            }
        }
        else
        {
            useText.color = Color.clear;
        }
    }
}
