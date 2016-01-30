using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
    GameManager gameManager;
    AudioListener alistener;
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        alistener = FindObjectOfType<AudioListener>();
	}
	
	void Update () {
	
	}

    public void SinglePlayerGame()
    {
        gameManager.ChangeState(GameManager.GameState.Gameplay);

    }
    public void LocalMultiplayerGame()
    {
        gameManager.ChangeState(GameManager.GameState.Gameplay);

    }
    public void OnlineMultiplayerGame()
    {
        gameManager.ChangeState(GameManager.GameState.MultiplayerMenu);
    }
    public void Mute()
    {
        alistener.enabled = !alistener.enabled;

       
    }
    public void Quit()
    {

        Application.Quit();
    }
}
