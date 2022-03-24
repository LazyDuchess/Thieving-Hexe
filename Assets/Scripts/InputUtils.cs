using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtils
{
    private static Vector3 lastMouse = Vector3.zero;

    public static bool MouseInFocus()
    {
        var aimCoords = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);
        if (aimCoords.x > 1.0f || aimCoords.y > 1.0f || aimCoords.x < 0.0f || aimCoords.y < 0.0f)
            return false;
        return true;
    }
    public static Vector3 GetMouse()
    {
        if (!MouseInFocus())
            return lastMouse;
        var aimCoords = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);
        aimCoords -= new Vector3(0.5f, 0.5f, 0.0f);
        lastMouse = aimCoords;
        return aimCoords;
    }

    public static Vector3 GetMouseScreen()
    {
        var mouse = GetMouse();
        mouse = new Vector3((mouse.x + 0.5f) * Screen.width, (mouse.y + 0.5f) * Screen.height, 0f);
        return mouse;
    }
}
