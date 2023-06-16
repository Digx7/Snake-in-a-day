using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public int ID;
    public int score;
    public int multiplier = 1;
    public int lives;
    [SerializeField] private IntEventChannelSO playerDeathChannelSo;
    [SerializeField] private IntEventChannelSO playerGotFruitChannelSo;
    [SerializeField] private IntEventChannelSO playerLostChannelSo;
    [SerializeField] private PlayerValueUpdateChannelSO playerScoreUpdateChannelSO;
    [SerializeField] private PlayerValueUpdateChannelSO playerLivesUpdateChannelSO;

    public Color color;
    public Gradient trailGradient;
    public Vector2EventChannelSO movementInputChannelSo;


    private void OnEnable()
    {
        playerGotFruitChannelSo.OnEventRaised += ((ID) => IncreaseScore(ID));
        playerDeathChannelSo.OnEventRaised += ((ID) => Die(ID));
    }

    private void IncreaseScore(int ID)
    {
        if(ID == this.ID)
        {
            score += (GlobalConstants.FRUIT_VALUE * multiplier);
            playerScoreUpdateChannelSO.RaiseEvent(this, score);
        }
    }

    private void Die(int ID)
    {
        if(ID == this.ID)
        {
            lives--;
            playerLivesUpdateChannelSO.RaiseEvent(this, lives);
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
        this.score = 0;
        this.multiplier = 1;
        this.lives = 3;
    }

}
