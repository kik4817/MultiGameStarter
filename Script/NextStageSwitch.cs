using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Essence;

public class NextStageSwitch : NetworkBehaviour
{
    public NetworkVariable<bool> IsSwitchOn = new NetworkVariable<bool>(false,
    NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Server);

    List<Collider2D> triggerColliders = new();
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            enabled = false;
        }

        IsSwitchOn.OnValueChanged += NextStageChanged;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer)
        {
            enabled = false;
        }

        IsSwitchOn.OnValueChanged -= NextStageChanged;
    }

    private void NextStageChanged(bool previousValue, bool newValue)
    {
        //if(newValue == true)
        //{
            
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerColliders.Remove(collision);
    }

    private void FixedUpdate() // 물리적 계산을 처리할 때 호출하면 좋다. TriggerEvent 발생 후에 실행이 보장된다.
    {
        if (!IsSpawned) { return; }

        triggerColliders.RemoveAll(col => col == null); // 예외처리, null인 상태로 저장이되면 그 null 삭제하라.

        IsSwitchOn.Value = triggerColliders.Count > 0; // List갯수가 1보다 크면 true 아니면 false
    }

}
