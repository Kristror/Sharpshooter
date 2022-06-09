using Photon.Pun.Demo.PunBasics;
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer
{

    public class MTimer : MonoBehaviour
    {
        [Header("Countdown time in seconds")]
        [SerializeField] private float _round = 300f;

        [SerializeField] private MScorebord _mScorebord;
        [SerializeField] private TMP_Text _timer;

        private bool isEnded = false;

        void FixedUpdate()
        {
            if (!isEnded)
            {
                if (_round <= 0)
                {
                    End();
                }
                _round -= Time.fixedDeltaTime;
                _timer.text = Math.Round(_round).ToString();
            }
        }

        private void End()
        {
            isEnded = true;

            _mScorebord.gameObject.SetActive(true);
            _mScorebord.Fill();

            PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
            {
                VirtualCurrency = "PS",
                Amount = MPlayerInventory.LocalPlayerInstance.GetComponent<MPlayerInventory>().Score
            }, OnAddSuccess, OnFailure);

            

            Debug.Log("End of round");
        }

        private void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnFailure(PlayFabError obj)
        {
            Debug.Log("Eror");
        }

        private void OnAddSuccess(ModifyUserVirtualCurrencyResult obj)
        {
            Debug.Log("All good " + obj.VirtualCurrency);
            Invoke("MainMenu", 5f);
        }
    }
}