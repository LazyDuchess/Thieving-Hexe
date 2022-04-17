using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHPBar : MonoBehaviour
{
    private UIBar bar;
    private float lastHP;
    public bool playerTwo = false;

    PlayerController getTargetPlayer()
    {
        if (playerTwo)
            return GameController.instance.coopPlayer;
        return GameController.instance.playerController;
    }
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<UIBar>();
        bar.maxValue = getTargetPlayer().maxHP;
        bar.minValue = 0f;
        bar.SetValue(getTargetPlayer().hp);
        lastHP = getTargetPlayer().hp;
    }

    private void Update()
    {
        if (getTargetPlayer().hp != lastHP)
        {
            bar.SetValue(getTargetPlayer().hp);
            lastHP = getTargetPlayer().hp;
        }
    }
}
