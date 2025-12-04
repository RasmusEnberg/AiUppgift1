using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
namespace dinos.FSM
{
    public class PatrollingState : BaseState
    {
        Animator animator;
        float timer;
        List<Transform> wayPoints = new List<Transform>();
        NavMeshAgent agent;
        MovementSM stateMachine;
        public PatrollingState(dinos.FSM.MovementSM stateMachine) : base("PatrollingState",stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            agent = stateMachine.GetComponent<NavMeshAgent>();
            animator = stateMachine.GetComponent<Animator>();
            timer = 0;
            GameObject go = GameObject.FindGameObjectWithTag("Waypoints");
            wayPoints.Clear();
            foreach (Transform t in go.transform)
            {
                wayPoints.Add(t);
            }

            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            //animator.SetBool("isPatrolling", true);
            animator.SetInteger("state", 2);
        }

        public override void Update()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {                                                                                              
                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            }
            timer += Time.deltaTime;
            if (timer > 5)
            {
                BaseState[] randomState =
{
                    stateMachine.idleState,
                    stateMachine.patrollingState
                };
                stateMachine.ChangeState(randomState[Random.Range(0, randomState.Length)]);
            }
        }

        public override void Exit()
        {
            base.Exit();
            agent.SetDestination(agent.transform.position);
            //animator.SetBool("isPatrolling", false);
        }
    }
}
