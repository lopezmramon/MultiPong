using UnityEngine;
using System.Collections;

public class ButtonManager : Photon.MonoBehaviour {

    public AudioClip clip;

    public void Quit()
    {

        if (PhotonNetwork.connected)
        {
            Gameplay.instance.spawnedList.ForEach(PhotonNetwork.Destroy);
            Gameplay.instance.FinishGame("Defeat");
            PhotonNetwork.Disconnect();
            SoundManager.instance.audioSource.PlayOneShot(clip);
            
        }
        else
        {
            SoundManager.instance.audioSource.PlayOneShot(clip);
            SinglePlayerGameplay.instance.FinishGame("Defeat");
            LocalMultiplayerGameplay.instance.FinishGame("Defeat");
        }
    }
}
