using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
	StartGame,
	PauseGame,
	EndLevel,
	EndGame
}

public class GameManager : MonoBehaviour
{

	public MenuManager mMenuManager;
	public GameStatus mCurrentGameStatus;
	public GameObject mPlayer;
	public MumblingSound mMumblingSound;

	//====================================================================================================


	void Awake()
	{

		mCurrentGameStatus = GameStatus.StartGame;

	}

	private void Start()
    {

		mMenuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
		Time.timeScale = 1;

	}

    void Update()
	{
	
		if (mCurrentGameStatus == GameStatus.StartGame)
		{	
			Time.timeScale = 1;
			mPlayer.GetComponent<BoneManager>().enabled = true;
			mMumblingSound.audioSource.UnPause();
			Debug.Log("Status START Game");
		}

		else if (mCurrentGameStatus == GameStatus.EndLevel)
		{
		
			Time.timeScale = 0;
			mPlayer.GetComponent<BoneManager>().enabled = false;
			Debug.Log("Status END LEVEL");

            mMenuManager.SwitchMenu(MenuType.LevelCompleteMenu);
        }
		else if (mCurrentGameStatus == GameStatus.PauseGame)
		{
	
			Time.timeScale = 0;
			mPlayer.GetComponent<BoneManager>().enabled = false;
			mMumblingSound.audioSource.Pause();

			Debug.Log("Status PAUSE Game");

		}
		else if (mCurrentGameStatus == GameStatus.EndGame)
		{
			Time.timeScale = 0;
			mPlayer.GetComponent<BoneManager>().enabled = false;

			//  Won the game back to main menu 

		}
		//else if(mCurrentGameStatus == GameStatus.LostGame)
		//      {
		//	Time.timeScale = 0;
		//	Debug.Log("LOST GAME");

		//	mMenuManager.SwitchMenu(MenuType.LostGame);
		//}







	}

	
}
