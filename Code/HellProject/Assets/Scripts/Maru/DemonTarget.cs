using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTarget : MonoBehaviour
{
    [SerializeField]
    private float _resizeSpeed;
    private float _size;
    private float _resizeFactor;


    [SerializeField]private List<GameObject> mesh0;

    private void Start()
    {
        OnEnable();
    }
    void OnEnable()
    {
        _size = 1f;
        _resizeFactor = -0.02f;
        StartCoroutine(Resize());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Resize()
    {
        while (true)
        {
            _size += _resizeFactor;
            if (_size > 1 || _size < 0.75f)
            {
                if (_size > 1)
                {
                    _size = 1;
                    _resizeFactor = -0.02f;
                }
                else
                {
                    _size = 0.75f;
                    _resizeFactor = 0.02f;
                }
            }
            transform.localScale = new Vector3(_size, _size, _size);
            yield return new WaitForSeconds(_resizeSpeed);
        }
    }
}
