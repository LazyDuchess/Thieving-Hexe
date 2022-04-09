using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDashBar : MonoBehaviour
{
    private UIBar bar;
    private float lastDash;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<UIBar>();
        bar.maxValue = GameController.instance.playerController.dashTime;
        bar.minValue = 0f;
        bar.SetValue(GameController.instance.playerController.GetDashStamina());
        lastDash = GameController.instance.playerController.GetDashStamina();
    }

    private void Update()
    {
        if (GameController.instance.playerController.GetDashStamina() != lastDash)
        {
            bar.SetValue(GameController.instance.playerController.GetDashStamina());
            lastDash = GameController.instance.playerController.GetDashStamina();
        }
    }
}
