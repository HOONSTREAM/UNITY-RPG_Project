using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawning_Pool_South2 : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0; //현재 몬스터가 몇마리 있는지
    int reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0; //유지시켜야 하는 몬스터 개체 수

    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    float spawnradius = 55.0f;
    [SerializeField]
    float spawnTime = 3.0f;

    public void AddMonsterCount(int value)
    {
        _monsterCount += value;
    }
    public void SetKeepMonsterCount(int count)
    {
        _keepMonsterCount = count;
    }


    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }


    void Update()
    {
        while (reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(ReserveSpawn_Turtle_Slime());
            StartCoroutine(ReserveSpawn_Punchman());
        }
    }

    IEnumerator ReserveSpawn_Turtle_Slime()
    {
        reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Turtle_Slime");
        NavMeshAgent nma = obj.GetAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while (true)
        {

            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, spawnradius); // 방향벡터가 나옴 
            randDir.y = 0;
            randPos = spawnPosition + randDir;

            //갈수 있는가?
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
            {
                break;

            }

        }
        obj.transform.position = randPos;
        reserveCount--;

    }

    IEnumerator ReserveSpawn_Punchman()
    {
        reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Punch_man");
        NavMeshAgent nma = obj.GetAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {

            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, spawnradius); // 방향벡터가 나옴 
            randDir.y = 0;
            randPos = spawnPosition + randDir;

            //갈수 있는가?
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
            {
                break;

            }

        }
        obj.transform.position = randPos;
        reserveCount--;

    }


}
