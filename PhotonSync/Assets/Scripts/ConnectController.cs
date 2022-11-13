using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public enum RegionCode
{
    AUTO, CAE, EU, US, USW, SA
}

public class ConnectController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    string gameVersion = "1";

    [SerializeField]
    string regionCode = null;

    public void SetRegion(int index)
    {
        RegionCode region = (RegionCode)index;

        if(region == RegionCode.AUTO)
        {
            regionCode = null;
        }
        else
        {
            regionCode = region.ToString();
        }

        Debug.Log("Region seleccionada: " + regionCode);
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regionCode;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #region MonobehaviourPunCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUM. No random room available, so we create one. \nCalling: PhotoNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUM. Now this client is in a room.");

        ////PhotonNetwork.LoadLevel("Game"); ////pa no compilar mucho

        //if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        //{
        //    Debug.Log("Sala lista");//lo tiene que llamar el master si no no da
        //    //PhotonNetwork.LoadLevel("Game");//ESTO SE COMENTA DESPUES
        //}
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Debug.Log(newPlayer.NickName + " Se ha unido " + PhotonNetwork.CurrentRoom.PlayerCount);

        //if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        //{
        //    Debug.Log("Sala llena*");//lo tiene que llamar el master si no no da
        //    PhotonNetwork.LoadLevel("Game");
        //    /*ShowRoomPanel();*/
        //}
    }


    #endregion
}
