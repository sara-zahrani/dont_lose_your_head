using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MenuController
{

    public void Next()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions2);
    }

}
