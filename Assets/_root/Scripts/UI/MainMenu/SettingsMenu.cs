using Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        [SerializeField] private Button _backButton;

        private SoundController _soundController;

        private SoundSettings _soundSettings;

        void Start()
        {
            _soundController = FindObjectOfType<SoundController>();

            _musicSlider.onValueChanged.AddListener(MusicVolumeUpdate);
            _effectsSlider.onValueChanged.AddListener(EffectsVolumeUpdate);

            gameObject.SetActive(false);
            _backButton.onClick.AddListener(PlaySound);
            _backButton.onClick.AddListener(() => { gameObject.SetActive(false); });

            _soundSettings = _soundController.SoundSettings;
        }

        public void SetBackButton(UnityAction onClikc)
        {
            _backButton.onClick.AddListener(onClikc);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void MusicVolumeUpdate(float newValue)
        {
            _soundSettings.SetMusicVolume(newValue);
            _soundController.UpdateVolume();
        }

        private void EffectsVolumeUpdate(float newValue)
        {
            _soundSettings.SetEffectsVolume(newValue);
        }
        private void PlaySound()
        {
            _soundController.Button();
        }

        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveAllListeners();
            _effectsSlider.onValueChanged.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
       
    }
}
