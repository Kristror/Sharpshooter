using Items;
using Photon.Pun;
using UnityEngine;

namespace Multiplayer
{
    public class MItemController : MonoBehaviour
    {
        [SerializeField] private GameObject _medKit;
        [SerializeField] private GameObject _ammo;
        [SerializeField] private GameObject itemsSpawns;

        private float _range = 2;

        public void Start()
        {
            MItemController[] soundController = FindObjectsOfType<MItemController>();

            if (soundController.Length != 1)
            {
                Destroy(gameObject);
            }

            for (int i = 0; i < itemsSpawns.transform.childCount; i++)
            {
                Spawn(itemsSpawns.transform.GetChild(i), ItemType.Medkit);
                Spawn(itemsSpawns.transform.GetChild(i), ItemType.Ammo);
            }
        }

        public void Spawn(Transform spawnPoint, ItemType type)
        {
            switch (type)
            {
                case ItemType.Medkit:
                    GameObject medkit = PhotonNetwork.Instantiate(_medKit.name, spawnPoint.position, Quaternion.identity);
                    medkit.transform.position = new Vector3(medkit.transform.position.x + Random.value * _range, medkit.transform.position.y + 1, medkit.transform.position.z + Random.value * _range);                    
                    medkit.GetComponent<MMedKit>().Init(Spawn, spawnPoint);
                    break;

                case ItemType.Ammo:
                    GameObject ammo = PhotonNetwork.Instantiate(_ammo.name, spawnPoint.position, Quaternion.identity);
                    ammo.transform.position = new Vector3(ammo.transform.position.x + Random.value * _range, ammo.transform.position.y + 1, ammo.transform.position.z + Random.value * _range);
                    ammo.GetComponent<MAmmo>().Init(Spawn, spawnPoint);
                    break;
            }
        }
    }
}