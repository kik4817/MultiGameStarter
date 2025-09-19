using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemeyManager : NetworkBehaviour
{
    //[SerializeField] List<Enemy> enemyList;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] List<Enemy> enemyList;

    [Header("Common Monster Spqwn Setting")]
    public int count = 5;
    [SerializeField] Transform spawnPosition;

    [Header("Boss Monster Spawn Setting")]
    [SerializeField] Transform BossSpawnPos;
    // 보스 소환 조건 : 소환된 모든 일반 몬스터가 없어지면 소환하라

    private List<Enemy> spawnedEnemy = new();


    // 몬스터 소환을 실행하는 함수
    // 한번만 실행이 될 것인가? -> Start
    // 여러번에 겹쳐서 실행이 될 것인가? -> Update // Event


    // 일반 몬스터를 소환을 합니다.
    // 몬스터를 소환을  하고 소환된 몬스터를 SpawnedMonster 저장
    // 몬스터가 모두 처치가되면 보스 몬스터를 소환하라는 이벤트를 실행
    // Spawn(1)

    // Vector3.zero 대신에 Spawn(int index, Vector3 spawnPos위치 값을 전달받는 인자)
    // 몬스터가 소환을 위치를 선택해서 만들어보세요
    // RandomPos를 Random 코드를 사용하여 구현해보세요

    public void Spawn()
    {
        Enemy instance = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

        instance.GetComponent<NetworkObject>().Spawn();
    }

    public void Spawn(int index)
    {
        Enemy instance = Instantiate(enemyList[index], Vector3.zero, Quaternion.identity);

        instance.GetComponent<NetworkObject>().Spawn();
    }

    public void Spawn(int index, Vector3 targetPos)
    {
        Enemy instance = Instantiate(enemyList[index], targetPos, Quaternion.identity);

        instance.GetComponent<NetworkObject>().Spawn();

        
    }

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            int rand = UnityEngine.Random.Range(-5, 6);

            Vector3 randomPosition = new Vector3(spawnPosition.position.x + rand,
                spawnPosition.position.y + rand,
                0);

            Spawn(0, randomPosition);

            // random에서 값은 값이 나올 확률이 존재
            // 지금 소환된 위치에 이미 소환된 어떤 객체가 있다면 소환하지 마세요
        }
    }

    private void Update()
    {
        if(!IsServer) { return; }
        if(!IsSpawned) { return; }

        if(Keyboard.current.oKey.isPressed)
        {
            Spawn(0);
        }
        
        if (Keyboard.current.pKey.isPressed)
        {
            Spawn(1);
        }
    }
}
