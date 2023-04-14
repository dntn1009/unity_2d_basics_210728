using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController : MonoBehaviour
{
    //[SerializeField]
    //float m_duration;
    //float m_time;
    [SerializeField]
    Animator m_animator;
    public void SetVfx(Vector3 pos)
    {
        transform.position = pos;
     //   transform.position = pos;
     //   Invoke("RmoveVfx", m_duration); // RmoveVfx 함수를 m_duration 시간에 예약을 걸고 지워달라고 요청하는 함수
                                        // 한마디로 ~시간에 예약하고 에약한 시간에 그함수를 불러내는 역할을 한다.
    }

    void RemoveVfx()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetVfx(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        var stateinfo = m_animator.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.normalizedTime >= 1f)
        {
            RemoveVfx();
        }
    }

    // 지우는 방법 3가지
    //1. 끝나는 애나메이션에 Add Animation Event에 함수를 걸어서 지운다.
    //2. 시간을 정해놓고 Invoke 이용하여 지정해둔 m_duration 시간에 예약을 걸고 지워달라고 요청하는 RmoveVfx를 건다.
    //3. 애니메이션 정보인 GetCurrentAnimatorStateInfo를 불러와서 다 끝나면 지워달라느 요청을 보낸다.
}
