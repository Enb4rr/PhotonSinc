using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour,IOnEventCallback
{

    private const byte CureEventCode = 1;


    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void GenerateCure()
    {
        RaiseEventOptions eventOptions = new RaiseEventOptions 
        { 
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };
        PhotonNetwork.RaiseEvent(CureEventCode, null, eventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CureEventCode)
        {
            Debug.Log("Cura instanciada");
            //instantiate
        }
    }

}
