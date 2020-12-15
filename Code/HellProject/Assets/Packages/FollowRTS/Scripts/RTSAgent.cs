using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSAgent : PointMovement
{

    #region Variables

    private Vector3 targetLocalPos;
    private RTSZone myRTSZone;
    [SerializeField] private float lookAtSpeed = 2f;
    [SerializeField] private bool alwaysFaceTarget = false;
    private bool changingDirection;

    [SerializeField] private float minSpeedAllowed = .3f;
    [SerializeField] private float speedCheckTime = 1f;
    private float speedCheckCoolDown;
    private Vector3 pos0;
    private float speed;
    private bool isPositioned;

    #endregion

    private new void Awake()
    {
        base.Awake();
        speedCheckCoolDown = speedCheckTime;
    }

    private void Update()
    {
        if (!myRTSZone) return;
        isPositioned = myAgent.remainingDistance < .2f;

        speedCheckCoolDown -= Time.deltaTime;
        if(speedCheckCoolDown <= 0 && !isPositioned) CheckSpeed();

        Vector3 _targetPos = targetLocalPos + myRTSZone.GetRootPosition();
        //Move(_targetPos); Does not work, constant update of target Position!
        myAgent.SetDestination(_targetPos);

        if (alwaysFaceTarget) { FaceTarget(); return; }
        if(changingDirection)
        {
            FaceTarget();
            if (isPositioned) changingDirection = false;
        }
        if (isPositioned) FaceTarget();
    }

    private void CheckSpeed()
    {
        if(pos0 != null)
        {
            speed = Vector3.Magnitude(transform.position - pos0);
            Debug.Log(speed);
            if (speed < minSpeedAllowed) targetLocalPos = myRTSZone.GetRootPosition() - transform.position;
        }
        pos0 = transform.position;
        speedCheckCoolDown = speedCheckTime;
    }

    private void FaceTarget()
    {
        //transform.LookAt(myRTSZone.GetRealTarget()); return;
        Vector3 _currentDir = transform.forward;
        Vector3 _targetDir = (myRTSZone.GetRealTarget().position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_targetDir.x, 0, _targetDir.z));

        float _alpha = Vector3.Angle(_currentDir, _targetDir);
        float _rotSpeed = 180 / completeRotationTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * lookAtSpeed);
        //transform.rotation = _lookRotation;
    }

    public void SetTargetLocalPos(Vector3 _position, bool _changeOfDirection)
    {
        targetLocalPos = _position;
    }

    public void setRTSZone(RTSZone _zone)
    {
        myRTSZone = _zone;
    }
}
