using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_OKCancel : MonoBehaviour
{
    [SerializeField]
    UILabel m_subject;
    [SerializeField]
    UILabel m_body;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    UILabel m_cancelBtnLabel;

    ButtonDelegate m_okbtnDel;
    ButtonDelegate m_cancelBtnDel;
    public void SetUI(string subject, string body, ButtonDelegate okBtnDel, ButtonDelegate cancelBtnDel, string okBtnText = "OK", string cancelBtnText = "CANCEL")
    {
        m_subject.text = subject;
        m_body.text = body;
        m_okbtnDel = okBtnDel;
        m_cancelBtnDel = cancelBtnDel;
        m_okBtnLabel.text = okBtnText;
        m_cancelBtnLabel.text = cancelBtnText;
    }

    public void OnPressOK()
    {
        if(m_okbtnDel != null)
        {
            m_okbtnDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }

    public void OnPressCancel()
    {
        if(m_cancelBtnDel != null)
        {
            m_cancelBtnDel();
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
