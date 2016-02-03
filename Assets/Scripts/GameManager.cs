using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
   

    
    Canvas MainMenuCanvas, GameplayCanvas, 
        OptionsCanvas, CreditsCanvas,MultiplayerCanvas,
        GameCreateMenuCanvas,SinglePlayerGameplayCanvas,
        LocalMPGameplayCanvas,VictoryCanvas,DefeatCanvas;
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
        Victory,
        Defeat,
        
        Credits,

    }
    public GameState currentState;

	void Start () {
        
        instance = this;
        MainMenuCanvas = GameObject.Find("MainMenuCanvas").gameObject.GetComponent<Canvas>();
        GameplayCanvas = GameObject.Find("GameplayCanvas").gameObject.GetComponent<Canvas>();
        MultiplayerCanvas = GameObject.Find("MultiplayerCanvas").gameObject.GetComponent<Canvas>();
        GameCreateMenuCanvas = GameObject.Find("GameCreateMenuCanvas").gameObject.GetComponent<Canvas>();
        DefeatCanvas = GameObject.Find("DefeatCanvas").gameObject.GetComponent<Canvas>();
        VictoryCanvas = GameObject.Find("VictoryCanvas").gameObject.GetComponent<Canvas>();

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
      
       GetComponent<Gameplay>(). enabled = true;


        while (currentState == GameState.Gameplay)
        {
            yield return null;
        }
        GetComponent<Gameplay>().enabled = false;
        GameplayCanvas.enabled = false;
       

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
        GameplayCanvas.GetComponent<LocalMultiplayerGameplay>().enabled = true;



        while (currentState == GameState.LocalMPGameplay)
        {
            yield return null;
        }
        GameplayCanvas.enabled = false;
        GameplayCanvas.GetComponent<LocalMultiplayerGameplay>().enabled = false;

    }

    IEnumerator MultiplayerMenuState() {
        MultiplayerCanvas.enabled = true;
       


        while (currentState == GameState.MultiplayerMenu)
        {
            yield return null;
        }
        MultiplayerCanvas.enabled = false;
    }
    IEnumerator VictoryState()
    {
        VictoryCanvas.enabled = true;
        VictoryCanvas.GetComponent<EndCanvas>().enabled = true;


        while (currentState == GameState.Victory)
        {
            yield return null;
        }
        VictoryCanvas.enabled = false;
        VictoryCanvas.GetComponent<EndCanvas>().enabled = false;

    }
    IEnumerator DefeatState()
    {
        DefeatCanvas.enabled = true;

        DefeatCanvas.GetComponent<EndCanvas>().enabled = true;

        while (currentState == GameState.Defeat)
        {
            yield return null;
        }
        DefeatCanvas.enabled = false;
        DefeatCanvas.GetComponent<EndCanvas>().enabled = false;

    }
}
