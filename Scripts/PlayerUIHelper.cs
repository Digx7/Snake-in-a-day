using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private int ID;
    public void setID (int ID) 
    {
        this.ID = ID;
        Refresh();
    }
    [SerializeField] private PlayerValueUpdateChannelSO livesChannelSo;
    [SerializeField] private PlayerValueUpdateChannelSO scoreChannelSo;
    [SerializeField] private IntEventChannelSO playerLostChannelSo;

    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI livesTMP;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI deadTMP;

    private void Awake()
    {
        Refresh();
    }

    private void Refresh()
    {
        nameTMP.text = "Player " + ID;
    }

    public void Reset()
    {
        Refresh();
        livesTMP.text = "Lives 3";
        scoreTMP.text = "Score 0";
        deadTMP.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // set up events
        livesChannelSo.OnEventRaised += ( (player, lives) => RenderLives(player, lives));
        scoreChannelSo.OnEventRaised += ( (player, score) => RenderScore(player, score));
        playerLostChannelSo.OnEventRaised += ( (ID) => RenderLost(ID));
    }

    private void OnDisable()
    {
        // remove events
        livesChannelSo.OnEventRaised -= ( (player, lives) => RenderLives(player, lives));
        scoreChannelSo.OnEventRaised -= ( (player, score) => RenderScore(player, score));
        playerLostChannelSo.OnEventRaised -= ( (ID) => RenderLost(ID));
    }

    private void RenderLives(Player player, int lives)
    {
        if(IDMatches(player))
        {
            livesTMP.text = "Lives " + lives;
        }
    }

    private void RenderScore(Player player, int score)
    {
        if(IDMatches(player))
        {
            scoreTMP.text = "Score " + score;
        }
    }

    private void RenderLost(int ID)
    {
        if(IDMatches(ID))
        {
            deadTMP.gameObject.SetActive(true);
        }
    }

    private bool IDMatches(int ID)
    {
        if(ID == this.ID)
        {
            return true;
        }
        else return false;
    }

    private bool IDMatches(Player player)
    {
        if(player.ID == this.ID)
        {
            return true;
        }
        else return false;
    }


}
