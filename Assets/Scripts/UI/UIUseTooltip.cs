using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUseTooltip : MonoBehaviour
{
    public Text useText;
    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.playerController.currentInteractable != null)
        {
            useText.color = Color.white;
            switch(GameController.instance.playerController.currentInteractable.interactionType)
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
            }
        }
        else
        {
            useText.color = Color.clear;
        }
    }
}
