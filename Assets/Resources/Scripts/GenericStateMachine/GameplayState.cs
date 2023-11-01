using Cinemachine;
using UnityEngine;

public class GameplayState : IState
{
    protected Camera m_camera;

    public GameplayState(Camera camera)
    {
        m_camera = camera;
    }

    public bool CanEnter(IState currentState)
    {
        Debug.Log("CanEnter GameplayState " + (GameManagerSM.GetInstance().DesiredState == this));
        return GameManagerSM.GetInstance().DesiredState == this;
        //Debug.Log("CanEnter GameplayState " + (GameManagerSM.GetInstance().DesiredState == currentState));
        //return GameManagerSM.GetInstance().DesiredState == currentState;
    }

    public bool CanExit()
    {
        return GameManagerSM.GetInstance().DesiredState != this;
    }

    public void OnEnter()
    {
        Debug.Log("On Enter GameplayState");
        //m_camera.enabled = true;
        CinemachineBrain brain = CinemachineCore.Instance.GetActiveBrain(0);

        if (brain != null)
        {
            brain.ActiveVirtualCamera?.VirtualCameraGameObject.SetActive(false);
        }

        m_camera.gameObject.SetActive(true);
        GameManagerSM.GetInstance().DesiredState = null;
        GameManagerSM.GetInstance().CharacterControllerStateMachine.OnGameManagerStateChange(false);
    }

    public void OnExit()
    {
        Debug.Log("On Exit GameplayState");
        //m_camera.enabled = false;
    }

    public void OnFixedUpdate()
    {
        //Debug.Log("GameplayState : OnFixedUpdate() : Enters OnFixedUpdate()");
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Bullet time activated");
            // Activate the bullet time
            GameManagerSM.GetInstance().ActivateBulletTime();
        }
    }

    public void OnStart()
    {
        //Debug.Log("GameplayState OnStart()"); // TODO: Remove after debugging
    }

    public void OnUpdate()
    {
    }
}