using Unity.Netcode.Components;
using UnityEngine;

// server�� ���� ������ ������ �� ������ �մϴ�.

public class ClientNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
