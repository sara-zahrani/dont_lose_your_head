using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuType
{
    MainMenu,
    Instructions1,
    Instructions2,
    Instructions3,
    GameUI,
    PauseMenu,
    LevelCompleteMenu,
    LostGame
}

public class MenuManager : MonoBehaviour
{
    List<MenuController> mMenuControllers;
    MenuController mLastActiveMenu;
    public MenuType mFirstActiveMenu;
    public GameObject mBackgroundMainMenu;
    public GameObject mBackgroundInstruction;

    void Awake()
    {
        mMenuControllers = GetComponentsInChildren<MenuController>().ToList();
        mMenuControllers.ForEach(x => x.gameObject.SetActive(false));
        SwitchMenu(mFirstActiveMenu);
        if(mBackgroundMainMenu != null && mBackgroundInstruction != null)
        {
            //first value main menu //second value instruction
            ActivateBackground(true, false); 
        }


    }

    public void SwitchMenu(MenuType type)
    {
        Scene currenttScene = SceneManager.GetActiveScene();


        if (mLastActiveMenu != null)
        {
            mLastActiveMenu.gameObject.SetActive(false);
        }

        MenuController desiredMenu = mMenuControllers.Find(x => x.mMenuType == type);

        if (desiredMenu != null)
        {
            desiredMenu.gameObject.SetActive(true);
            mLastActiveMenu = desiredMenu;
        }

        else { Debug.LogWarning("The desired menu was not found!"); }

        if (currenttScene.name == "MainMenu")
        {
            if (desiredMenu.mMenuType == MenuType.Instructions1 ||
                desiredMenu.mMenuType == MenuType.Instructions2 ||
                desiredMenu.mMenuType == MenuType.Instructions3)
            {
                ActivateBackground(false, true);
            }
            else
            {
                ActivateBackground(true, false);

            }
        }
    }

    private void ActivateBackground(bool mainMenu, bool instruction)
    {

        mBackgroundMainMenu.SetActive(mainMenu);
        mBackgroundInstruction.SetActive(instruction);

    }


}












