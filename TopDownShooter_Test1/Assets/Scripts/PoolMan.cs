using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Mr. poolman ready for duty
 **/
public class PoolMan
{
    private Dictionary<int, Queue<PoolObject>> poolLib = new Dictionary<int, Queue<PoolObject>>();
    private Transform transform;
    private static PoolMan _instance;
    public static PoolMan Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PoolMan();
            }
            return _instance;
        }
    }

    private PoolMan()
    {
        transform = new GameObject("PoolManager").transform;
    }
    
    public void CreatePool(GameObject prefab, int size)
    {
        int key = prefab.GetInstanceID();

        if (!poolLib.ContainsKey(key))
        {
            poolLib.Add(key, new Queue<PoolObject>()); //Tilføjer en ny kø (Queue) med objektets id som key

            //Instantierer et nyt GameObect som kan holde på vores pool objekter
            GameObject pool = new GameObject(prefab.name + " pool");
            pool.transform.SetParent(transform);

            for(int i = 0; i < size; i++)
            {
                //Her instantieres vore prefab som GameObject og bruges som parameter til at instantiere et PoolObject
                PoolObject obj = new PoolObject(GameObject.Instantiate(prefab));
                //Dernæst settes vores pool GameObject som parent til dette
                obj.SetParent(pool.transform);
                //Og til sidst tilføjer vi det til dets tilhørende kø
                poolLib[key].Enqueue(obj);
            }

        }
    }

    public PoolObject ReuseObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        int key = prefab.GetInstanceID();

        if (poolLib.ContainsKey(key))
        {
            //Flytter objekter fra forest til bagerst i køen
            PoolObject obj = poolLib[key].Dequeue();
            poolLib[key].Enqueue(obj);

            obj.Reuse(pos, rot);

            return obj;
        }

        return null;
    }

    public void Reset()
    {
        poolLib = new Dictionary<int, Queue<PoolObject>>();
        transform = null;
    }

    public class PoolObject
    {
        private GameObject gameObject;
        public GameObject GameObject
        {
            get { return gameObject; }
        }
        private Transform transform;
        private bool hasInteface;
        private IPoolable _interface;
        public IPoolable Interface
        {
            get { return _interface; }
        }

        public PoolObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
            transform = gameObject.transform;

            _interface = gameObject.GetComponent<IPoolable>();
            if (_interface != null){
                hasInteface = true;
                _interface.OnCreate();
            }

            gameObject.SetActive(false);
        }

        public void Reuse(Vector3 pos, Quaternion rot)
        {
            gameObject.SetActive(true);
            transform.position = pos;
            transform.rotation = rot;

            if (hasInteface) _interface.OnReuse();
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}
