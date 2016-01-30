using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
   

    
    Canvas MainMenuCanvas, GameplayCanvas, 
        OptionsCanvas, CreditsCanvas,MultiplayerCanvas,
        GameCreateMenuCanvas,SinglePlayerGameplayCanvas,
        LocalMPGameplayCanvas;
    [SerializeField]
    GameObject gameplayStuff;

    public enum GameState
    {
        MainMenu,
        Gameplay,
        MultiplayerMenu,
        SinglePlayerGameplay,
        GameCreateMenu,
        LocalMPGameplay,
        
        Credits,

    }
    public GameState currentState;

	void Start () {
        
        instance = this;
        MainMenuCanvas = GameObject.Find("MainMenuCanvas").gameObject.GetComponent<Canvas>();
        GameplayCanvas = GameObject.Find("GameplayCanvas").gameObject.GetComponent<Canvas>();
        MultiplayerCanvas = GameObject.Find("MultiplayerCanvas").gameObject.GetComponent<Canvas>();
        GameCreateMenuCanvas = GameObject.Find("GameCreateMenuCanvas").gameObject.GetComponent<Canvas>();
       
        ChangeState(GameState.MainMenu);

    }

    void Update () {
	
	}
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        StartCoroutine(newState.ToString() + "State");


    }

   

    // Game States

    IEnumerator MainMenuState()
    {
        MainMenuCanvas.enabled = true;

        while (currentState == GameState.MainMenu)
        {
            yield return null;
        }
        MainMenuCanvas.enabled = false;
    }
    IEnumerator GameCreateMenuState()
    {
        GameCreateMenuCanvas.enabled = true;

        while (currentState == GameState.GameCreateMenu)
        {
            yield return null;
        }
        GameCreateMenuCanvas.enabled = false;
    }


    IEnumerator GameplayState()
    {
        GameplayCanvas.enabled = true;
      
        Gameplay.instance.enabled = true;


        while (currentState == GameState.Gameplay)
        {
            yield return null;
        }
        GameplayCanvas.enabled = false;
        Gameplay.instance.enabled = false;

    }
    IEnumerator SinglePlayerGameplayState()
    {

        GameplayCanvas.enabled = true;
        GameplayCanvas.GetComponent<SinglePlayerGameplay>().enabled = true;



        while (currentState == GameState.SinglePlayerGameplay)
        {
            yield return null;
        }
        GameplayCanvas.enabled = false;
        GameplayCanvas.GetComponent<SinglePlayerGameplay>().enabled = false;

    }
    IEnumerator LocalMPGameplayState()
    {

        GameplayCanvas.enabled = true;
        GameplayCanvas.GetComponent<LocalMultiplayerScript>().enabled = true;



        while (currentState == GameState.LocalMPGameplay)
        {
            yield return null;
        }
        GameplayCanvas.enabled = false;
        GameplayCanvas.GetComponent<LocalMultiplayerScript>().enabled = false;

    }

    IEnumerator MultiplayerMenuState() {
        MultiplayerCanvas.enabled = true;
       


        while (currentState == GameState.MultiplayerMenu)
        {
            yield return null;
        }
        MultiplayerCanvas.enabled = false;
    }

}
