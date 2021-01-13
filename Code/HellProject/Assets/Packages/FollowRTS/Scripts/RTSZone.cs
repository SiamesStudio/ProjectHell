using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSZone : MonoBehaviour
{
    #region Variables

    [Header("General Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform realTarget;
    [SerializeField] private float speed;
    public bool following = true;
    private Vector3 localPos;

    [Header("Agent Settings")]
    [SerializeField] private RTSAgent agentPrefab;
    private List<RTSAgent> myAgents;
    private List<Vector3> agentsPositions;
    [SerializeField] private int numAgents;

    [Header("Agent Randomize Settings")]
    [SerializeField] [Range(0, 180)] private float anglesToDisorder = 145f;
    [SerializeField] private float directionCheckTime = .25f;
    private float directionCheckCooldown;
    private Vector3 dir0; //Last frame target forward

    [Header("Agent Positioning Settings")]
    [SerializeField] private float positioningRadius;
    private Vector3 agentRootPosition = Vector3.zero;
    [SerializeField] private List<Transform> defaultPositions; //In case we want default positioning

    #endregion

    void Awake()
    {
        if (!target) Debug.LogError("RTSZone error: No target found in " + name);
        if (!agentPrefab) Debug.LogError("RTSZone error: No agent prefab found in " + name);
        directionCheckCooldown = directionCheckTime;
        localPos = target.position - transform.position;
        agentsPositions = new List<Vector3>();
        GeneratePositions();
        GenerateAgents();
    }

    void Update()
    {
        //Check distance to target
        float _distance = Vector3.Distance(realTarget.transform.position, transform.position);
        LevelManager.instance.isQuestionVisible = _distance < transform.localScale.x*.5f;

        if (Input.GetKeyDown(KeyCode.Space)) ChangeState();
        if (following)
        {
            transform.position = Vector3.Lerp(transform.position, target.position - localPos, speed*Time.deltaTime);
            directionCheckCooldown -= Time.deltaTime;
            if(directionCheckCooldown < 0f) CheckDirection();
        }
    }

    #region Getters

    public Transform GetRealTarget()
    {
        return realTarget;
    }

    public Vector3 GetRootPosition()
    {
        return agentRootPosition + transform.position;
    }

    #endregion

    #region Change State

    /// <summary>
    /// Checks if direction has changed more than expected and provokes diorder if so
    /// </summary>
    private void CheckDirection()
    {
        if(dir0 != null)
        {
            float _alpha = Vector3.Angle(target.forward, dir0);
            if (_alpha > anglesToDisorder)
            {
                SetPositions(true);
            }
        }
        dir0 = target.forward;
        directionCheckCooldown = directionCheckTime;
    }


    /// <summary>
    /// Switches between following and not following the target
    /// </summary>
    public void ChangeState()
    {
        following = !following;
        GetComponent<AudioSource>().Play();
        if (following) SetPositions(false);
    }

    #endregion

    #region Generators

    /// <summary>
    /// Generates RTS agents
    /// </summary>
    private void GenerateAgents()
    {
        myAgents = new List<RTSAgent>();
        for (int i = 0; i < numAgents; i++)
        {
            myAgents.Add(Instantiate(agentPrefab, transform.position + agentsPositions[i], transform.rotation, null));
            //myAgents.Add(Instantiate(agentPrefab, defaultPositions[i].position, defaultPositions[i].rotation, null));
            myAgents[i].SetTargetLocalPos(agentsPositions[i], false);
            myAgents[i].setRTSZone(this);
        }
    }

    /// <summary>
    /// Sets agents local positions
    /// </summary>
    private void SetPositions(bool _changeOfDirection)
    {
        GeneratePositions();
        ShufflePositions();
        for(int i = 0; i < numAgents; i++)
        {
            myAgents[i].SetTargetLocalPos(agentsPositions[i], _changeOfDirection);
        }
    }

    /// <summary>
    /// Generates random positions and fills positions list
    /// </summary>
    private void GeneratePositions()
    {
        agentsPositions.Clear();
        agentRootPosition = GenerateCirclePos(transform.localScale.x * .5f - positioningRadius);
        for (int i = 0; i < numAgents; i++)
        {
            if (defaultPositions.Count == 0) 
                agentsPositions.Add(GenerateCirclePos(positioningRadius));
            else agentsPositions.Add(defaultPositions[i].position - transform.position);
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Shuffles agentsPositions list
    /// </summary>
    private void ShufflePositions()
    {
        for (int i = 0; i < numAgents; i++)
        {
            Vector3 temp = agentsPositions[i];
            int randomIndex = Random.Range(i, agentsPositions.Count);
            agentsPositions[i] = agentsPositions[randomIndex];
            agentsPositions[randomIndex] = temp;
        }
    }

    /// <summary>
    /// Generates a local position in a circle given the radius
    /// </summary>
    /// <param name="_radius"></param>
    /// <returns></returns>
    private Vector3 GenerateCirclePos(float _radius)
    {
        Vector3 _pos;
        float _alpha = Random.Range(0, 360);
        _pos = new Vector3(Mathf.Sin(_alpha), 0f, Mathf.Cos(_alpha));
        float _distance = Random.Range(0, _radius);
        _pos *= _distance;

        return _pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + agentRootPosition, positioningRadius);
    }

    #endregion
}
