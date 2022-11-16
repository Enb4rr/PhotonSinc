using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour, IOnEventCallback
{
    [SerializeField] GameObject pill;
    private float timer = 0f;
    private EventData evento;

    private const byte CureEventCode = 1;


    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateCure();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f)
        {
            timer = 0f;          
            evento.Code = 1;
            OnEvent(evento);
        }

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
            GameObject.Instantiate(pill);
        }
    }

}
