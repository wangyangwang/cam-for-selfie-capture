using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffMaker : MonoBehaviour
{


    public int n = 100;
    public int r = 500;

    public List<GameObject> things = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject newGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            things.Add(newGO);
            newGO.transform.parent = this.transform;
            newGO.transform.localScale = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
            newGO.transform.position = new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r));
        }
    }

    public GameObject GetOne()
    {
        if (things.Count == 0) return null;
        return things[Random.Range(0, things.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
