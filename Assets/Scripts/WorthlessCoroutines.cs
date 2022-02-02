using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorthlessCoroutines : MonoBehaviour
{
    public float distance = 3f;
    private Animator animator;
    private bool isInMovement = false;
    private CharacterController cc;
    private float currentDistance = 0f;
    private float currentDir = 0f;
    public float length;
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        if (isInMovement == false && dir != 0)
        {
            isInMovement = true;
            currentDir = dir;
            currentDistance = distance;
            if (dir > 0)
                animator.SetTrigger("Right");
            if (dir < 0)
                animator.SetTrigger("Left");
        }
        if (isInMovement)
        {
            move();
        }
        bool dirs = Input.GetButtonDown("Jump");
        if (dirs)
        {
            animator.SetTrigger("Jump");
            cc.Move(Vector3.up * Time.deltaTime);
        }
        timeAnim();
    }
    private void timeAnim()
    {
        AnimatorClipInfo[] stInfos = animator.GetCurrentAnimatorClipInfo(0);
        if (stInfos == null) ;
        {
            AnimationClip clip = stInfos[0].clip;
            length = clip.length;
        }
    }
    private void move()
    {
        if (currentDistance <= 0)
        {
            isInMovement = false;
            return;
        }
        float speed = distance / length;
        float tmpDist = Time.deltaTime * speed;
        cc.Move(Vector3.right * currentDir * tmpDist);
        currentDistance -= tmpDist;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dangers"))
        {
            animator.SetTrigger("Death");
            StartCoroutine(Wait());
        }
    }
}