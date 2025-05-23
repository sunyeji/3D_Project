using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
      public float moveSpeed; //움직이는 속도
      private Vector2 moveInput; //좌우(x) 앞뒤(y) 이동방향
      private Rigidbody rb; //리기드바디 저장할 변수
      public float jumpPower; //점프하는 힘
      public LayerMask groundLayerMask; //땅이 어떤레이어인지 지정해주는 변수.
    
      [Header("Look")] 
      public Transform cameraContainer; //카메라 컨테이너 회전관련 변수. 카메라가 달린 부모오브젝트.
      public float minXLook; // 카메라 위쪽 회전 한계점
      public float maxXLook; // 카메라 아래쪽 회전 한계점
      private float camCurXRot; // 현재 X축 회전 각도 (상하 시야)
      public float lookSensitivity; //마우스 회전 민감도
      private Vector2 mouseDelta;  //마우스 변화량(델타값) , 이전프레임과 지금프레임 사이의 마우스 이동거리.
      void Start() 
        {
            rb = GetComponent<Rigidbody>(); //Rigidbody(유니티에 있는) 가져와서 변수(rb)에 저장.
        }
        
        
      public void OnMove(InputAction.CallbackContext context)  // 입력 시스템이 Move 액션을 감지했을 때 호출되는 함수
        {
            if (context.phase == InputActionPhase.Performed) //만약 입력이 눌러진 상태라면
            {
                moveInput = context.ReadValue<Vector2>(); // moveInput에 입력된 방향값(x,y)을 저장한다
            }
            else if (context.phase == InputActionPhase.Canceled) //그렇지않고 입력이 떨어졌을때는
            {
                moveInput = Vector2.zero; // 값을 0으로해준다(멈추게 해준다)
            }
        }
    
      void FixedUpdate()  // 물리 연산용 함수 (고정 프레임으로 실행됨) 
        {                       //플레이어의오른쪽방향(X축 기준)     //플레이어가 앞을 보는 방향(Z축 기준)   
            Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;   // WASD 입력을 "플레이어가 바라보는 방향 기준"으로 바꾸는 핵심 코드
            direction *= moveSpeed; //이동속도
            direction.y = rb.velocity.y; // 위아래(y) 속도는 기존 Rigidbody의 y속도를 유지 (중력이나 점프 대비)
    
            rb.velocity = direction; //계산된 속도로 리기드바디 이동
        }
      
        public void OnJump(InputAction.CallbackContext context)  // 입력 시스템이 Move 액션을 감지했을 때 호출되는 함수
        {
            
            if(context.phase == InputActionPhase.Started && IsGrounded()) // 입력이 시작된 시점이고 && 현재 땅에 닿아 있을 경우에만 점프
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // Rigidbody에 위쪽 방향으로 힘을 가해 점프
                Debug.Log("Jump");
            }
        }
        
        public void OnLook(InputAction.CallbackContext context) // 마우스 움직임을 받아 캐릭터와 카메라를 회전시키는 함수
        {
            mouseDelta = context.ReadValue<Vector2>();  // 마우스 움직임(델타)를 2D 벡터 형태로 읽어옴 (x: 좌우, y: 상하)

            // 상하 회전 (카메라 컨테이너 회전)
            camCurXRot += mouseDelta.y * lookSensitivity;    // 카메라의 상하 회전: 마우스 y축 움직임에 따라 회전 각도 조정
            camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);  // 회전 각도가 너무 크거나 작아지지 않도록 Clamp로 제한
            cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);    // 카메라 컨테이너를 상하로 회전시킴 (로컬 회전 기준)

            // 좌우 회전 (플레이어 본체 회전)
            transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);  // 캐릭터 본체를 좌우로 회전시킴 (transform.eulerAngles는 월드 기준)
        }
        
        bool IsGrounded()
        {
            // 플레이어 기준으로 네 방향(앞, 뒤, 좌, 우)에서 아래 방향으로 Ray를 발사합니다. (총 4개 만들기)
            // 약간 위(transform.up * 0.01f)에서 시작해 바닥에 닿는지 검사.
            Ray[] rays = new Ray[4]
            {
                // 앞쪽에서 아래로
                new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                // 뒤쪽에서 아래로
                new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
                // 오른쪽에서 아래로
                new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
                // 왼쪽에서 아래로
                new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
            };

            // 위에서 만든 4개의 Ray 중 하나라도 groundLayerMask에 닿으면 땅에 있다고 판단
            for(int i = 0; i < rays.Length; i++)
            {
                if (Physics.Raycast(rays[i], 0.8f, groundLayerMask))// Ray를 아래로 0.8 유닛 쏘고, 바닥에 닿는지 확인
                {
                    return true; // 닿았다면 Grounded 상태임
                }
            }
            
            return false;  // 4개 모두 닿지 않았다면 공중에 떠 있다고 판단
        }
        
        private void Awake() 
        {
            rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져와서 rb에 저장
        }

        public void JumpByPad(float jumpPower)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // y축 속도를 0으로 초기화해서 기존 점프/낙하 영향 제거
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);      // 위 방향으로 즉시 점프력(Impulse) 적용
        }
}
