using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour
{
    List<MeshFilter> meshFilters = new List<MeshFilter>();
    List<CombineInstance> combine = new List<CombineInstance>();
    MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter[] meshs = GetComponentsInChildren<MeshFilter>();
        meshFilters.AddRange(meshs);
        Debug.Log(meshFilters.Count);
        int i = 0;
        while (i < meshFilters.Count)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = meshFilters[i].mesh;
            ci.transform = meshFilters[i].transform.localToWorldMatrix;
            combine.Add(ci);
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, -90.0f, 0);
        meshFilter = transform.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine.ToArray());
        transform.gameObject.SetActive(true); 
        transform.gameObject.AddComponent<BoxCollider>();       
    }

    public bool Bake()
    {
        if (combine.Count <= 0) 
        {
            gameObject.SetActive(false);
            return false;
        }
        combine.RemoveAt(combine.Count - 1);
        meshFilter.mesh.CombineMeshes(combine.ToArray());
        return true;
    }
}
