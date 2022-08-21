using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Multiplayer
{
    public class NameTag : MonoBehaviourPun
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        private Transform _cameraTrasform;
        void Start()
        {
            if (photonView.IsMine)
            {
                enabled = false;
                return;
            }
            _nameText.text = gameObject.transform.parent.name;
            _cameraTrasform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _cameraTrasform.rotation* Vector3.forward, _cameraTrasform.rotation * Vector3.up);
        }
    }
}