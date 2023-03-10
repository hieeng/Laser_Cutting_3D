// 츄이
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour
{
    List<MeshFilter> _meshFilters = new List<MeshFilter>();
    List<CombineInstance> _combine = new List<CombineInstance>();
    MeshFilter _meshFilter;
    public List<CombineInstance> CombineInst {get => _combine;}

    public void Bake()
    {
        _meshFilter.mesh.CombineMeshes(_combine.ToArray());
    }

    public void Init()
    {
        MeshFilter[] meshs = GetComponentsInChildren<MeshFilter>();
        _meshFilters.AddRange(meshs);
        _meshFilters.RemoveAt(0);
        Debug.Log(_meshFilters.Count);
        int i = 0;
        while (i < _meshFilters.Count)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = _meshFilters[i].mesh;
            ci.transform = _meshFilters[i].transform.localToWorldMatrix;
            _combine.Add(ci);
            _meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        transform.position = Vector3.zero;
        _meshFilter = transform.GetComponent<MeshFilter>();
        _meshFilter.mesh = new Mesh();
        _meshFilter.mesh.CombineMeshes(_combine.ToArray());
        transform.gameObject.SetActive(true); 
        transform.gameObject.AddComponent<MeshCollider>();
        transform.gameObject.GetComponent<MeshCollider>().convex = true;
        transform.gameObject.GetComponent<Pie>()._combine = _combine;
        //transform.localRotation = Quaternion.Euler(0, -90.0f, 0);
    }
}
