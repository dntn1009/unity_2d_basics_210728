using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{


 /*   bool m_isActive;
    bool m_isOpen;
    string m_id = "아이디를 입력하세요.";
    string m_pwd = "비밀번호를 입력하세요.";
    string[] m_weaponList = { "단검", "한손검", "바스타드", "학다리" };
    int m_select = -1;
    float m_height = 20;*/
    public void GoNextScene()
    {
        //   m_loadInfo = SceneManager.LoadSceneAsync(1);
        LoadScene.Instance.LoadSceneAsync(LoadScene.SceneState.SampleScene);

    }
/*    void OnGUI()
    {
        
        if(GUI.Button(new Rect((Screen.width - 150) / 2, (Screen.height - 50) / 2, 150, 50), "Start"))
        {
              Debug.Log("GAME START");
        }
        // if문 붙이면 이 버튼을 눌렀을시의 동작이 나옴.
        //10 년전에 툴없었을떄 사용하던 방식 요즘은 NGUI UGUI 가있음 => 이제 UGUI가 다 흡수해서 UGUI가 떠오름

        GUILayout.BeginArea(new Rect(10, Screen.height - 300, 200, 300), GUI.skin.box);
        GUILayout.Space(5);
        m_id = GUILayout.TextField(m_id);
        m_pwd = GUILayout.PasswordField(m_pwd, '*');
        GUILayout.Button("OPTION",GUILayout.Height(50));//매개변수의 좌표를 지정하는게 없다.
        GUILayout.Button("QUIT", GUILayout.Height(50));
        m_isActive = GUILayout.Toggle(m_isActive, "배경음악");//첫 Toggle안에있는 m_isActive는 false다.
        if(m_isActive)
        {
            GUILayout.TextArea("배경음악이 꺼졌습니다.");
        }
        GUILayout.Label("OnGUI 테스트 중..");
        GUILayout.EndArea();// begin 이후 end가 무조건 잇어야함
        //영역의 시작 해상도가 변경이되도 좌측 화면에 보여야한다.

        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - m_height, 200, 300), GUI.skin.box);
        m_isOpen = GUILayout.Toggle(m_isOpen, "무기 선택", GUI.skin.button);
        if(m_isOpen)
        {
            m_height = 300;
           m_select = GUILayout.SelectionGrid(m_select, m_weaponList, 1);
        }
        else
        {
            m_height = 20;
        }
        GUILayout.EndArea();
    }*/
    //기초 GUI에 대해 적혀있음.
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
