using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        //Debug.Log("Enter State: Jump State");

        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnExit()
    {
        //Debug.Log("Exit state: Jump state");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter()
    {
        // This must be run in Update absolutely
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}