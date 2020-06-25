using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]

public class MoveAI : MonoBehaviour
{
    public Transform transform;
    Animator anim;
    NavMeshAgent agent;
    public Transform player;
    float moveSpeed = 1.0f;
    float rotspeed = 0.5f;
    float timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(player.transform.position);
        //agent.speed = moveSpeed;
    }

    void RotateTo()
    {
        ////set the current angle
        //Vector3 oldAngle = transform.eulerAngles;
        ////get the angle facing the player 
        //transform.LookAt(player.transform);

        Vector3 targetdir = player.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdir, 
            rotspeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveTo()
    {

        float speed = moveSpeed * Time.deltaTime;

        agent.Move(transform.TransformDirection(new Vector3(0, 0, speed)));
    }

    // Update is called once per frame
    void Update()
    {


        //set the goal of navigation
        agent.SetDestination(player.position);

        //flash counter
        timer -= Time.deltaTime;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
 
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.idle")
            && !anim.IsInTransition(0)){

            anim.SetBool("idle", false);

            if (timer > 0)
                return;

            if (Vector3.Distance(transform.position, player.transform.position)<1.5f)
            {
                agent.ResetPath();
                anim.SetBool("attack", true);
            }
            else
            {
                timer = 1;

                agent.SetDestination(player.transform.position);

                anim.SetBool("walk", true);

            }
        }
        
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.walk")
            && !anim.IsInTransition(0))
        {
            anim.SetBool("walk", false);
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                agent.SetDestination(player.transform.position);

                timer = 1;
            }

            MoveTo();
            
            if (Vector3.Distance(transform.position, player.transform.position) < 1.5)
            {
                agent.ResetPath();
                anim.SetBool("attack", true);
            }
        }

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.attack")
            && !anim.IsInTransition(0))
        {
            RotateTo();
            anim.SetBool("attack", false);

            if (stateInfo.normalizedTime >= 1.0f)
            {
                anim.SetBool("idle", true);

                timer = 2;
            }
        }

        //this part of code has been moved to script (target);
        //if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.fallingback")
        //    && !anim.IsInTransition(0))
        //{
        //    timer = 5;
        //    if (timer < 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
