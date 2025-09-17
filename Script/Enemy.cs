using System;
using Unity.Netcode;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    [SerializeField] Health health;

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
    private void HandleEnemyDie()
    {
        Destroy(gameObject);
    }
}
