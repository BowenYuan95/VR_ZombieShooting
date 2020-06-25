
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject player;

    public GameObject[] portals;

    public int portaltimes = 0;

    CharacterController cc;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public void Start()
    {
        cc = player.GetComponent<CharacterController>();
        agent = player.GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "portal")
        {
            switch (portaltimes)
            {
                case 0:
                    player.transform.position = new Vector3(portals[1].transform.position.x + 1,
                        portals[1].transform.position.y,
                        portals[1].transform.position.z);
                    portaltimes++;

                    break;

                case 1:
                    player.transform.position = new Vector3(portals[3].transform.position.x + 1,
                        portals[3].transform.position.y,
                        portals[3].transform.position.z);
                    portaltimes++;
                    cc.enabled = true;

                    break;
            }
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        //Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    private void Update()
    {
        if (portaltimes == 1)
        {
            cc.enabled = false;
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }
}
