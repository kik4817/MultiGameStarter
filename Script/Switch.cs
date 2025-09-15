using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Switch : NetworkBehaviour
{
    // NetworkObject������ ������Ʈ�� NetworkBehaviour ��ӽ�Ű���� ������ ���ּ���

    // NetworkVariable ������ �غ�����. ������ �浹 �̺�Ʈ�� ���� �� �����ϰ� �ִ��� ���ϴ��� üũ�ϴ� ������Ʈ
    // bool

    public NetworkVariable<bool> IsSwitchOn = new NetworkVariable<bool>(false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    List<Collider2D> triggerColliders = new(); // �������� �浹�̺�Ʈ�� �߻����� ��
    
    // OnNetworkSpawn, OnNetworkDespawn ������Ʈ�� ������ ��� �޾ƾ� �ϴ��� ����. (Server, <Host, Client>)
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            enabled = false;
        }

        IsSwitchOn.OnValueChanged += OnSwitchChanged;
    }

    private void OnSwitchChanged(bool previousValue, bool newValue)
    {
        // ����ġ�� On�� �Ǿ animator�� ����˴ϴ�.
    }

    public override void OnNetworkDespawn()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ��ǻ�Ϳ��� ���� ��ǻ�Ϳ� ������ �÷��̾ �����̴� ������ �������ݴϴ�.
        // ������ ��ü�� ���� ��ǻ�;ȿ��� �浹���� �� �߻��ϴ� �̺�Ʈ.

        triggerColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerColliders.Remove(collision);
    }

    private void FixedUpdate() // ������ ����� ó���� �� ȣ���ϸ� ����. TriggerEvent �߻� �Ŀ� ������ ����ȴ�.
    {
        if(!IsSpawned) { return; }

        triggerColliders.RemoveAll(col => col == null); // ����ó��, null�� ���·� �����̵Ǹ� �� null �����϶�.

        IsSwitchOn.Value = triggerColliders.Count > 0; // List������ 1���� ũ�� true �ƴϸ� false
    }
}
