using Unity.Netcode;
using UnityEngine;

public class EnemeyManager : NetworkBehaviour
{
    [SerializeField] Enemy enemyPrefab;

    public void Spawn()
    {
        Enemy instance = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

        instance.GetComponent<NetworkObject>().Spawn();
    }
}
