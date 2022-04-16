using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    public static DebugController instance;
    public GameObject console;
    public InputField commandText;
    public Text consoleOutputText;
    public GameObject castleKeyPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hexe.Debug.Console.Initialize();
    }

    public static void Clear()
    {
        instance.consoleOutputText.text = "";
    }
    public static void PrintLine(string text)
    {
        instance.consoleOutputText.text += text+ System.Environment.NewLine;
    }

    public static void PrintErrorLine(string text)
    {
        instance.consoleOutputText.text += "<color=red>"+text+"</color>"+ System.Environment.NewLine;
    }

    public void SubmitCommand()
    {
        var text = commandText.text;
        PrintLine("> " + text);
        commandText.text = "";
        Hexe.Debug.Console.Execute(text);
        commandText.Select();
        commandText.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (console.activeSelf)
                SubmitCommand();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (console.activeSelf)
            {
               
                console.SetActive(false);
                GameController.PopBusy();
            }
            else
            {
                Cursor.visible = true;
                console.SetActive(true);
                GameController.PushBusy();
                commandText.Select();
                commandText.ActivateInputField();
            }
        }
    }
}
