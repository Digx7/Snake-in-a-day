using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    [SerializeField] private int ID;
    [SerializeField] private int score;
    [SerializeField] private int multiplier = 1;
    [SerializeField] private int lives;

    [SerializeField] private IntEventChannelSO playerDeathChannelSo;
    [SerializeField] private IntEventChannelSO playerGotFruitChannelSo;
    [SerializeField] private IntEventChannelSO playerLostChannelSo;

    private const int FRUIT_VALUE = 10;


    private void OnEnable()
    {
        playerGotFruitChannelSo.OnEventRaised += ((ID) => IncreaseScore(ID));
        playerDeathChannelSo.OnEventRaised += ((ID) => Die(ID));
    }

    private void IncreaseScore(int ID)
    {
        if(ID == this.ID)
        {
            score += (FRUIT_VALUE * multiplier);
        }
    }

    private void Die(int ID)
    {
        if(ID == this.ID)
        {
            lives--;
            if(lives == 0) playerLostChannelSo.RaiseEvent(ID);
        }
    }

    private void IncreaseMultipliyer(int amount)
    {
        this.multiplier *= amount;
    }

    private void ResetMultiplier()
    {
        this.multiplier = 1;
    }

    public void Reset()
    {
        score = 0;
        multiplier = 1;
        lives = 3;
    }

}
