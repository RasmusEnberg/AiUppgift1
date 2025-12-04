using UnityEngine;


namespace dinos.FSM
{
    public class MovementSM : StateMachine
    {
        public IdleState idleState;
        public PatrollingState patrollingState;
        public LayingDownState layingDownState;
        public BaseState pendingNextState;
        private void Awake()
        {
            idleState = new IdleState(this);
            patrollingState = new PatrollingState(this);
            layingDownState = new LayingDownState(this);
        }

        protected override BaseState GetInitialState()
        {
            return idleState;
        }
        public void OnGetUpFinished()
        {
            if(pendingNextState != null)
            {
                Debug.Log("GetUp finished");
                ChangeState(pendingNextState);
                pendingNextState = null;
            }
        }
    }
}
