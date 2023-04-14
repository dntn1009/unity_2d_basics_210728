using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    GameObject m_selectObj;
    GameObject GetSelectObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000f))//hit는 구조체이기 때문에 포인터가 없어서 out 에 담아서 보내주세요 라고 뜻하는거임
        {
            return hit.collider.gameObject;
           
        }
        return null;
      
    }
    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//0:왼쪽 1:오른쪽 2:가운데
        {
            m_selectObj = GetSelectObject();
            if(m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.back * 3f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.forward * 3f;
            }
        }
        if (hit.collider == null)
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
        else
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
    }
}
