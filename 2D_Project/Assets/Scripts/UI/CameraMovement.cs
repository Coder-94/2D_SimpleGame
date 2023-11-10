using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform       playerTransform;
    [SerializeField]
    Vector3         cameraPosition;

    [SerializeField]
    Vector2         center;
    [SerializeField]
    Vector2         mapSize;
    [SerializeField]
    float           cameraMoveSpeed;
    float           height;
    float           width;

    void Start()
    {
        //Player라는 이름의 게임오브젝트 검색 후 Transform 컴포넌트 가져오기
        playerTransform =   GameObject.Find("Player").GetComponent<Transform>();

        //카메라의 정중앙에서 세로 까지의 수직선의 길이(=높이의 절반)
        height =            Camera.main.orthographicSize;
        //카메라 세로 * 현재 해상도에 따른 가로 크기 / 현재 해상도에 따른 세로 크기
        //즉, 카메라 정중앙에서 가로까지의 수직선의 길이(=너비의 절반)
        width =             height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    //카메라 이동가능구역 확인을 위한 코드
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
