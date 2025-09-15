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

    // ��Ʈ��ũ �ڵ�� �������� �����ϴ���, Ŭ���̾�Ʈ���� �����ؾ� �ϴ��� ������ �ؾ��Ѵ�.
    // ���ǹ��� ����ؼ� ��������, ������ �ƴ��� �����ϰ� �� ���¿� �´� �Լ��� �����ϴ� ������ �ڵ带 �����Ѵ�.

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