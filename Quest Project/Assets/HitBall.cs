using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball")
        {
            Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log($"{rigid.velocity} : 현재 속도");
            
            animator.SetTrigger("hit");
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
