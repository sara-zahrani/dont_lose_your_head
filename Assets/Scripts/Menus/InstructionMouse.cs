using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMouse : MenuController
{
    public void Next()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions3);
    }

    public void Back()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions1);
    }
}
