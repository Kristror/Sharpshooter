using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class MBullet : MonoBehaviourPunCallbacks
    {
        [SerializeField] public int damage = 20;

        public string _owner = "";

        public void SetOwner(string owner)
        {
            _owner = owner;
        }
    }
}