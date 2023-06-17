using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<Player> players;
    [SerializeField] private int numberOfPlayers = 1;
    public void setNumberOfPlayers(int numberOfPlayers)
    {
        this.numberOfPlayers = numberOfPlayers;
        players.Clear();
        for (int i = 0; i < (numberOfPlayers); i++)
        {
            players.Add(allPossiblePlayers[i]);
        }
    }
    [SerializeField] private List<Player> allPossiblePlayers;
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private GameState shouldResetOnThisState;
    [SerializeField] private PlayerValueUpdateChannelSO deathChannel;
    [SerializeField] private IntEventChannelSO lostChannel;
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;
    [SerializeField] private PlayerScriptableObjectChannelSO setUpChannel;
    [SerializeField] private IntEventChannelSO setNumberOfPlayersChannel;
    [SerializeField] private IntEventChannelSO playerWhoWonChannel;

    private List<int> playersWhoLost;

    protected override void Awake()
    {
        base.Awake();
        setNumberOfPlayers(numberOfPlayers);

        playersWhoLost = new List<int>();
    }

    private void OnEnable()
    {
        deathChannel.OnEventRaised += (player, lives) => SpawnSnake(player);
        lostChannel.OnEventRaised += (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised += (state) => OnGameStateChange(state);
        setNumberOfPlayersChannel.OnEventRaised += (numberOfPlayers) => setNumberOfPlayers(numberOfPlayers + 1);
    }

    private void OnDisable()
    {
        deathChannel.OnEventRaised -= (player, lives) => SpawnSnake(player);
        lostChannel.OnEventRaised -= (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised -= (state) => OnGameStateChange(state);
        setNumberOfPlayersChannel.OnEventRaised -= (numberOfPlayers) => setNumberOfPlayers(numberOfPlayers + 1);
    }

    private void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                Reset();
                break;
            case GameState.Gameplay:
                SpawnAllPlayers();
                break;
        }
    }

    private void Reset()
    {
        playersWhoLost.Clear();
    }

    private void SpawnAllPlayers()
    {
        playersWhoLost.Clear();
        foreach (Player player in players)
        {
            player.Reset();
            SpawnSnake(player);
            setUpChannel.RaiseEvent(player);
        }
    }
    
    private bool playerHasLostAlready(int ID)
    {
        if(playersWhoLost.Find(value => value == ID) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    private void SpawnSnake(Player player)
    {
        if(playerHasLostAlready(player.ID)) return;
        
        StartCoroutine(spawn(player));
    }

    private void OnPlayerLost(int ID)
    {
        if(playerHasLostAlready(ID)) return;
        
        playersWhoLost.Add(ID);

        if(playersWhoLost.Count == numberOfPlayers)
        {
            gameStateRequestChannelSO.RaiseEvent(GameState.WinScreen);
            Player winner = players.Find(player => player.score == players.Max(player => player.score));
            playerWhoWonChannel.RaiseEvent(winner.ID);
        }
    }

    private IEnumerator spawn(Player player)
    {
        yield return new WaitForSeconds(0.1f);

        if( !playerHasLostAlready(player.ID))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition.x += Random.Range(spawnRange.x * -1, spawnRange.x);
            spawnPosition.y += Random.Range(spawnRange.y * -1, spawnRange.y);

            GameObject snake = Instantiate(snakePrefab, spawnPosition, Quaternion.identity);
            snake.GetComponent<Movement>().setInputChannelSo(player.movementInputChannelSo);
            Snake snakeComponent = snake.GetComponent<Snake>();
            snakeComponent.setID(player.ID);
            snakeComponent.setColor(player.color);
            snakeComponent.setTrailGradient(player.trailGradient);
        }
    }

}
