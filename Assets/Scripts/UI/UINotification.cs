using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotification : MonoBehaviour
{
    public static UINotification instance;
    public Text text;
    // Start is called before the first frame update
    void Awake()
    {
        Hide();
        instance = this;
    }

    public void Hide()
    {
        CancelInvoke();
        text.color = Color.clear;
    }

    public void Show(string text, float duration)
    {
        CancelInvoke();
        this.text.text = text;
        this.text.color = Color.white;
        Invoke("Hide", duration);
    }
}
