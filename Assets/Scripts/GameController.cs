using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform player1SpawnPoint;
    [SerializeField]
    private Transform player2SpawnPoint;
    Color m_PlayerColor;

    private GameObject tank;


    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Falta la referencia al player prefab");
        }
        else
        {
            Transform spawnPoint = (PhotonNetwork.IsMasterClient) ? player1SpawnPoint : player2SpawnPoint;   

            object[] initData = new object[1];
            initData[0] = "Data instace";

            //Debug.Log("Color seleccionado " + (string)PhotonNetwork.LocalPlayer.CustomProperties["color"]);


            tank = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0, initData);
        }
    }
}
