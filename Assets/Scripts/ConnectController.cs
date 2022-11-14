using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject connectPanel, selectColorPanel;

    [SerializeField] 
    private TMP_Text nicknameText;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

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

            nicknameText.text = PhotonNetwork.NickName;
            connectPanel.SetActive(false);
            selectColorPanel.SetActive(true);
        }
    }

    public void SetColor(int index)
    {
        string color = GameObject.Find("DropDownColors").GetComponent<TMP_Dropdown>().options[index].text;

        Debug.Log("Color: " + color);

        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "color", color } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
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

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Room ready");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Se ha unido " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    #endregion
}
