using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

// Network 연동을 할 때 무엇을 상속하면 되었나요?
// 네트워크에서 Spawn이 되었을 때 조건
// 참조한 버튼을 Host일 때만 활성화하도록 코드를 작성해보세요

public class HostButton : NetworkBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
