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
    // ���� ��ȯ ���� : ��ȯ�� ��� �Ϲ� ���Ͱ� �������� ��ȯ�϶�

    private List<Enemy> spawnedEnemy = new();


    // ���� ��ȯ�� �����ϴ� �Լ�
    // �ѹ��� ������ �� ���ΰ�? -> Start
    // �������� ���ļ� ������ �� ���ΰ�? -> Update // Event


    // �Ϲ� ���͸� ��ȯ�� �մϴ�.
    // ���͸� ��ȯ��  �ϰ� ��ȯ�� ���͸� SpawnedMonster ����
    // ���Ͱ� ��� óġ���Ǹ� ���� ���͸� ��ȯ�϶�� �̺�Ʈ�� ����
    // Spawn(1)

    // Vector3.zero ��ſ� Spawn(int index, Vector3 spawnPos��ġ ���� ���޹޴� ����)
    // ���Ͱ� ��ȯ�� ��ġ�� �����ؼ� ��������
    // RandomPos�� Random �ڵ带 ����Ͽ� �����غ�����

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

            // random���� ���� ���� ���� Ȯ���� ����
            // ���� ��ȯ�� ��ġ�� �̹� ��ȯ�� � ��ü�� �ִٸ� ��ȯ���� ������
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
