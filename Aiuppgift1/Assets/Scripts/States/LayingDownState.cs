using UnityEngine;

namespace dinos.FSM
{
    public class LayingDownState : BaseState
    {
        Animator animator;
        MovementSM stateMachine;
        float timer;
        public LayingDownState(dinos.FSM.MovementSM stateMachine) : base("PatrollingState", stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0;
            animator = stateMachine.GetComponent<Animator>();
            animator.SetInteger("state", 3);
            animator.ResetTrigger("getUp");
            animator.SetTrigger("layDown");
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
                    stateMachine.patrollingState
                };
                stateMachine.pendingNextState = randomState[Random.Range(0, randomState.Length)];
                animator.SetInteger("state", stateMachine.pendingNextState == stateMachine.patrollingState ? 2 : 1);
                animator.SetTrigger("getUp");
                timer = 0;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}
