using UnityEngine;

namespace Sound
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private SoundSettings _soundSettings;

        [SerializeField] private AudioSource _sourceMusic;
        [SerializeField] private AudioSource _sourceShoot;
        [SerializeField] private AudioSource _sourceEmpty;
        [SerializeField] private AudioSource _sourceFootsteps;
        [SerializeField] private AudioSource _sourceDeath;
        [SerializeField] private AudioSource _sourceWin;
        [SerializeField] private AudioSource _sourceButton;
        [SerializeField] private AudioSource _sourceSkeletonDeath;
        [SerializeField] private AudioSource _sourceHit;

        public SoundSettings SoundSettings => _soundSettings;        


        private void Awake()
        {
            SoundController[] soundController = FindObjectsOfType<SoundController>();

            if (soundController.Length == 1)
            {
                DontDestroyOnLoad(gameObject);
                Music();
            }
            else 
            {
                for (int i = 1; i < soundController.Length; i++)
                {
                    Destroy(soundController[i].gameObject);
                }
                DontDestroyOnLoad(soundController[0].gameObject);
                Music();
            }
        }

        public void UpdateVolume()
        {
            _sourceMusic.volume = _soundSettings.MusicVolume * 0.75f;
        }

        public void SetPosition(Vector3 position)
        {
           transform.position = position;
        }

        public void Music()
        {
            _sourceMusic.volume = _soundSettings.MusicVolume * 0.75f;
            _sourceMusic.Play();
        }

        public void StopMusic()
        {
            _sourceMusic.Stop();
        }
        
        public void Shoot()
        {
            _sourceShoot.volume = _soundSettings.EffectsVolume;
            _sourceShoot.Play();
        } 

        public void Empty()
        {
            _sourceEmpty.volume = _soundSettings.EffectsVolume;
            _sourceEmpty.Play();
        } 

        public void Footsteps()
        {
            _sourceFootsteps.volume = _soundSettings.EffectsVolume;
            _sourceFootsteps.Play();
        } 
        public void StopFootsteps()
        {
            _sourceFootsteps.Stop();
        }         

        public void Death()
        {
            _sourceDeath.volume = _soundSettings.EffectsVolume;
            _sourceDeath.Play();
            _sourceMusic.Stop();
        } 
        
        public void Win()
        {
            _sourceWin.volume = _soundSettings.EffectsVolume;
            _sourceWin.Play();
            _sourceMusic.Stop();
        } 
        
        public void Button()
        {
            _sourceButton.volume = _soundSettings.EffectsVolume;
            _sourceButton.Play();
        } 
        
        public void SkeletonDeath()
        {
            _sourceSkeletonDeath.volume = _soundSettings.EffectsVolume;
            _sourceSkeletonDeath.Play();
        } 
        
        public void Hit()
        {
            _sourceHit.volume = _soundSettings.EffectsVolume;
            _sourceHit.Play();
        } 
    }
}