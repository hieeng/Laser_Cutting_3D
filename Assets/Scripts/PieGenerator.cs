using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieGenerator : MonoBehaviour
{
    [SerializeField] int _slice = 100;
    [SerializeField] GameObject _obj = null;
    
    GameObject[] pie;
    // Start is called before the first frame update
    void Start()
    {
        pie = new GameObject[_slice];
        for (int j = 0; j < _slice; j ++)
        {
            pie[j] = Instantiate(_obj, transform.position, Quaternion.Euler(0, j * (360.0f / _slice), 0));
            pie[j].transform.parent = transform;
        }

        MeshFilter[] meshFilters;
        CombineInstance[] combine;
        meshFilters = GetComponentsInChildren<MeshFilter>();
        combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].mesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
