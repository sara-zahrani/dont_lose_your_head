using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MenuController
{

    public override void LoadScene(string sceneName)
    {
        base.LoadScene(sceneName);
        //mMenuManager.SwitchMenu(MenuType.GameUI);
    }


    public void Instructions1()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions1);
    }

    public void Instructions2()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions2);
    }

    public void Instructions3()
    {
        mMenuManager.SwitchMenu(MenuType.Instructions3);
    }
}
