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

    private void FixedUpdate() // ������ ����� ó���� �� ȣ���ϸ� ����. TriggerEvent �߻� �Ŀ� ������ ����ȴ�.
    {
        if (!IsSpawned) { return; }

        triggerColliders.RemoveAll(col => col == null); // ����ó��, null�� ���·� �����̵Ǹ� �� null �����϶�.

        IsSwitchOn.Value = triggerColliders.Count > 0; // List������ 1���� ũ�� true �ƴϸ� false
    }

}
