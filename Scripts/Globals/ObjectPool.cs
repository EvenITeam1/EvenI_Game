using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������ƮǮ ����
//1. ������ƮǮ �������� ���� �ֱ�
//2. Ǯ���ϰ� ���� ������Ʈ�� �ν����ͻ��� Pooling Objects ����Ʈ�� �߰�
//3. �׷��� ���� �����Ҷ� �˾Ƽ� Pooling Amount��ŭ Ǯ�� �߰��ϰ� �����մϴ�.
//4. Ǯ���� �ʿ��� ��쿡�� �̱������� �ʿ��� �Լ� ����Ͻø� �˴ϴ�.
//���� : Ǯ�� ���� ������Ʈ Ǯ���õ��ϸ� �˾Ƽ� ���� �ֱ�� �ϴµ� �����ϸ� Ǯ�� �ְ� Ǯ���õ����ּ���.


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] List<GameObject> _poolingObjects;

    Dictionary<string, Queue<GameObject>> _pooledObjects = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] int _poolingAmount;

    [SerializeField] public Transform _parent;

    private void Start()
    {
        InitialPooling();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// �� �ε��� ���� 50���� Ǯ���ϱ�
    /// </summary>
    private void InitialPooling()
    {
        _pooledObjects = new Dictionary<string, Queue<GameObject>>();
        for (int i = 0; i < _poolingObjects.Count; i++)
        {
            if (!_pooledObjects.ContainsKey(_poolingObjects[i].name) || _pooledObjects[_poolingObjects[i].name].Count == 0)
            {
                Queue<GameObject> newQueue = new Queue<GameObject>();
                _pooledObjects.Add(_poolingObjects[i].name, newQueue);
                for (int j = 0; j < _poolingAmount; j++)
                {
                    GameObject newObject = Instantiate(_poolingObjects[i], transform);
                    newObject.transform.SetParent(_parent);
                    int index = newObject.name.IndexOf("(Clone)");
                    if (index > 0)
                        newObject.name = newObject.name.Substring(0, index);
                    newObject.SetActive(false);
                    _pooledObjects[_poolingObjects[i].name].Enqueue(newObject);
                }
            }
        }
    }
    /// <summary>
    /// ������Ʈ�� ��ųʸ��� �ִ��� Ȯ�� �� ������ Ǯ���� ���ų� �����ؼ� return(active false ����), ������ Ǯ���ϰ� return �� ��� �α� ���, ������ ���°� Ǯ�� �䱸���� �ʱ�!!!
    /// </summary>
    public GameObject GetObject(GameObject getObject)
    {
        GameObject returnObject;
        if (_pooledObjects.ContainsKey(getObject.name))
        {
            if (_pooledObjects[getObject.name].Count != 0)
            {
                returnObject = _pooledObjects[getObject.name].Dequeue();
            }
            else
            {
                returnObject = Instantiate(getObject);
                returnObject.SetActive(false);
            }
            //returnObject.transform.SetParent(null);
            return returnObject;
        }
        else
        {
            //Debug.LogWarning(getObject + "��(��) ������ƮǮ�� ��� �߰� �� Ǯ���մϴ�");
            returnObject = AddPool(getObject);
            //returnObject.transform.SetParent(null);
            return returnObject;
        }
    }

    /// <summary>
    /// ������Ʈ ��ųʸ��� �ִ��� Ȯ�� �� ������ Ǯ�� ����������, ������ ��ųʸ��� Ǯ �߰� �� ����������, ������ ��� �α� ���
    /// </summary>
    public void ReturnObject(GameObject returnObject)
    {
        if (_pooledObjects.ContainsKey(returnObject.name))//������ ������Ʈ�� dictionary�� ������
        {
            returnObject.SetActive(false);
            _pooledObjects[returnObject.name].Enqueue(returnObject);//�����ֱ�
        }
        else//������(�̷����ɼ� ���� ����)
        {
            //Debug.LogWarning(returnObject + "��(��) ������ƮǮ�� ��� �߰� �� Ǯ���մϴ�");
            returnObject.SetActive(false);
            Queue<GameObject> newQueue = new Queue<GameObject>();
            _pooledObjects.Add(returnObject.name, newQueue);//dictionary�� �߰��ϰ�
            _pooledObjects[returnObject.name].Enqueue(returnObject);//�ֱ�
        }
    }

    /// <summary>
    /// �䱸�ϴ� ������Ʈ�� Ǯ�� ������ _poolingAmount ��ŭ �߰�
    /// </summary>
    private GameObject AddPool(GameObject addObject)
    {
        Queue<GameObject> newQueue = new Queue<GameObject>();
        _pooledObjects.Add(addObject.name, newQueue);
        for (int i = 0; i < _poolingAmount; i++)
        {
            GameObject newObject = Instantiate(addObject);
            int index = newObject.name.IndexOf("(Clone)");
            if (index > 0)
                newObject.name = newObject.name.Substring(0, index);
            //newObject.transform.SetParent(null);
            newObject.SetActive(false);
            _pooledObjects[addObject.name].Enqueue(newObject);
        }
        return _pooledObjects[addObject.name].Dequeue();
    }
}