using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    private void Awake() 
    {
        PlayerManager.Instance.Player = this;             // 플레이어 매니저에 현재 플레이어 인스턴스를 등록
        controller = GetComponent<PlayerController>();    // 이 오브젝트에서 PlayerController 컴포넌트를 가져옴
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPad"))                  // 충돌한 객체의 태그가 "JumpPad"이면
        {
            controller.JumpByPad(5f);                    // 점프 패드를 통해 위로 점프 (점프력 5f)
        }
    }
    
    
}

