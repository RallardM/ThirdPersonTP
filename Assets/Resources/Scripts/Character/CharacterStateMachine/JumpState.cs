using Unity.Mathematics;
using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.7f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter State: Jump State");

        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_stateMachine.UpdateAnimation(this);
        m_stateMachine.AudioController.PlaySound(ESoundType.Jump);
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Jump state");
        m_stateMachine.AudioController.PlaySound(ESoundType.Land);
    }

    public override void OnFixedUpdate()
    {
        Vector3 newDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 vectOnFloorDollyDir = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
            vectOnFloorDollyDir.Normalize();
            newDirection += vectOnFloorDollyDir * m_stateMachine.AccelerationValue;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 vectOnFloorDollyDir = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
            vectOnFloorDollyDir.Normalize();
            newDirection -= vectOnFloorDollyDir * m_stateMachine.AccelerationValue;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 vectOnFloorTruckDir = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
            vectOnFloorTruckDir.Normalize();
            newDirection -= vectOnFloorTruckDir * m_stateMachine.AccelerationValue;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 vectOnFloorTruckDir = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
            vectOnFloorTruckDir.Normalize();
            newDirection += vectOnFloorTruckDir * m_stateMachine.AccelerationValue;
        }

        // Rotate the player's mesh toward the new input direction
        if (newDirection != Vector3.zero)
        {
            Quaternion meshRotation = Quaternion.LookRotation(newDirection, Vector3.up);
            float interpolationSpeed = 2.0f;
            // Source : https://forum.unity.com/threads/what-is-the-difference-of-quaternion-slerp-and-lerp.101179/
            m_stateMachine.RB.rotation = Quaternion.Slerp(m_stateMachine.RB.rotation, meshRotation, interpolationSpeed * Time.deltaTime);
        }

        // Limit the velocity of the player while jumping leaving the 'y' to the jump force
        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            Vector3 currentVelocity = m_stateMachine.RB.velocity;
            currentVelocity.x = m_stateMachine.RB.velocity.normalized.x * m_stateMachine.MaxVelocity;
            currentVelocity.z = m_stateMachine.RB.velocity.normalized.z * m_stateMachine.MaxVelocity;
            m_stateMachine.RB.velocity = currentVelocity;
        }

        //Debug.Log("Jump velocity : " + m_stateMachine.RB.velocity.x);
        // If the sides velocity is too small reduce the player direction
        //if (math.abs(m_stateMachine.RB.velocity) < MIN_JUMP_VELOCITY)
        //{
        //    Debug.Log("Slow jump velocity : " + m_stateMachine.RB.velocity.x);
        //    //float strength = 0.7f;
        //    newDirection = Vector3.Lerp(Vector3.zero, newDirection, math.abs(m_stateMachine.RB.velocity) * 0.5f);
        //    //Debug.Log("newDirection : " + newDirection);
        //}

        // Apply the new direction to the rigidbody
        m_stateMachine.RB.AddForce(newDirection, ForceMode.Acceleration);
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(IState currentState)
    {
        if (m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Can enter: Jump State");
            return true;
        }

        return false;
    }

    public override bool CanExit()
    {
        if (m_stateMachine.IsInContactWithFloor() && m_currentStateTimer <= 0.0f)
        {
            Debug.Log("Can exit: Jump State");
            return true;
        }

        return false;
    }
}
