using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Multiplayer;

namespace Photon.Pun.Demo.PunBasics
{

	public class GameManager : MonoBehaviourPunCallbacks
	{
		static public GameManager Instance;

		[SerializeField] private GameObject playerPrefab;
		[SerializeField] private GameObject playersSpawns;

		[SerializeField] private GameObject pauseMenu;
		[SerializeField] private Dead dead;
		[SerializeField] private MScorebord scorebord;

		private MShooting mShooting;
		private MPlayerControlls mPlayerControlls;

		void Start()
		{
			Instance = this;

			if (!PhotonNetwork.IsConnected)
			{
				SceneManager.LoadScene("MainMenu");

				return;
			}

			if (MPlayerInventory.LocalPlayerInstance == null )
			{
				GameObject player = PhotonNetwork.Instantiate(this.playerPrefab.name, playersSpawns.transform.GetChild(Random.Range(0, playersSpawns.transform.childCount)).position, Quaternion.identity, 0);
				mPlayerControlls = player.GetComponent<MPlayerControlls>();
				mShooting = player.GetComponent<MShooting>();
				player.name = PlayerPrefs.GetString("PlayerName");

				player.GetComponent<MPlayerInventory>().SetDead(dead);
				player.GetComponent<MPlayerInventory>().SetPause(pauseMenu.GetComponent<MPauseMenu>());
			}
		}

		public void RespawnPlayer()
        {
			GameObject player = MPlayerInventory.LocalPlayerInstance;
			player.transform.position = playersSpawns.transform.GetChild(Random.Range(0, playersSpawns.transform.childCount)).position;
			player.GetComponent<MPlayerInventory>().Revive();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				pauseMenu.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				mPlayerControlls.isPause = true;
				mShooting.isPause = true;
			}
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				scorebord.gameObject.SetActive(true);
				scorebord.Fill();
			}
			if (Input.GetKeyUp(KeyCode.Tab)) scorebord.Close();
		}

		public override void OnPlayerEnteredRoom(Player other)
		{
			Debug.Log("OnPlayerEnteredRoom() " + other.NickName);
			if (PhotonNetwork.IsMasterClient)
			{
				Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
			}
		}

		public override void OnPlayerLeftRoom(Player other)
		{
			Debug.Log("OnPlayerLeftRoom() " + other.NickName);

			if (PhotonNetwork.IsMasterClient)
			{
				Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
			}
		}

		public override void OnLeftRoom()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}