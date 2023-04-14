using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum ItemType
    {
        Ball,
        Bomb,
        Bowling_Ball,
        Coin,
        Hat,
        Magnet,
        Max
    }
    const int BASE_SLOT_COUNT = 24;
    [SerializeField]
    Sprite[] m_iconSprites;
    [SerializeField]
    GameObject m_itemPrefab;
    [SerializeField]
    GameObject m_slotPrefab;
    [SerializeField]
    UIGrid m_grid;
    [SerializeField]
    UIScrollView m_scrollView;
    [SerializeField]
    Transform m_cursor;
    List<ItemSlot> m_slotList = new List<ItemSlot>();
    UITweener[] m_tweener;

    public void OnSelectSlot(ItemSlot selectslot)
    {
        /*for(int i = 0; i< m_slotList.Count; i++)
        {
            if (m_slotList[i].IsSelect)
                m_slotList[i].IsSelect = false;
        }*/
        // 위의 포문을 압축한 것이 아래의 코딩
        var curSlot = m_slotList.Find(slot => slot.IsSelect);
        if(curSlot != null)
        curSlot.IsSelect = false;
        selectslot.IsSelect = true;
        if (!m_cursor.gameObject.activeSelf) 
        m_cursor.gameObject.SetActive(true);
        m_cursor.transform.position = selectslot.transform.position;
    }
    public void OnUseSlotItem()
    {
        var curSlot = m_slotList.Find(slot => slot.IsSelect);
        if (curSlot != null)
        {
            curSlot.UseItem();
        }
    }
    public void CreateItem()
    {
        int index = FindEmptySlot();
        if (index != -1)
        {
            ItemType type = (ItemType)Random.Range((int)ItemType.Ball, (int)ItemType.Max);
            int count = Random.Range(1, 100) < 30 ? 1 : Random.Range(1, 100); // 30퍼 확률로 1나옴
            var obj = Instantiate(m_itemPrefab);
            var item = obj.GetComponent<GameItem>();
            item.SetItem(type, m_iconSprites[(int)type], count);
            m_slotList[index].InitSlot(item);
        }


    }

    int FindEmptySlot()
    {
        for(int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsEmpty)
                return i;
        }
        return -1;
    }
    void CreateSlot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(m_slotPrefab);
            obj.transform.SetParent(m_grid.transform);
            obj.transform.localPosition = Vector3.zero; // 아이템 프리팹이 엉뚱한 좌표로 있으면 이상해지기 때문에 제로로 설정해놈.
            obj.transform.localRotation = Quaternion.identity;// 이건 생략 가능 회전 되어있을 경우도 생각한거임.
            obj.transform.localScale = Vector3.one; // {1,1,1} 좌표
            //항상 좌표 설정 해줘야 함.
            var slot = obj.GetComponent<ItemSlot>();
            slot.SetSlot(this);//만드는 자신의 인벤토리 정보를 넘겨줌.
            m_slotList.Add(slot);
        }

    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < m_tweener.Length; i++)
        { 
        m_tweener[i].ResetToBeginning();
        m_tweener[i].PlayForward();
        }
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    // 인벤토리 창 활성화 비활성화.
    // Start is called before the first frame update
    void Start()
    {
        CreateSlot(BASE_SLOT_COUNT);
        m_cursor.gameObject.SetActive(false);
        m_tweener = gameObject.GetComponents<UITweener>();
        HideUI();
    }

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Period))
        {
            CreateSlot(6);
            m_grid.repositionNow = true;
            m_scrollView.ResetPosition();
        }
    }
}
