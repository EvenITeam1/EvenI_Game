using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using DG.Tweening;

public class CircleSort : MonoBehaviour
{
    [SerializeField] List<GameObject> dogs;
    [SerializeField] Transform center;
    [SerializeField] float degree;
    [SerializeField] float radius;
    private void Start()
    {
        SortCircle();
    }
  
    private void Update()
    {
        Rotate();
    }
    void SortCircle()
    {
        var Rad = Mathf.Deg2Rad * degree;
        for (int i = 1; i <= 10; i++)
        {
            dogs[i - 1].transform.position = center.transform.position + new Vector3(radius * Mathf.Sin(Rad * i), 0, radius * Mathf.Cos(Rad * i));
        }
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 36);
        for (int i = 0; i < 10; i++)
        {
            dogs[i].transform.Rotate(-Vector3.up * Time.deltaTime * 36);
        }
    }
}
