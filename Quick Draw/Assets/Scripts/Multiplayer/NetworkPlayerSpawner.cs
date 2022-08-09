using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Multiplayer
{
    public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
    {
        private GameObject _playerPrefab;

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Vector3 spawnPosition = GameObject.Find("Spawn One").transform.position;;
           
            if(PhotonNetwork.CurrentRoom.PlayerCount.Equals(1))
            {
                spawnPosition = GameObject.Find("Spawn One").transform.position;
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount.Equals(2))
            {
                spawnPosition =  GameObject.Find("Spawn Two").transform.position;
            }
            _playerPrefab = PhotonNetwork.Instantiate("Photon/Player", spawnPosition, transform.rotation);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.Destroy(_playerPrefab);
        }
    }
}
