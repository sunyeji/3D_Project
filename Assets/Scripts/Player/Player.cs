using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    private void Awake() 
    {
        PlayerManager.Instance.Player = this;  
        controller = GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPad"))
        {
            controller.JumpByPad(10f); // 자동 점프
        }
    }
}

