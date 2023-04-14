using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
    // : MoonBehaviour 를 상속한다는 의미 , MonoBehaviour도 클래스임.
{
    [SerializeField]
    AnimationCurve m_curve;
    [SerializeField]
    Inventory m_myInven; // 인벤토리 스크립트.
    [SerializeField]
    GameObject m_bulletPrefab;// 1. 총알 날리기 쉬운 방법
    [SerializeField]
    Transform m_firePos; // 1. 총알 날리기 쉬운 방법
    [SerializeField]
    Animator m_animator;// 지역인지 전역인지 m으로 표시함.
    [SerializeField]
    SpriteRenderer m_sprRenderer;
    [SerializeField]
    Rigidbody2D m_rigidbody;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float m_jumpPower = 4f;
    [SerializeField]
    Vector3 m_dir;
    bool m_isGrounded;
    bool m_isFall;
    int m_hp = 5;

    public void SetDamage()
    {
        m_hp--;
        if(m_hp <= 0)
        {
            Destroy(gameObject);
        }
        m_animator.SetTrigger("IsHit");
    }
    void CreateBullet()
    {
        var obj = Instantiate(m_bulletPrefab);// 1. 총알 날리기 쉬운방법
        var bullet = obj.GetComponent<BulletController>();
        bullet.SetBullet(m_firePos.position, transform.eulerAngles.y == 180.0f ? Vector3.right : Vector3.left);
        //                  총알 위치          y가 180도 돌면 right 아니면 left 이다.
    }
    void Move()
    {
        //Vector2 a = new Vector2(0f, 2f);
        //Vector2 b = new Vector2(5f, 3f); // 두 벡터가 존재
        // var dist = (b - a).magnitude; // a와b의 거리 두벡터간의 거리를 구할수 있다.
        // var dist = (b - a).sqrMagnitude;// 제곱근으로 구한 거리

      /*  if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }*/
      //=> 묶어버리기
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
      //
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_dir = Vector3.left;
           // transform.position += Vector3.left * speed * Time.deltaTime;
            m_animator.SetBool("IsMove", true);
            //Vector3.left = 왼쪽 방향을 의미함
            //m_sprRenderer.flipX = false; 이러면 캡슐 및 박스 콜로라이더 위치가 뭉개짐
            //transform.rotation = new Quaternion(x,y,z,w); or . Euler(0f, 0f, 0f // 일반 xyz축은 돌리다보면 축이 겹쳐지는데 그럼 그 축들이 같이 한축이됨
            // 그래서 xyzw 를 이용한다.
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //transform.eulerAngles = Vector3.zero; = of, 위와 동일한 코드
            //짐벌락 = x,y,z,w 축이 있는데 2개의 축이 겹치게 되어 회전이 안되는 현상.
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_dir = Vector3.right;
           // transform.position += Vector3.right * speed * Time.deltaTime;
            m_animator.SetBool("IsMove", true);
            //m_sprRenderer.flipX = true;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        // transform.positon += m_dir * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_rigidbody.velocity = Vector2.zero;
            m_rigidbody.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
            m_animator.SetInteger("JumpState", 1);
            
        }
        
        if( Input.GetKeyDown(KeyCode.I))//인벤토리창
        {
            if (!m_myInven.gameObject.activeSelf)
                m_myInven.ShowUI();
            else
                m_myInven.HideUI();
        }

       /* if( Input.GetKeyDown(KeyCode.Escape))
        {
            if (LoadScene.Instance != null)
            {
                LoadScene.Instance.LoadSceneAsync(LoadScene.SceneState.Title);
            }
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    } // 물리적용이 적용된 상태에서 충돌가능한 상황을 알려주는 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            m_isGrounded = true;
            m_animator.SetInteger("JumpState", 0);
            m_isFall = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      if(collision.CompareTag("Ground") && m_animator.GetInteger("JumpState") == 2)
        {
            m_isGrounded = true;
            m_animator.SetInteger("JumpState", 0);
            m_isFall = false;
        }
    }// istrigger가 체크된 상태에서의 물리적 상태가 계산되지 않은 상태에서 어딘가에 닿았을떄 알려주는 이벤트들

    private void FixedUpdate()
    {
        var stateInfo = m_animator.GetCurrentAnimatorStateInfo(0); // 현재 사용되고 있는 애니메이션 상태 정보를 가져옴.
        if (!stateInfo.IsName("Shoot"))//만약 슈팅 상태이면
            m_rigidbody.AddForce(m_dir * speed, ForceMode2D.Force);// 움직이지 못하게 중력을 가해준다.
        else
            m_rigidbody.velocity = Vector2.zero;
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
        // 1/60 마다 명령어가 처리된다는 뜻임
        // 그게 프레임임
    }
    // Start is called before the first frame update
    //첫번째 프레임이 업데이트되기전에 시작하는 함수.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();// 정리해노면 좋을거같다.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetBool("IsShoot", true);
        
            //obj.transform.position = m_fi
            /////
            // GameObject obj = new GameObject();
            //obj.name = "bullet";
            //var sprRenderer = obj.AddComponent<SpriteRenderer>();
            // sprRenderer.sprite = m_bulletSpr; => bulletSpr은 위에 전역번수로 설정해논다. 그래서 bullet 이미지 로드해논상태
            //sprRenderer.sortingOrder = 2;
            // obj.AddComponent<BulletController>();
            // obj.transform.eulerAngles = new Vector3(0,0,-90f);
            //이미지가 계속 생기므로 별로 좋지 않은 방법 매우좋지않음
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_animator.SetBool("IsShoot", false);
        }

        if (!m_isGrounded)
        {
            if(m_rigidbody.velocity.y < 0f)
            {
                Debug.Log("Fall");
                if (!m_isFall)
                {
                    m_animator.SetInteger("JumpState", 2);
                    m_isFall = true;
                }
            }
        }// 떨어지는 타이밍
    }
}
