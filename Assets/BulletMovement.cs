using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed = 1.0f;
    private SpriteRenderer m_renderer;
    public bool isVisible = true;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (!m_renderer.isVisible)
        {
            isVisible = false;
        }
    }

}
