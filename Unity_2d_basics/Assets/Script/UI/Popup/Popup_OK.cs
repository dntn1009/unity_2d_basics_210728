using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_OK : MonoBehaviour
{
    [SerializeField]
    UILabel m_subject;
    [SerializeField]
    UILabel m_body;
    [SerializeField]
    UILabel m_okBtnLabel;

    ButtonDelegate m_okbtnDel;
  
    public void SetUI(string subject, string body, ButtonDelegate okBtnDel, string okBtnText = "OK")
    {
        m_subject.text = subject;
        m_body.text = body;
        m_okbtnDel = okBtnDel;
        m_okBtnLabel.text = okBtnText;
    }

    public void OnPressOK()
    {
        if (m_okbtnDel != null)
        {
            m_okbtnDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        /* SetUI("공지", "안녕하세요", null, null);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
