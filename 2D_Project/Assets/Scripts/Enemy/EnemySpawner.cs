using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum mobType
{

}

//CameraMovement���� �� �� ��ǥ�� �������� ���� ���
public class EnemySpawner : MonoBehaviour
{
    //������ ���� ����
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

    //������ ���� ������ �� �� ���ڶ��� ���� ����
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
            //���� ���� ���� Ư�� �� �̻��϶� ��ȯ�Ǵ� ���� ����, ��ȯ�Ǵ� ���� Ȯ��, �ִ� ���� ���� ����
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
