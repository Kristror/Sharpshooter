using System.Collections;
using System.Collections.Generic;
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

        void Start()
        {
            _musicSlider.onValueChanged.AddListener(MusicVolumeUpdate);
            _effectsSlider.onValueChanged.AddListener(EffectsVolumeUpdate);

            gameObject.SetActive(false);
            _backButton.onClick.AddListener(() => { gameObject.SetActive(false); });
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
            //поменять значение в звуке
        }
        private void EffectsVolumeUpdate(float newValue)
        {
            //поменять значение в звуке
        }
        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveAllListeners();
            _effectsSlider.onValueChanged.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
    }
}
