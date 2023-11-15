using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //������ ���� ����
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
        //�� �� ��
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

    //������ ���� ������ �� �� ���ڶ��� ���� ����
    public void SpawnEnemy()
    {
        if(nowMobCount < maxMobCount)
        {
            

            for (; nowMobCount < maxMobCount; nowMobCount++)
            {
                
                /*
                //�׽�Ʈ�� �ӽ� �ڵ�
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


                //���� ���� ���� Ư�� �� �̻��϶� ��ȯ�Ǵ� ���� ����, ��ȯ�Ǵ� ���� Ȯ��, �ִ� ���� ���� ����
                switch (bossKillCount)
                {
                    //���� 0���� ��ɽ� 5����3, 5����2�� Ȯ���� ������, ���̷��� ����
                    case 0:
                        slimePercent = 45;
                        skelletonPercent = 90;

                        if(spawnMobChoose >= 0 && spawnMobChoose <= slimePercent)
                            spawnPoint = Instantiate(slime, critSpawnPos, Quaternion.identity);
                        else if(spawnMobChoose > slimePercent && spawnMobChoose <= skelletonPercent)
                            spawnPoint = Instantiate(skelleton, critSpawnPos, Quaternion.identity);
                        break;

                    //���� 1���� ��ɽ� ������ Ȯ�� ���߰� ���̷��� Ȯ�� ��, ���� Ȯ���� ��ũ ����, �ִ� ���� ���� ����
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

                    //���� 2���� ��ɽ� ������ Ȯ�� �ſ쳷�� ���̷��� ����, �߰� Ȯ���� ��ũ ����, �ִ� ���� ���� ����
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
                    //���� ���� �ϳ� ��ɽ� �ִ� ��� / ����ִ�ü�� ����
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
        // ���� ���� ��� ���� ������Ʈ�� ������
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
