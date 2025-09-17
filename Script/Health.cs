using System;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int MaxHealth = 10;

    public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();

    private bool isDead = false;
    public Action<Health> OnDie;

    public override void OnNetworkSpawn()
    {
        if(!IsServer) {  return; }

        CurrentHealth.Value = MaxHealth;
    }

    public override void OnNetworkDespawn()
    {
        
    }

    public void TakeDamage(int value)
    {
        // ���� ü�¿��� value ���ִ� ��
        ModifyHealth(-value);
    }

    public void RestoreHealth(int value)
    {
        // ���� ü�¿��� value �����ִ� ��
        ModifyHealth(value);
    }

    private void ModifyHealth(int value)
    {
        // �׾�����? -> ������������
        if(isDead) { return; }

        int newHealth = CurrentHealth.Value + value;
        // newHealth ���׷� ���ؼ� ���Ѵ�, �ִ�ü���� �Ѿ��.
        CurrentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);

        if(CurrentHealth.Value == 0)
        {
            // �� ��ü�� ����߽��ϴ�. �̺�Ʈ�� �߻��Ѵ�
            OnDie?.Invoke(this);
            isDead = true;
        }
    }
}
