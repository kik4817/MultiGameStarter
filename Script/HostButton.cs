using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

// Network ������ �� �� ������ ����ϸ� �Ǿ�����?
// ��Ʈ��ũ���� Spawn�� �Ǿ��� �� ����
// ������ ��ư�� Host�� ���� Ȱ��ȭ�ϵ��� �ڵ带 �ۼ��غ�����

public class HostButton : NetworkBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
