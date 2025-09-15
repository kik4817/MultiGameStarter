using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class UINetCodeTest : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private int value = 10;

    NetworkVariable<int> ScoreValue = new NetworkVariable<int>(10,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    public Action<int> OnScoreCall;
    //public Func<int, int> MyFunc;
    //public Func<string, int> MyFunc2;

    //public void Fun() { }

    // 넥트워크 코드는 서버에서 실행하는지, 클라이언트에서 실행해야 하는지 구분을 해야한다.
    // 조건문을 사용해서 서버인지, 서버가 아닌지 구분하고 각 상태에 맞는 함수를 연결하는 식으로 코드를 구현한다.

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            OnScoreCall += HandleAddPoint;
        }

        ScoreValue.OnValueChanged += OnScoreValueChanged;
    }

    private void HandleAddPoint(int value)
    {
        ScoreValue.Value += value;
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            OnScoreCall -= HandleAddPoint;
        }
        ScoreValue.OnValueChanged -= OnScoreValueChanged;
    }

    private void OnScoreValueChanged(int previousValue, int newValue)
    {
        scoreText.SetText($"ScoreValue : {newValue}");
    }
}