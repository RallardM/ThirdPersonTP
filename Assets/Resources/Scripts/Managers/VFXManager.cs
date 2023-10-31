using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_backgroundHits;

    [SerializeField]
    private List<GameObject> m_3DTextHits;

    private static VFXManager s_instance;

    public static VFXManager GetInstance()
    {
        if (s_instance == null)
        {
            return new VFXManager();
        }

        return s_instance;
    }

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            //DontDestroyOnLoad(this); // Is already in the DontDestroyOnLoad in the Character assets prefab
        }
        else if (s_instance != this)
        {
            Debug.LogWarning("VFXManager : Awake() - Attempted to create a second instance of the GameManagerSM singleton!");
            Destroy(this);
        }
    }

    public void InstantiateVFX(EVFX_Type vfxType, Vector3 pos)
    {
        int randomIndex = 0;
        switch (vfxType)
        {
            case EVFX_Type.Hit:
                randomIndex = Random.Range(0, m_3DTextHits.Count);
                Instantiate(m_backgroundHits[randomIndex], pos, Quaternion.identity, transform);
                Instantiate(m_3DTextHits[randomIndex], pos, Quaternion.identity, transform);
                break;

            default:
                break;
        }
    }
}

public enum EVFX_Type
{
    Hit,
    count
}