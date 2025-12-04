using UnityEngine;
namespace dinos.FSM
{
    public class StateMachine : MonoBehaviour
    {
        BaseState currentState;

        void Start()
        {
            currentState = GetInitialState();
            if(currentState !=null)
            {
                currentState.Enter();
            }
        }


        void Update()
        {
            if(currentState != null)
            {
                currentState.Update();
            }
        }

        public void ChangeState(BaseState newState)
        {
            if (newState == null || newState == currentState) return;

            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }

        private void OnGUI()
        {
            string content = currentState != null ? currentState.name : "(no current state)";
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }
    }
}
