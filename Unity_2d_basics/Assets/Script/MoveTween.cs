using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
   
    public AnimationCurve m_curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public float m_duration;
    public Vector3 m_from;
    public Vector3 m_to;
    float m_time;
    bool m_isStart;

    public void Play()
    {
        m_isStart = true;
        m_time = 0f;
    }

    public void Play(Vector3 from, Vector3 to, float duration)
    {
        m_from = from;
        m_to = to;
        m_duration = duration;
        Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isStart)
        {
            m_time += Time.deltaTime / m_duration;
            var value = m_curve.Evaluate(m_time);
            transform.position = m_from * (1f - value) + m_to * value;
            if(m_time > 1f)
            {
                m_isStart = false;
                m_time = 0f;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_from = transform.position;
            m_to = m_from + Vector3.left * 3f;
            m_duration = 1f;
            Play();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            m_from = transform.position;
            m_to = m_from + Vector3.right * 3f;
            m_duration = 1f;
            Play();
        }
    }
}
