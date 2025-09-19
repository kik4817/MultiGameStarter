using System;
using Essence;
using Unity.Netcode;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    [SerializeField] Health health;

    public Action<Enemy> OnMonsterSpawned;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        health.OnDie += (health) => HandleEnemyDie();
        //health.OnDie += HandleEnemyDie();

    }


    public override void OnNetworkDespawn()
    {
        if (!IsServer) { return; }

        health.OnDie -= (health) => HandleEnemyDie();
    }
    protected virtual void HandleEnemyDie()
    {
        //Bus<IEnemyDeathEvent>
        //OnMonsterSpawned?.Invoke(this);
        NetworkManager.Destroy(gameObject);
    }

    // protected virtual void HandleEnemyDie(Health health)
}
