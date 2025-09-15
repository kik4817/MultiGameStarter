using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIClient : NetworkBehaviour
{
    private UINetCodeTest netcodeTest;

    private void Awake()
    {
        netcodeTest = FindAnyObjectByType<UINetCodeTest>();
    }

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) { return; }
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) { return; }
    }

    private void Update()
    {
        if(!IsOwner){ return; }

        // fkey ������ ������?
        // Bus<IScoreEvent>.Raise
        // ���Ͱ� ó���Ǿ��� �� ������ ������.

        if (Keyboard.current.fKey.isPressed)
        {
            // ���� Host ����, Client ����
            AddPointServerRpc();
        }
    }

    /*
     * Rpc : Remote Procedual Call
     * ��Ʈ��ũ�� ����Ǿ� �ִ� ��ǻ�Ͱ� �ٸ� ��ǻ��, �������� �����϶�� ����� �ϱ� ���� ����
     * ������ ���� ������ �� �־�� �ϴµ�, ������ �ƴ� ��ǻ�Ϳ��� ���� �����ϰ� ���� ��쿡
     * �������� ���, �ٸ� ��ǻ�Ϳ� �ִ� �Լ��� ȣ���� �� �ִ� ���.
     * RequireOwnership = false : ������ ȣ���� �� �ִ� �Լ�
     * RequireOwnership = true : Owner�� �ִ� ����� ȣ���Ѵ�.
     */

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void AddPointServerRpc()
    {
        netcodeTest.OnScoreCall?.Invoke(1);
    }
}
