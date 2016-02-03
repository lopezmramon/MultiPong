using UnityEngine;
using System.Collections;

public class BetterNetworkPlayer : Photon.MonoBehaviour {

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
 {
     if (stream.isWriting)
     {
         stream.SendNext(GetComponent<Rigidbody2D>().position);
         stream.SendNext(GetComponent<Rigidbody2D>().velocity);
     }
     else
     {
         Vector3 syncPosition = (Vector3)stream.ReceiveNext();
         Vector3 syncVelocity = (Vector3)stream.ReceiveNext();

         syncTime = 0f;
         syncDelay = Time.time - lastSynchronizationTime;
         lastSynchronizationTime = Time.time;

         syncEndPosition = syncPosition + syncVelocity * syncDelay;
         syncStartPosition = GetComponent<Rigidbody2D>().position;
     }
 }
    }
    void Update()
    {
        if (photonView.isMine)
        {
           
        }
        else
        {
            SyncedMovement();
        }
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;
        GetComponent<Rigidbody2D>().position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }
}
