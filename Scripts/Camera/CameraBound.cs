using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public Camera camera_ = null;
    public GameObject mirrorL;
    public GameObject mirrorR;
    public GameObject mirrorU;

    private float size_y_;
    private float size_x_;

    public float Bottom
    {
        get
        {
            return size_y_ * -1 + camera_.gameObject.transform.position.y;
        }
    }

    public float Top
    {
        get
        {
            return size_y_ + camera_.gameObject.transform.position.y;
        }
    }

    public float Left
    {
        get
        {
            return size_x_ * -1 + camera_.gameObject.transform.position.x;
        }
    }

    public float Right
    {
        get
        {
            return size_x_ + camera_.gameObject.transform.position.x;
        }
    }

    public float Height
    {
        get
        {
            return size_y_ * 2;
        }
    }

    public float Width
    {
        get
        {
            return size_x_ * 2;
        }
    }

    void Start()
    {
        size_y_ = camera_.orthographicSize;
        size_x_ = camera_.orthographicSize * Screen.width / Screen.height;

        mirrorL.transform.position = new Vector3(Left, 0, 0);
        mirrorR.transform.position = new Vector3(Right, 0, 0);
        mirrorU.transform.position = new Vector3(0, Top, 0);
    }
}
