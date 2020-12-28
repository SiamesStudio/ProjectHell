using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] Vector2 scaleMult;
    [SerializeField] float scaleSpeed;
    [SerializeField] Mesh[] meshes;
    Material material;
    [HideInInspector] public Vector3 dir;
    float time = 0f;

    private void Awake()
    {
        //if (TryGetComponent(out Rigidbody _rb))
        //    _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        //else Debug.LogError("Smoke error: no Rigidbody found");
        transform.localScale *= Random.Range(scaleMult.x, scaleMult.y);
        material = GetComponent<MeshRenderer>().material;
        GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
        Vector3 _worldDir = transform.parent.TransformDirection(dir);
        transform.position = _worldDir * speed * time + transform.parent.position;
        transform.localScale -= scaleSpeed * Time.deltaTime * Vector3.one;
        //transform.localPosition = new Vector3(transform.position.x, y, transform.position.z); 
    }

    private IEnumerator FadeOut()
    {
        float _time = 0f;
        while(_time < lifeTime)
        {
            _time += Time.deltaTime;
            float _alphaTreshold = Mathf.Lerp(0, 1, _time / lifeTime);
            material.SetFloat("AlphaTreshold", _alphaTreshold);
            yield return null;
        }
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
