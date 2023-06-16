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

    public void Refresh()
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
        livesChannelSo.OnEventRaised += ( (ID, lives) => RenderLives(ID, lives));
        scoreChannelSo.OnEventRaised += ( (ID, score) => RenderScore(ID, score));
        playerLostChannelSo.OnEventRaised += ( (ID) => RenderLost(ID));
    }

    private void OnDisable()
    {
        // remove events
        livesChannelSo.OnEventRaised -= ( (ID, lives) => RenderLives(ID, lives));
        scoreChannelSo.OnEventRaised -= ( (ID, score) => RenderScore(ID, score));
        playerLostChannelSo.OnEventRaised -= ( (ID) => RenderLost(ID));
    }

    private void RenderLives(int ID, int lives)
    {
        if(IDMatches(ID))
        {
            livesTMP.text = "Lives " + lives;
        }
    }

    private void RenderScore(int ID, int score)
    {
        if(IDMatches(ID))
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


}
