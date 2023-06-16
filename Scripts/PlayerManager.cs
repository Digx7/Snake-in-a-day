using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<Player> players;
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private GameState shouldResetOnThisState;
    [SerializeField] private PlayerValueUpdateChannelSO deathChannel;
    [SerializeField] private IntEventChannelSO lostChannel;
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;
    [SerializeField] private PlayerScriptableObjectChannelSO setUpChannel;

    private List<int> playersWhoLost;

    protected override void Awake()
    {
        base.Awake();

        playersWhoLost = new List<int>();
    }

    private void OnEnable()
    {
        deathChannel.OnEventRaised += (player, lives) => SpawnSnake(player);
        lostChannel.OnEventRaised += (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised += (state) => {if(state == shouldResetOnThisState) Reset();};
    }

    private void OnDisable()
    {
        deathChannel.OnEventRaised -= (player, lives) => SpawnSnake(player);
        lostChannel.OnEventRaised -= (ID) => OnPlayerLost(ID);
        gameStateRequestChannelSO.OnEventRaised -= (state) => {if(state == shouldResetOnThisState) Reset();};
    }

    private void Reset()
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
