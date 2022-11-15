using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSystem : MonoBehaviourPun
{
    
    [SerializeField] GameObject mine;

    void Update()
    {

        if (photonView.IsMine && Input.GetKeyUp(KeyCode.Return))
        {
            photonView.RPC("SetMine", RpcTarget.AllBuffered);
        }
        
    }

    [PunRPC]
    void SetMine()
    {
        Debug.Log("Plantando mina");
        GameObject.Instantiate(mine);
    }
}
