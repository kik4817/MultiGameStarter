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
        // 현제 체력에서 value 빼주는 것
        ModifyHealth(-value);
    }

    public void RestoreHealth(int value)
    {
        // 현재 체력에서 value 더해주는 것
        ModifyHealth(value);
    }

    private void ModifyHealth(int value)
    {
        // 죽었으면? -> 실행하지마라
        if(isDead) { return; }

        int newHealth = CurrentHealth.Value + value;
        // newHealth 버그로 인해서 무한대, 최대체력을 넘어간다.
        CurrentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);

        if(CurrentHealth.Value == 0)
        {
            // 이 객체가 사망했습니다. 이벤트를 발생한다
            OnDie?.Invoke(this);
            isDead = true;
        }
    }
}
