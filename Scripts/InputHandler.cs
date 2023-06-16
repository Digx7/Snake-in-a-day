using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    // Input channels
    [SerializeField] private Vector2EventChannelSO Player1InputChannelSO;
    [SerializeField] private Vector2EventChannelSO Player2InputChannelSo;
    [SerializeField] private Vector2EventChannelSO Player3InputChannelSo;
    [SerializeField] private Vector2EventChannelSO Player4InputChannelSo;

    private PlayerActions playerActions;

    protected override void Awake()
    {
        base.Awake();

        playerActions = new PlayerActions();

        playerActions.Snake.Move_Player_1.performed += ( (context) => Player1InputChannelSO.RaiseEvent(context.ReadValue<Vector2>()) );
        playerActions.Snake.Move_Player_2.performed += ( (context) => Player2InputChannelSo.RaiseEvent(context.ReadValue<Vector2>()) );
        playerActions.Snake.Move_Player_3.performed += ( (context) => Player3InputChannelSo.RaiseEvent(context.ReadValue<Vector2>()) );
        playerActions.Snake.Move_Player_4.performed += ( (context) => Player4InputChannelSo.RaiseEvent(context.ReadValue<Vector2>()) );
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
