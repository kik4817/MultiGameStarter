using Unity.Netcode.Components;
using UnityEngine;


/// <summary>
/// �� Ŭ������ ������ ��Ʈ��ũ ������Ʈ�� Ŭ���̾�Ʈ�� ���ؼ��� �̵��� �� �� �ִ�.
/// </summary>
public class ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }    
}