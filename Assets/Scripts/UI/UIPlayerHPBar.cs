using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHPBar : MonoBehaviour
{
    private UIBar bar;
    private float lastHP;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<UIBar>();
        bar.maxValue = GameController.instance.playerController.maxHP;
        bar.minValue = 0f;
        bar.SetValue(GameController.instance.playerController.hp);
        lastHP = GameController.instance.playerController.hp;
    }

    private void Update()
    {
        if (GameController.instance.playerController.hp != lastHP)
        {
            bar.SetValue(GameController.instance.playerController.hp);
            lastHP = GameController.instance.playerController.hp;
        }
    }
}
