using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimation : MonoBehaviour
{
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private float _animationLength;
    private float _animationCurrentFrame;
    //innerRing
    private int _visible;

    //outerRing
    private float _size;
    private float _resizeFactor;



    GameObject innerRing;
    GameObject outerRing;


    private void Start()
    {
        innerRing = transform.GetChild(0).gameObject;
        outerRing = transform.GetChild(1).gameObject;

        Play();
    }

    public void Play()
    {
        _visible = 0;
        _animationCurrentFrame = 0;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        while (_animationCurrentFrame < _animationLength)
        {

            _visible++;
            if(_visible < 10 && _visible % 2 == 0 )
            {
                innerRing.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (_visible < 20 && _visible % 4 == 0)
            {
                innerRing.GetComponent<MeshRenderer>().enabled = false;
            }
            
            else
            {
                innerRing.GetComponent<MeshRenderer>().enabled = true;
            }


            //outerRing
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
            outerRing.transform.localScale = new Vector3(_size, _size, _size);



            _animationCurrentFrame++;

            yield return new WaitForSeconds(_animationSpeed);
        }

        transform.position = new Vector3(0, -100, 0);

        yield return null;
    }
}
