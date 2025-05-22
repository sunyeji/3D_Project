using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;  //싱글톤 언더바 소문자
    
    public static PlayerManager Instance //싱글톤 언더바 소문자, static은 모든곳에 공유되며, 하나만 존재해야함.
    {
        get //외부에서 인스텐스로 접근할수있게 해주는 getter프로퍼티
        {
            if (_instance == null) //만약 소문자 인스턴스가 없다면
            {    // 새로운 GameObject를 만들고, 그 위에 CharacterManager 컴포넌트를 붙이고 인ㄴ스턴스를 초기화합니다.
                _instance =new GameObject("CharactorManager").AddComponent<PlayerManager>(); 
            }
            return _instance;  //그 뒤에 소문자 인스턴스로 반환ㅐ
        }
    }public Player player; //플레이어도 선언
    public Player Player
    {
        get { return player; } 
        set { player = value; }
    }

    private void Awake()
    {
        if (_instance == null) //인스턴스가 비어있을때
        {
            _instance = this; //나를 집어넣는다
            DontDestroyOnLoad(gameObject); //씬을 이동하더라도 파괴되지 않는 문구
        }

        else //인스턴스가 넣이 아닐때
        {
            if (_instance == this) //인스턴스에 있는것과 내 자신이 다르다면
            {
                Destroy(gameObject); //현재것을 파괴해줘라.
            }
        }
    }
   
}
