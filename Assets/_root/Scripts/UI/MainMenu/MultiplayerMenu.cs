using Sound;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

namespace UI
{
    public class MultiplayerMenu : Launcher
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _enterButton;
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_Text _score;

        const string playerNamePrefKey = "PlayerName";
        const string playerPasswordPrefKey = "PlayerPassword";

        private SoundController _soundController;

        void Start()
        {
            _soundController = FindObjectOfType<SoundController>();

            gameObject.SetActive(false);
            _backButton.onClick.AddListener(PlaySound);
            _backButton.onClick.AddListener(() => { gameObject.SetActive(false); });

            _enterButton.onClick.AddListener(PlaySound);
            _enterButton.onClick.AddListener(Login);

            _registerButton.onClick.AddListener(PlaySound);
            _registerButton.onClick.AddListener(Register);
            
            _exitButton.onClick.AddListener(PlaySound);
            _exitButton.onClick.AddListener(Exit);
            _exitButton.enabled = false;

            _connectButton.onClick.AddListener(PlaySound);
            _connectButton.onClick.AddListener(Connect);
            _connectButton.enabled = false;

            string defaultName = string.Empty;

            if (_nameField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey) && PlayerPrefs.HasKey(playerPasswordPrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _nameField.text = defaultName;
                    _passwordField.text = PlayerPrefs.GetString(playerPasswordPrefKey);
                }
            }
            PhotonNetwork.NickName = defaultName;

            _nameField.onValueChanged.AddListener(SetPlayerName);
            _passwordField.onValueChanged.AddListener(SetPlayerPassword);
        }

        private void Register()
        {
            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
            {
                Username = _nameField.text,
                Password = _passwordField.text,
                RequireBothUsernameAndEmail = false
            }, OnCreateSuccess, OnFailure);

            _connectButton.enabled = true;
            _exitButton.enabled = true;
        }

        private void OnFailure(PlayFabError obj)
        {
            Debug.Log(obj.ErrorMessage);
        }

        private void OnCreateSuccess(RegisterPlayFabUserResult obj)
        {
            feedbackText.text += System.Environment.NewLine + "Created Account";
            _connectButton.enabled = true;
            _exitButton.enabled = true;
        }
        public void Login()
        {
            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
            {
                Username = _nameField.text,
                Password = _passwordField.text
            }, OnSignInSuccess, OnFailure);
        }

        private void OnSignInSuccess(LoginResult obj)
        {
            feedbackText.text += System.Environment.NewLine + "Signed in Account";
            
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), CurrencySuccess, OnFailure);
            
            _connectButton.enabled = true;
            _exitButton.enabled = true;
        }

        private void CurrencySuccess(GetUserInventoryResult obj)
        {
            _score.text = obj.VirtualCurrency["PS"].ToString();
        }

        private void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

        public void Exit()
        {
            PlayFabClientAPI.ForgetAllCredentials();
            _connectButton.enabled = false;
            _exitButton.enabled = false;
        }

        private void SetPlayerPassword(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player password is null or empty");
                return;
            }
            PlayerPrefs.SetString(playerPasswordPrefKey, value);
        }

        public void SetBackButton(UnityAction onClikc)
        {
            _backButton.onClick.AddListener(onClikc);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void PlaySound()
        {
            _soundController.Button();
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _enterButton.onClick.RemoveAllListeners();
            _registerButton.onClick.RemoveAllListeners();
            _nameField.onValueChanged.RemoveAllListeners();
            _passwordField.onValueChanged.RemoveAllListeners();
        }
    }
}