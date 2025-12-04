using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace dinos.FSM
{
    public class IdleState : BaseState
    {
        private Animator animator;
        private float timer;
        private MovementSM stateMachine;

        public IdleState(dinos.FSM.MovementSM stateMachine) : base("IdleState", stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0f;
            animator = stateMachine.GetComponent<Animator>();
            //animator.SetBool("isPatrolling", false);
            animator.SetInteger("state", 1);
        }

        public override void Update()
        {
            base.Update();
            timer += Time.deltaTime;
            if (timer > 5)
            {
                BaseState[] randomState =
{
                    stateMachine.idleState,
                    stateMachine.layingDownState,
                    stateMachine.patrollingState
                };
                stateMachine.ChangeState(randomState[Random.Range(0, randomState.Length)]);
            }
        }

        public override void Exit()
        {
            Debug.LogWarning("kommer in här");
            base.Exit();         
            
        }
    }
}
