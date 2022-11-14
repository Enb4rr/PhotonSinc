using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviourPun
{
    private Color m_PlayerColor;

    private void Start()
    {
        photonView.RPC("ChangeColor", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void ChangeColor()
    {
        if (photonView.IsMine)
        {
            switch ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"])
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
        }
        else
        {
            switch ((string)PhotonNetwork.PlayerList
                        [Equals(PhotonNetwork.PlayerList[1]
                            , PhotonNetwork.LocalPlayer) ? 0 : 1]
                        .CustomProperties["color"])
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
        }

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            Debug.Log(renderers[i].transform.gameObject.name + "tank renderer");
            renderers[i].material.color = m_PlayerColor;
        }
    }
}
