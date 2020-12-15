using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] GameObject smokePrefab;
    [SerializeField] int numSmokes;
    [SerializeField] Transform platform;

    public void Play()
    {
        float _degrees = 360 / numSmokes;
        GameObject _parent = Instantiate(new GameObject(), transform.position, transform.rotation);
        _parent.transform.parent = platform;

        for(float i = 0; i < 360; i += _degrees)
        {
            float _radians = Mathf.Deg2Rad * i;
            Vector3 _dir = new Vector3(Mathf.Sin(_radians), 0, Mathf.Cos(_radians));
            Quaternion _quaternion = Quaternion.LookRotation(_dir, transform.up);
            GameObject _smoke = Instantiate(smokePrefab, transform.position, _quaternion);
            _smoke.transform.SetParent(_parent.transform);
            _smoke.GetComponent<Smoke>().dir = _dir;
        }
    }
}
