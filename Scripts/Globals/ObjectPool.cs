using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//오브젝트풀 사용법
//1. 오브젝트풀 프리팹을 씬에 넣기
//2. 풀링하고 싶은 오브젝트를 인스펙터상의 Pooling Objects 리스트에 추가
//3. 그러면 게임 시작할때 알아서 Pooling Amount만큼 풀에 추가하고 시작합니다.
//4. 풀링이 필요한 경우에는 싱글톤으로 필요한 함수 사용하시면 됩니다.
//주의 : 풀에 없는 오브젝트 풀링시도하면 알아서 만들어서 주기는 하는데 웬만하면 풀에 넣고 풀링시도해주세요.


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] List<GameObject> _poolingObjects;

    Dictionary<string, Queue<GameObject>> _pooledObjects = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] int _poolingAmount;

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
    /// 씬 로딩시 각각 50개씩 풀링하기
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
                    GameObject newObject = Instantiate(_poolingObjects[i]);
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
    /// 오브젝트가 딕셔너리에 있는지 확인 후 있으면 풀에서 빼거나 생성해서 return(active false 상태), 없으면 풀링하고 return 후 경고 로그 출력, 가급적 없는거 풀링 요구하지 않기!!!
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
<<<<<<< HEAD
            returnObject.transform.parent = null;
=======
            returnObject.transform.SetParent(null);
>>>>>>> develop2
            return returnObject;
        }
        else
        {
            Debug.LogWarning(getObject + "이(가) 오브젝트풀에 없어서 추가 후 풀링합니다");
            returnObject = AddPool(getObject);
<<<<<<< HEAD
            returnObject.transform.parent = null;
=======
            returnObject.transform.SetParent(null);
>>>>>>> develop2
            return returnObject;
        }
    }

    /// <summary>
    /// 오브젝트 딕셔너리에 있는지 확인 후 있으면 풀로 돌려보내기, 없으면 딕셔너리에 풀 추가 후 돌려보내기, 없으면 경고 로그 출력
    /// </summary>
    public void ReturnObject(GameObject returnObject)
    {
        if (_pooledObjects.ContainsKey(returnObject.name))//돌려줄 오브젝트가 dictionary에 있으면
        {
            returnObject.SetActive(false);
<<<<<<< HEAD
            returnObject.transform.parent = null;
=======
            returnObject.transform.SetParent(null);
>>>>>>> develop2
            _pooledObjects[returnObject.name].Enqueue(returnObject);//돌려주기
        }
        else//없으면(이럴가능성 거의 없음)
        {
            Debug.LogWarning(returnObject + "이(가) 오브젝트풀에 없어서 추가 후 풀링합니다");
            returnObject.SetActive(false);
            Queue<GameObject> newQueue = new Queue<GameObject>();
            _pooledObjects.Add(returnObject.name, newQueue);//dictionary에 추가하고
            _pooledObjects[returnObject.name].Enqueue(returnObject);//넣기
        }
    }

    /// <summary>
    /// 요구하는 오브젝트가 풀에 없을때 _poolingAmount 만큼 추가
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
<<<<<<< HEAD
            newObject.transform.parent = null;
=======
            newObject.transform.SetParent(null);
>>>>>>> develop2
            newObject.SetActive(false);
            _pooledObjects[addObject.name].Enqueue(newObject);
        }
        return _pooledObjects[addObject.name].Dequeue();
    }
}