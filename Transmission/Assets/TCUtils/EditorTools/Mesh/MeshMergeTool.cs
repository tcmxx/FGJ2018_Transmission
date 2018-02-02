using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshMergeTool : MonoBehaviour {
    public GameObject rootObject;
    public bool generate = false;
    public bool disableOrigins = false;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (generate)
        {
            CombineMeshes();
            generate = false;

        }
    }


    public void CombineMeshes()
    {
        Dictionary<Material,List<int>> tempMats = new Dictionary<Material, List<int>>();

        MeshFilter[] meshFilters = rootObject.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = rootObject.GetComponentsInChildren<MeshRenderer>();

        Debug.Assert(meshFilters.Length == meshRenderers.Length, "All objects with mesh filter need a mesh renderer");


        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {

            //if the meshes use the same mat, them mark them to be the same submesh later
            Material sharedMat = meshRenderers[i].sharedMaterial;
            if (!tempMats.ContainsKey(sharedMat)) {
                tempMats[sharedMat] = new List<int>();
            }
            tempMats[sharedMat].Add(i);

            combine[i].subMeshIndex = 0;
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            if(disableOrigins)
                meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combine,false);

        //reset the submeshes for meshes of the same material

        int subMeshInd = 0;
        Material[] origins = new Material[tempMats.Keys.Count];  //list of original materials
        foreach (var p in tempMats)
        {
            origins[subMeshInd] = p.Key;
            List<int> submeshTriangles = new List<int>();
            foreach(var m in p.Value)
            {
                submeshTriangles.AddRange(newMesh.GetTriangles(m));
            }
            newMesh.SetTriangles(submeshTriangles.ToArray(), subMeshInd);
            subMeshInd++;
        }
        newMesh.subMeshCount = subMeshInd;

        
        //set the mesh to the mesh fileter
        transform.GetComponent<MeshFilter>().mesh = newMesh;

        //set the material for the final mesh renderer
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.materials = origins;
    }



}
