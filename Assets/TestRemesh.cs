using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRemesh : MonoBehaviour {
    private MeshFilter mesh;
	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshFilter>();
        for (int i = 0; i < mesh.mesh.vertices.Length-1; i++)
        {
            mesh.mesh.vertices[i] = new Vector3(0,0,0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
