using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour

{
    [SerializeField]
    GameObject m_vfxExplosionPrefab;
    [SerializeField]
    Rigidbody2D m_rigidbody;
    [SerializeField]
    SpriteRenderer m_sprRenderer;
    [SerializeField]
    float m_lifeTime = 2f;
    float m_time;
    Vector3 m_dir = Vector3.left;
    [SerializeField]
    float m_speed = 8f;
    Vector3 m_PrevPos;
    public void SetBullet(Vector3 pos, Vector3 dir)
    {
        m_dir = dir;
        transform.position = pos;
        if (m_dir == Vector3.right)
            m_sprRenderer.flipY = true;
        else
            m_sprRenderer.flipY = false;
     //   m_rigidbody.AddForce(m_dir * m_speed, ForceMode2D.Impulse);

    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("wall") || collision.transform.CompareTag("Enemy"))
        {
            var obj = Instantiate(m_vfxExplosionPrefab);
            var effect = obj.GetComponent<VfxController>();
            effect.SetVfx(transform.position); // 위와 아래의 순서가 중요 잘못하면 위치가 지워짐
            Destroy(gameObject);
            if (collision.transform.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<PlayerController>();
                enemy.SetDamage();
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        if(m_time > m_lifeTime)
        {
            Destroy(gameObject);
            m_time = 0f;
        }
        m_PrevPos = transform.position;
        var moveValue = m_speed * Time.deltaTime;
        transform.position += m_dir * moveValue;
        var dir = transform.position - m_PrevPos;
        var hit = Physics2D.Raycast(m_PrevPos, dir.normalized, moveValue, 1 << LayerMask.NameToLayer("Background") | 1 << LayerMask.NameToLayer("Enemy"));
        if(hit.collider != null)
        {
            transform.position = hit.point;
        }
    }
}
