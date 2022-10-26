using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionRetrave : MenuController
{
    public void Back()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions2);
    }

    public void Close()
    {
        mMenuManager.SwitchMenu(MenuType.MainMenu);
    }
}
