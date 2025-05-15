using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float startWaitTime = 4f;
    [SerializeField] private float timeToRotate = 1f;
    [SerializeField] private float speedWalk = 1f;
    [SerializeField] private float speedRun = 3f;

    [SerializeField] private float viewRadius = 4f;
    [SerializeField] private float viewAngle = 90;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float lostSightTime = 3f;
    [SerializeField] private float timeSinceLastSeen = 0f;
    // [SerializeField] private UnityEngine.Vector3 viewPosition;
    // // [SerializeField] private float meshResulotion = 1f;
    // // [SerializeField] private int edgeIterations = 4;
    // // [SerializeField] private float edgeDistance = 0.5f;

    [SerializeField] private Transform[] waypoints;
    int m_CurrentWaypointIndex;

    UnityEngine.Vector3 playerLastPosition = UnityEngine.Vector3.zero;
    UnityEngine.Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    [SerializeField] bool m_PlayerInRange;
    [SerializeField] bool m_PlayerNear;
    [SerializeField] bool m_IsPatrol;
    [SerializeField] bool m_CaughtPlayer;
    
    void Start()
    {
        m_PlayerPosition = UnityEngine.Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.stoppingDistance = 0.2f;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView();

        if(!m_IsPatrol){
            Chasing();
            
        } else {
            Patrolling();
        }


        
    }

    private void Chasing(){
        m_PlayerNear = false;
        playerLastPosition = UnityEngine.Vector3.zero;

        if(!m_CaughtPlayer){
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }

        

        if(!m_PlayerInRange) {
            timeSinceLastSeen += Time.deltaTime;
        

            if(timeSinceLastSeen >= lostSightTime) {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
            }
        } else {
            timeSinceLastSeen = 0f;
        }


        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
            if(m_WaitTime <= 0 && !m_CaughtPlayer && UnityEngine.Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position ) >= 6f) {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            } else {
                if(UnityEngine.Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f) {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }

            }
        }

    }

    private void Patrolling(){
        if (m_PlayerNear){
            if(m_TimeToRotate <= 0 ){
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            } else {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        } else {
            m_PlayerNear = false;
            playerLastPosition = UnityEngine.Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_WaitTime <= 0)
                {
                    NextPoint();
                    Move( speedWalk);
                    m_WaitTime = startWaitTime;
                } else {
                    Stop();
                    m_WaitTime -= Time.deltaTime;

                }
            }
        }
    }

    void Move(float speed) {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop() {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0; 
    }

    public void NextPoint() {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }



    void CaughtPlayer() {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(UnityEngine.Vector3 player) {
        navMeshAgent.SetDestination(player);
        if(UnityEngine.Vector3.Distance(transform.position, player) <= 0.3)
        {
            if(m_WaitTime <= 0) {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            } else {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView() {
        m_PlayerInRange = false;
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        bool canSeePlayer = false;

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            UnityEngine.Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if(UnityEngine.Vector3.Angle(transform.forward, dirToPlayer) < viewAngle/2)
            {
                float dstToPlayer = UnityEngine.Vector3.Distance(transform.position, player.position);
                
                if(!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)){
                    canSeePlayer = true;
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                    m_PlayerPosition = player.transform.position;
                    timeSinceLastSeen = 0f;
                    break;

                } else {
                    m_PlayerInRange =false;
                }
            }
        
        }

        if(!canSeePlayer) {
            timeSinceLastSeen += Time.deltaTime;
        }

        // if(m_PlayerInRange)
        // {
        //     m_PlayerPosition = player.transform.position;
        // }
    }

    
}
