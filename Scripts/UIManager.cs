using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playerUIPrefab;
    [SerializeField] private GameObject playerUIHolder;
    [SerializeField] private TextMeshProUGUI endScreenHeader;
    [SerializeField] private GameObject playerEndScreenScorePrefab;
    [SerializeField] private GameObject playerEndScreenScoreHolder;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gamePlayScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private PlayerScriptableObjectChannelSO setUpPlayerUIChannel;
    [SerializeField] private VoidEventChannelSO destroyPlayerUIChannel;
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;
    [SerializeField] private IntEventChannelSO playerWhoWonChannel;

    private List<GameObject> playerUIs;
    private List<GameObject> endScreenUIs;

    protected override void Awake()
    {
        base.Awake();

        playerUIs = new List<GameObject>();
        endScreenUIs = new List<GameObject>(); 
    }

    private void OnEnable()
    {
        setUpPlayerUIChannel.OnEventRaised += (player) => GeneratePlayerUI(player);
        gameStateRequestChannelSO.OnEventRaised += (state) => OnGameStateChange(state);
        playerWhoWonChannel.OnEventRaised += (ID) => endScreenHeader.text = "Player " + ID + " Wins";
        destroyPlayerUIChannel.OnEventRaised += () => DestroyAllPlayerUIs();
    }

    private void OnDisable()
    {
        setUpPlayerUIChannel.OnEventRaised -= (player) => GeneratePlayerUI(player);
        gameStateRequestChannelSO.OnEventRaised -= (state) => OnGameStateChange(state);
        playerWhoWonChannel.OnEventRaised -= (ID) => endScreenHeader.text = "Player " + ID + " Wins";
        destroyPlayerUIChannel.OnEventRaised -= () => DestroyAllPlayerUIs();
    }

    private void GeneratePlayerUI(Player player)
    {
        StartCoroutine(generatePlayerUI(player));
    }

    private IEnumerator generatePlayerUI(Player player)
    {
        yield return new WaitForSeconds(0.1f);
        
        Log("Player UI Prefab is " + playerUIPrefab);
        Log("Player UI Holder is " + playerUIHolder);
        GameObject playerUI = Instantiate(playerUIPrefab, playerUIHolder.transform);
        PlayerUIHelper playerUIHelper = playerUI.GetComponent<PlayerUIHelper>();
        playerUIHelper.setID(player.ID);
        playerUIHelper.setColor(player.color);
        playerUIs.Add(playerUI);

        GameObject endScreenScore = Instantiate(playerEndScreenScorePrefab, playerEndScreenScoreHolder.transform);
        EndScreenScoreHelper endScreenScoreHelper = endScreenScore.GetComponent<EndScreenScoreHelper>();
        endScreenScoreHelper.setPlayer(player);
        endScreenScoreHelper.setColor(player.color);
        endScreenUIs.Add(endScreenScore);
    }

    private void DestroyAllPlayerUIs()
    {
        foreach (GameObject playerUI in playerUIs)
        {
            // playerUIs.Remove(playerUI);
            Destroy(playerUI);
        }

        foreach (GameObject endScreenUI in endScreenUIs)
        {
            // endScreenUIs.Remove(endScreenUI);
            Destroy(endScreenUI);
        }
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.TitleScreen:
                titleScreen.SetActive(true);
                gamePlayScreen.SetActive(false);
                endScreen.SetActive(false);
                break;
            case GameState.MainMenu:
                DestroyAllPlayerUIs();
                titleScreen.SetActive(false);
                gamePlayScreen.SetActive(false);
                endScreen.SetActive(false);
                break;
            case GameState.Gameplay:
                gamePlayScreen.SetActive(true);
                endScreen.SetActive(false);
                break;
            case GameState.WinScreen:
                gamePlayScreen.SetActive(false);
                endScreen.SetActive(true);
                break;
        }
    }

}
