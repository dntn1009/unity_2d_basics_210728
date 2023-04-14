using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    GameItem m_item;
    [SerializeField]
    bool m_isSelect;
    Inventory m_inven;
    public bool IsEmpty {  get { return m_item == null; } }// get으로 아이템 값이 널값이면 false가 됌.
    public bool IsSelect { get { return m_isSelect; } set { m_isSelect = value; } }

    public void SetSlot(Inventory inven)
    {
        m_inven = inven;
    }
    
    public void OnSelect()
    {
        m_inven.OnSelectSlot(this);
    }

    public void InitSlot(GameItem item)
    {
        m_item = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;

    }
    public void UseItem()
    {
        if (IsEmpty) return;
        var count = m_item.Decrease();
        if (count == -1)
        {
            Destroy(m_item.gameObject);
            m_item = null;
        }

    }
    void Start()
    {
        /* var obj = GameObject.Find("Panel_Inven");
         m_inven = obj.GetComponent<Inventory>();*/
        // 이렇게 쓰면 안좋음 일일이 게임오브젝트 다 하나씩 읽어가면서 확인하는거라 안좋음
      /*  var obj = GameObject.FindGameObjectWithTag("Inventory");
        m_inven = obj.GetComponent<Inventory>();*/
      // 애초에 가지고있는 정보들이기 때문에 이렇게 할 필요가 없다. 그래서 위의 setslot 함수를 만듬.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
