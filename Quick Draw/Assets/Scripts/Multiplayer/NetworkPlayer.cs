using UnityEngine;
using Photon.Pun;

namespace Multiplayer
{
    public class NetworkPlayer : MonoBehaviour
    {
        public GameObject head, leftHand, rightHand;
        private PhotonView _photonView;
        void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }

        void Update()
        {
            
        }
    }
}
