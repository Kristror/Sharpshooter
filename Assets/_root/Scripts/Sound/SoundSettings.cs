using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "ScriptableObjects/SounSettings", order = 1)]
    public class SoundSettings : ScriptableObject
    {
        private float _musicVolume = 1;
        private float _effectsVolume = 1;

        public void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
        }
        public void SetEffectsVolume(float volume)
        {
            _effectsVolume = volume;
        }

        public float MusicVolume => _musicVolume;

        public float EffectsVolume => _effectsVolume;
    }
}