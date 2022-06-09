using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{

    public class EndLevel : MonoBehaviour
    {
        private UnityAction onEnd;


        public void SetOnEnd(UnityAction end)
        {
            onEnd = end;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onEnd();
                SoundSettings soundSettings = (SoundSettings)Resources.Load("SoundSettings");
            }
        }
    }
}