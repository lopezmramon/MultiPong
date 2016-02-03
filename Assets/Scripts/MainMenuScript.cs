using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
    GameManager gameManager;
    public AudioClip clip;
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	

    public void SinglePlayerGame()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        gameManager.ChangeState(GameManager.GameState.SinglePlayerGameplay);

    }
    public void LocalMultiplayerGame()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        gameManager.ChangeState(GameManager.GameState.LocalMPGameplay);

    }
    public void OnlineMultiplayerGame()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        gameManager.ChangeState(GameManager.GameState.MultiplayerMenu);
    }
    public void MuteSound()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        SoundManager.instance.audioSource.enabled = !SoundManager.instance.audioSource.enabled;

       
    }
    public void MuteMusic()
    {
        SoundManager.instance.audioSource.PlayOneShot(clip);
        MusicManager.instance.source.enabled = !MusicManager.instance.source.enabled;


    }
    public void Quit()
    {

        Application.Quit();
    }
}
