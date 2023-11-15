using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum mobType
{

}

//CameraMovement에서 맵 끝 좌표를 가져오기 위해 상속
public class EnemySpawner : MonoBehaviour
{
    //생성할 몬스터 선언
    [SerializeField]
    GameObject          slime;
    [SerializeField]
    GameObject          skelleton;
    [SerializeField]
    GameObject          orc;

    Vector2             rightSpawnPoint, leftSpawnPoint;

    //int                 slimePercent, skelletonPercent, orcPercent;
    int                 maxMobCount = 15;
   //int                bossKillCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        leftSpawnPoint =    new Vector2(-CameraMovement.instance.mapSize.x - 2, 0);
        rightSpawnPoint =   new Vector2(CameraMovement.instance.mapSize.x + 2, 0);

        SpawnEnemy(leftSpawnPoint, rightSpawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnEnemy(leftSpawnPoint, rightSpawnPoint);
    }

    //일정한 텀을 가지고 맵 각 끝자락에 몬스터 스폰
    public void SpawnEnemy(Vector2 leftSpawnPoint, Vector2 rightSpawnPoint)
    {
        for(int i=0; i<maxMobCount; i++)
        {
            int spawnPosChoose = Random.Range(0, 2);

            switch(spawnPosChoose)
            {
                case 0:
                    GameObject leftSpawnedMob = Instantiate(slime, leftSpawnPoint, Quaternion.identity);
                    break;
                case 1:
                    GameObject rightSpawnedMob = Instantiate(slime, rightSpawnPoint, Quaternion.identity);
                    break;
            }
            
            /*
            //죽인 보스 수가 특정 수 이상일때 소환되는 몬스터 종류, 소환되는 몬스터 확률, 최대 몬스터 갯수 변경
            switch (bossKillCount)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            */
        }
    }
}
