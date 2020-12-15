using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PointMovement : MonoBehaviour
{
    [SerializeField] protected bool smoothRotation;
    [HideInInspector] [SerializeField] protected float completeRotationTime = .5f; //Rotation of 180 degrees
    [HideInInspector] [SerializeField] protected float anglesToStop = 90;
    protected NavMeshAgent myAgent;
    protected Coroutine rotationCoroutine;

    protected void Awake()
    {
        if (!TryGetComponent(out myAgent))
            Debug.LogError("PointMovement error: NavMesh Agent not found in " + name);
    }

    /// <summary>
    /// Moves the player to mouse pointing position if possible
    /// </summary>
    public void Move(Vector3 _targetPos)
    {
        if (smoothRotation)
        {
            if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
            rotationCoroutine = StartCoroutine(FaceDestination(_targetPos));
        }
        else
        {
            Vector3 _targetDir = (_targetPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(_targetDir.x, 0, _targetDir.z));
            myAgent.SetDestination(_targetPos);
        }
    }

    /// <summary>
    /// Lerps from current rotation to the one facing the given target position
    /// Then, set agent destination to target
    /// </summary>
    /// <param name="_lookRotation"></param>
    /// <param name="_time"></param>
    /// <returns></returns>
    private IEnumerator FaceDestination(Vector3 _targetPosition)
    {
        Vector3 _currentDir = transform.forward;
        Vector3 _targetDir = (_targetPosition - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDir.x, 0, _targetDir.z));

        float _alpha = Vector3.Angle(_currentDir, _targetDir);
        myAgent.isStopped = true; // _alpha > anglesToStop;
        float _rotTime = completeRotationTime * _alpha * 0.0055555555f; // Divided by 180

        float _t = 0f;
        while(_t < _rotTime)
        {
            _t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, _t/ _rotTime);
            yield return null;
        }
        myAgent.isStopped = false;
        myAgent.SetDestination(_targetPosition);
    }

    #region EDITOR CUSTOMIZATION

    #if UNITY_EDITOR
        [CustomEditor(typeof(PointMovement), true)]
        public class RandomScript_Editor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector(); // for other non-HideInInspector fields

                PointMovement _script = (PointMovement)target;

                if (_script.smoothRotation) // if bool is true, show other fields
                {
                    _script.completeRotationTime = EditorGUILayout.FloatField("Complete Rotation Time", _script.completeRotationTime);
                    _script.anglesToStop = EditorGUILayout.FloatField("Angles To Stop", _script.anglesToStop);
                }
            }
        }
    #endif
    #endregion
}
