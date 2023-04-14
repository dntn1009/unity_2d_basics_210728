using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ButtonDelegate();

public class PopupManager : DonDestory<PopupManager>
{
    [SerializeField]
    GameObject m_popupOkCancelPrefab;
    [SerializeField]
    GameObject m_popupOKPrefab;

    List<GameObject> m_popList = new List<GameObject>();
    int m_popupDepth = 1000;
    int m_popDepthGap = 10;
    public void OpenPopupOkCancel(string subject, string body, ButtonDelegate okBtnDel, ButtonDelegate cancelBtnDel, string okBtnText = "OK", string cancelBtnText = "CANCEL")
    {
        var obj = Instantiate(m_popupOkCancelPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();// 해당 객체를 모두 반환하는 것.
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popList.Count * m_popDepthGap + i;
        }
        var popup = obj.GetComponent<Popup_OKCancel>();
        popup.SetUI(subject, body, okBtnDel, cancelBtnDel, okBtnText, cancelBtnText);
        m_popList.Add(obj);

    }

    public void OpenPopupOk(string subject, string body, ButtonDelegate okBtnDel, string okBtnText = "OK")
    {
        var obj = Instantiate(m_popupOKPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();// 해당 객체를 모두 반환하는 것.
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popList.Count * m_popDepthGap + i;
        }
        var popup = obj.GetComponent<Popup_OK>();
        popup.SetUI(subject, body, okBtnDel, okBtnText);
        m_popList.Add(obj);

    }

    protected override void OnStart()
    {
        m_popupOkCancelPrefab = Resources.Load("Prefab/Popup/PopupOkCancel") as GameObject;
        m_popupOKPrefab = Resources.Load("Prefab/Popup/PopupOK") as GameObject;
    }

    public bool IsPopupOpen()
    {
        return m_popList.Count > 0;
    }

    public void ClosePopup()
    {
        if(m_popList.Count > 0)
        {
            Destroy(m_popList[m_popList.Count - 1]);//팝업 자체를 지움. 
            m_popList.RemoveAt(m_popList.Count - 1);//팝업 자체가 지워진건아님
        }
    }
    // Update is called once per frame
    int count;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (count % 2 == 0)
                OpenPopupOkCancel("Notice", "안녕하세요 정수레 입ㄴ디ㅏ.", null, null, "확인", "취소");
            else
                OpenPopupOk("오류안내", "테스트용 오류 알람입니다. 게임을 종료합니다.", null, "확인");
            count++;
        }
    }
}
