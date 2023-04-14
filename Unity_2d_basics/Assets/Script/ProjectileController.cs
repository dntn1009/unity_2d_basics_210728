using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigidbody;
    [SerializeField]
    SpringJoint2D m_springJoint;
    Transform m_catapultTransform;
    bool m_isDrag;
    float m_maxDist = 4f;
    float m_sqrMaxDist;
    private void OnMouseDown()
    {
        m_isDrag = true;
        //m_springJoint.enabled = false;
    }
    private void OnMouseUp()
    {
        m_isDrag = false;
        m_rigidbody.isKinematic = false;
    }
    void DragProjectile()
    {
        if (!m_isDrag)
            return;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        var dir = worldPos - m_catapultTransform.position;

        if (dir.sqrMagnitude > m_sqrMaxDist)
            transform.position = m_catapultTransform.position + dir.normalized * m_maxDist;
        else
        transform.position = m_catapultTransform.position + dir;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody.isKinematic = true;
        m_catapultTransform = m_springJoint.connectedBody.transform;
        m_sqrMaxDist = Mathf.Pow(m_maxDist, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        DragProjectile();
    }
}
