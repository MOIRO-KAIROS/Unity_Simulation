using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject turtlebot3; //카메라가 쫓아다닐 대상입니다.
    float Ymin = 5.0f; //카메라가 내려오는 최저각입니다. 더 내리고 싶다면 내려줍시다.
    float Ymax = 40.0f; //카메라가 올라가는 최고각입니다. 90도를 넘어가면 뒤로 돌아갑니다.
    Camera cam; //카메라 변수입니다.
    Transform lookat; //카메라가 쫓아다닐 위치입니다.
    Transform camtrans; //카메라의 위치입니다.
    float Xnow = 0.0f; //카메라가 움직이는 좌우경로변수입니다.
    float Ynow = 15.0f; //카메라가 움직이는 상하경로변수입니다.
    // Vector3 offset = new Vector3(0, 0.01f, 0);

    public float distance = 1.0f; //카메라가 물체로부터 떨어져 있는 거리입니다.
    public float RotateX = 3.0f; //카메라의 회전속도입니다. 캐릭터 회전 속도와 똑같아야 합니다.

    // Start is called before the first frame update
    void Start()
    {
        lookat = turtlebot3.gameObject.transform.transform; //캐릭터로부터위치를 받아옵니다.
        camtrans = transform; //카메라 위치를 받아옵니다.
        cam = Camera.main; //메인 카메라를 cam변수에 할당합니다.
    }

    // Update is called once per frame
    void Update()
    {
        Xnow += Input.GetAxis("Mouse X"); //카메라의 좌우경로입력을 받습니다.
        Ynow -= Input.GetAxis("Mouse Y"); //카메라의 상하경로입력을 받습니다.
        Ynow = Mathf.Clamp(Ynow, Ymin, Ymax); //카메라의 상하 변동폭을 제한합니다.
        if (turtlebot3 == null) //플레이어가 사라졌을 경우 오류를 방지하기 위한 부분입니다. 사라짐을 감지합니다.
        {
            gameObject.GetComponent<FollowCam>().enabled = false; // 해당 스크립트를 비활성화합니다.
        }
    }

    private void LateUpdate()
    //바로바로 반응하는 것이 아닌, 위 행동을 한 후에 뒤따라 실행합니다. 부드러운 카메라 움직임이 나옵니다.
    {
        Vector3 dir = new Vector3(0, 2, -distance); //거리를 벌려줍니다.
        Quaternion rotation = Quaternion.Euler(Ynow * 2, Xnow * RotateX, 90);
        //회전값을 만듭니다. 상하는 2배, 좌우는 RotateX(캐릭터회전값)를 곱해주었습니다.
        camtrans.position = lookat.position + rotation * dir; //카메라를 위치를 지정해 줍니다.
        camtrans.LookAt(lookat.position); //카메라가 목적지를 바라보게 합니다.
    }
}