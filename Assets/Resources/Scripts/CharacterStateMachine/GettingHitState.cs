using UnityEngine;

public class GettingHitState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        //Debug.Log("Enter State: Getting Hit state");
        m_stateMachine.UpdateHeatlh();
        m_stateMachine.UpdateHitAnimation();
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnExit()
    {
        //Debug.Log("Exit state: Getting Hit state");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(CharacterState currentState)
    {
        //CharacterState freeState = currentState as FreeState;

        //if (freeState != null)
        //{
            return m_stateMachine.Health < m_stateMachine.PreviousHealth;
        //}

        //return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f && m_stateMachine.Health == m_stateMachine.PreviousHealth;
    }
}
