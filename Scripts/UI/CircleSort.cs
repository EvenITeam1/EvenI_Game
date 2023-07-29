using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using DG.Tweening;

public class CircleSort : MonoBehaviour
{
    [SerializeField] List<GameObject> dogs;
    [SerializeField] Transform center;
    [SerializeField] float rotateDegree;
    [SerializeField] float duration;
    float deg;
    [SerializeField] float radius;
    private void Awake()
    {
        SortCircle();
    }

    private void Start()
    {
        deg = 0;
        transform.rotation = Quaternion.identity;
    }
    private void Update()
    {
        DogFixed();
    }
    void SortCircle()
    {
        var Rad = Mathf.Deg2Rad * 36;
        for (int i = 1; i <= 10; i++)
        {
            dogs[i - 1].transform.position = center.transform.position + new Vector3(radius * Mathf.Sin(Rad * i), 0, radius * Mathf.Cos(Rad * i));
        }
    }

    void DogFixed()
    {
        for (int i = 0; i < 10; i++)
        {
            dogs[i].transform.forward = Vector3.forward;
        }
    }
    public void rightButton()
    {
        deg += rotateDegree;
        transform.DORotate(new Vector3(0, deg, 0), duration);
    }

    public void leftButton()
    {
        deg -= rotateDegree;
        transform.DORotate(new Vector3(0, deg, 0), duration);
    }

   
}
