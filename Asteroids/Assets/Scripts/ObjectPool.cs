using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPref;

    [SerializeField]
    private int numOfObject = 10;

    [SerializeField]
    private GameObject parent;

    private List<GameObject> pool;
    private bool hasInit;

    public void Init()
    {
        if (spawnPref == null)
            Debug.LogError("'SpawnPref' has not been set.", this);

        if (gameObject.tag == "ufo")
        {
            parent = gameObject.transform.root.Find("UFOBulletHolder").gameObject;
        }

        pool = new List<GameObject>(numOfObject);
        for (int i = 0; i < numOfObject; i++)
            AddGameObject();

        hasInit = true;
    }

    public GameObject GetGameObject()
    {
        if (!hasInit)
        {
            Debug.LogError("Call 'Init' first", this);
            return null;
        }

        for(int i = 0; i < pool.Count; i++)
        {
            GameObject currentObj = pool[i];
            if(!currentObj.activeSelf)
            {
                currentObj.transform.Translate(Vector3.zero);
                currentObj.transform.rotation = Quaternion.identity;
                currentObj.SetActive(true);
                return currentObj;
            }
        }
        GameObject addCurrentObj = AddGameObject();
        addCurrentObj.SetActive(true);
        return addCurrentObj;
    }

    public void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReleaseAll()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            pool[i].SetActive(false);
        }
    }


    private GameObject AddGameObject()
    {
        GameObject current = Instantiate(spawnPref, Vector3.zero, Quaternion.identity) as GameObject;
        if (parent == null)
            current.transform.SetParent(transform);
        else
        {
            current.transform.SetParent(parent.transform, true);
        }
            

        current.SetActive(false);
        pool.Add(current);
        return current;
    }
}
