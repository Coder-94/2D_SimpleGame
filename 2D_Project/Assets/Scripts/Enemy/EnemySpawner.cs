using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //생성할 몬스터 선언
    [SerializeField]
    GameObject          slime;
    [SerializeField]
    GameObject          skelleton;
    [SerializeField]
    GameObject          orc;

    Vector2             rightSpawnPos, leftSpawnPos, critSpawnPos;
    GameObject          spawnPoint;
    int                 spawnPosChoose, spawnMobChoose;

    int                 slimePercent, skelletonPercent, orcPercent = 0;
    int                 maxMobCount = 15;
    int                 nowMobCount = 0;
    int                 bossKillCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //양 맵 끝
        float lx = CameraMovement.instance.mapSize.x + CameraMovement.instance.center.x+2;
        float sx = CameraMovement.instance.center.x - CameraMovement.instance.mapSize.x-2;
        leftSpawnPos =    new Vector2(sx, 0);
        rightSpawnPos =   new Vector2(lx, 0);

        spawnPosChoose = Random.Range(0, 2);
        spawnMobChoose = Random.Range(0, 91);

        SpawnEnemy();

        int nowMobCount = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        NowMobCheck(nowMobCount);
        InvokeRepeating("SpawnEnemy", 0f, 2f);
    }

    //일정한 텀을 가지고 맵 각 끝자락에 몬스터 스폰
    public void SpawnEnemy()
    {
        if(nowMobCount < maxMobCount)
        {
            

            for (; nowMobCount < maxMobCount; nowMobCount++)
            {
                
                /*
                //테스트용 임시 코드
                switch (spawnPosChoose)
                {
                    case 0:
                        GameObject leftSpawnedMob = Instantiate(slime, leftSpawnPoint, Quaternion.identity);
                        break;
                    case 1:
                        GameObject rightSpawnedMob = Instantiate(slime, rightSpawnPoint, Quaternion.identity);
                        break;
                }*/

                
                switch (spawnPosChoose)
                {
                        case 0:
                            critSpawnPos = leftSpawnPos;
                            break;
                        case 1:
                            critSpawnPos = rightSpawnPos;
                            break;
                }


                //죽인 보스 수가 특정 수 이상일때 소환되는 몬스터 종류, 소환되는 몬스터 확률, 최대 몬스터 갯수 변경
                switch (bossKillCount)
                {
                    //보스 0마리 사냥시 5분의3, 5분의2의 확률로 슬라임, 스켈레톤 생성
                    case 0:
                        slimePercent = 45;
                        skelletonPercent = 90;

                        if(spawnMobChoose >= 0 && spawnMobChoose <= slimePercent)
                            spawnPoint = Instantiate(slime, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > slimePercent && spawnMobChoose <= skelletonPercent)
                            spawnPoint = Instantiate(skelleton, critSpawnPos, Quaternion.identity);
                        break;

                    //보스 1마리 사냥시 슬라임 확률 낮추고 스켈레톤 확률 업, 낮은 확률로 오크 출현, 최대 몬스터 갯수 증가
                    case 1:
                        maxMobCount = 25;
                        slimePercent = 30;
                        skelletonPercent = 80;
                        orcPercent = 90;

                        if(spawnMobChoose >= 0 && spawnMobChoose <= slimePercent)
                            spawnPoint = Instantiate(slime, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > slimePercent && spawnMobChoose <= skelletonPercent)
                            spawnPoint = Instantiate(skelleton, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > skelletonPercent && spawnMobChoose <= orcPercent)
                            spawnPoint = Instantiate(orc, critSpawnPos, Quaternion.identity);
                        break;

                    //보스 2마리 사냥시 슬라임 확률 매우낮음 스켈레톤 높음, 중간 확률로 오크 출현, 최대 몬스터 갯수 증가
                    case 2:
                        maxMobCount = 40;
                        slimePercent = 10;
                        skelletonPercent = 55;
                        orcPercent = 90;

                        if (spawnMobChoose >= 0 && spawnMobChoose <= slimePercent)
                            spawnPoint = Instantiate(slime, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > slimePercent && spawnMobChoose <= skelletonPercent)
                            spawnPoint = Instantiate(skelleton, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > skelletonPercent && spawnMobChoose <= orcPercent)
                            spawnPoint = Instantiate(orc, critSpawnPos, Quaternion.identity);
                        break;
                    //이후 보스 하나 사냥시 최대 잡몹 / 잡몹최대체력 증가
                    default:
                        if(bossKillCount >= 3)
                        {
                            maxMobCount += 2;

                            if (spawnMobChoose >= 0 && spawnMobChoose <= slimePercent)
                                spawnPoint = Instantiate(slime, critSpawnPos, Quaternion.identity);
                            else if (spawnMobChoose > slimePercent && spawnMobChoose <= skelletonPercent)
                                spawnPoint = Instantiate(skelleton, critSpawnPos, Quaternion.identity);
                            else if (spawnMobChoose > skelletonPercent && spawnMobChoose <= orcPercent)
                                spawnPoint = Instantiate(orc, critSpawnPos, Quaternion.identity);
                        }
                        break;
                }
                
            }
        }
    }

    int NowMobCheck(int layer)
    {
        // 현재 씬의 모든 게임 오브젝트를 가져옴
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(); 
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                count++;
            }
        }

        return count;
    }
}
