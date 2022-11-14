using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviourPun
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform player1SpawnPoint;
    [SerializeField]
    private Transform player2SpawnPoint;
    Color m_PlayerColor;

    private GameObject playerTank;

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

            Debug.Log("Color seleccionado " + (string)PhotonNetwork.LocalPlayer.CustomProperties["color"]);


            playerTank = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0, initData);
        }
    }

    [PunRPC]
    public void CreateTanks()
    {
        switch ((string) PhotonNetwork.LocalPlayer.CustomProperties["color"])
        {
            case "Green":
                m_PlayerColor = Color.green;
                break;
            case "Blue":
                m_PlayerColor = Color.blue;
                break;
            case "Red":
                m_PlayerColor = Color.red;
                break;
            case "Yellow":
                m_PlayerColor = Color.yellow;
                break;

            default:
                m_PlayerColor = Color.green;
                break;
        }

        MeshRenderer[] renderers = playerTank.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            Debug.Log(renderers[i].transform.gameObject.name + "tank renderer");
            renderers[i].material.color = m_PlayerColor;
        }
    }
}
