using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public float distance = 3f;
    private Animator animator;
    private bool isInMovement = true;
    private CharacterController cc;
    private float currentDistance = 0f;
    private float currentDir = 0f;
    public float length;
    public Text countext, foodtext;
    private float points = 0;
    private float worldTime = 1f;
    public List<Rigidbody> ragdollElements;
    public bool paused = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        StartCoroutine(WaitWorld());        
    }        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                paused = false;
            else
                paused = true;
        }
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = worldTime;

        if (isInMovement)
        {
            StartCoroutine(move());
        }

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

        bool dirs = Input.GetButtonDown("Jump");

        if (dirs)
        {
            animator.SetTrigger("Jump");
            cc.Move(Vector3.up * Time.deltaTime);
        }

        timeAnim();
        countext.text = "" + points;        
    }
    void FixedUpdate()
    {
        points = points + 1;
        Time.timeScale = worldTime;
    }
    IEnumerator WaitWorld()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            worldTime = worldTime + 0.1f;
        }
    }
    private void timeAnim()
    {
        AnimatorClipInfo[] stInfos = animator.GetCurrentAnimatorClipInfo(0);

        if (stInfos == null);
        {
            AnimationClip clip = stInfos[0].clip;
            length = clip.length;
        }
    }
    IEnumerator move()
    {
        if (currentDistance <= 0)
        {
            yield return new WaitForEndOfFrame();
            isInMovement = false;
        }

        float speed = distance / length;
        float tmpDist = Time.deltaTime * speed;

        cc.Move(Vector3.right * currentDir * tmpDist);
        currentDistance -= tmpDist;
    }
    IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            worldTime = worldTime - 0.5f;
            if (worldTime <= 1)
            {
                Time.timeScale = 0;
            }
        }        
    }
    IEnumerator WaitFood(Collider other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.gameObject);

        yield return new WaitForSeconds(2f);
        foodtext.text = "";
    }
    IEnumerator WaitText()
    {
        yield return new WaitForSeconds(3f);
        foodtext.text = "";
    }
    IEnumerator WaitHot(Collider other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.gameObject);

        yield return new WaitForSeconds(10f);
        worldTime = worldTime - 0.5f;
    }
    IEnumerator WaitDrink(Collider other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.gameObject);

        yield return new WaitForSeconds(0.5f);
        worldTime = worldTime - 0.5f;

        yield return new WaitForSeconds(5f);
        worldTime = worldTime + 0.5f;
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Food"))
        {
            animator.SetTrigger("Attack");
            StartCoroutine(WaitFood(other));
            points = points + 100;
            foodtext.text = "+ 100";
            StartCoroutine(WaitText());
            worldTime = worldTime - 0.1f;
        }

        if (other.CompareTag("Dangers"))
        {
            for (int i = 0; i < ragdollElements.Count; i++)
            {
                ragdollElements[i].isKinematic = false;
            }

            animator.enabled = false;
            //animator.SetTrigger("Death");
            StartCoroutine(Wait());
            foodtext.text = "DEATH";
            StartCoroutine(WaitText());
        }

        if (other.CompareTag("Pepper"))
        {
            animator.SetTrigger("Attack");
            worldTime = worldTime + 0.5f;
            foodtext.text = "HOT";
            StartCoroutine(WaitText());
            StartCoroutine(WaitHot(other));
        }

        if (other.CompareTag("Wine"))
        {
            animator.SetTrigger("Attack");
            foodtext.text = "DRUNK";
            StartCoroutine(WaitText());
            StartCoroutine(WaitDrink(other));
        }
    }
}
