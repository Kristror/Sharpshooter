using Items;
using UnityEngine;

namespace SingleGame
{
    public class ItemController
    {
        private GameObject _medKit;
        private GameObject _ammo;

        private float _range = 8;

        public ItemController(Transform itemSpawns)
        {
            _medKit = (GameObject)Resources.Load("Items/MedKit");
            _ammo = (GameObject)Resources.Load("Items/Ammo");

            for (int i = 0; i < itemSpawns.transform.childCount; i++)
            {
                Spawn(itemSpawns.GetChild(i), ItemType.Medkit);
                Spawn(itemSpawns.GetChild(i), ItemType.Ammo);
            }                 
        }
        
        public void Spawn(Transform spawnPoint, ItemType type)
        {
            switch (type)
            {
                case ItemType.Medkit:
                    GameObject medkit = GameObject.Instantiate(_medKit, spawnPoint);
                    medkit.transform.position = new Vector3(medkit.transform.position.x + Random.value * _range, medkit.transform.position.y, medkit.transform.position.z + Random.value * _range);
                    medkit.GetComponent<MedKit>().Init(Spawn);
                    break;
                case ItemType.Ammo:
                    GameObject ammo = GameObject.Instantiate(_ammo, spawnPoint);
                    ammo.transform.position = new Vector3(ammo.transform.position.x + Random.value * _range, ammo.transform.position.y, ammo.transform.position.z + Random.value * _range);
                    ammo.GetComponent<Ammo>().Init(Spawn); 
                    break;
            }
        }
    }
}