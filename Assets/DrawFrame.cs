using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFrame : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public bool inDrawMode = false;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
       
    }

    private void Update()
    {

        if (!inDrawMode)
        {
            if (Input.GetMouseButton(0))
            {
                inDrawMode = true;
                vertices = null;
                vertices = new Vector3[4];
                vertices[0] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            }
        }

        if (inDrawMode)
        {
            if (Input.GetMouseButton(0))
            {
                vertices[1] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Camera.main.WorldToScreenPoint(vertices[0]).y, 10f));
                vertices[3] = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.WorldToScreenPoint(vertices[0]).x, Input.mousePosition.y, 10f));
                vertices[2] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            }
            if( ((vertices[2].x > vertices[0].x) && (vertices[2].y < vertices[0].y)) ||
                ((vertices[2].x < vertices[0].x) && (vertices[2].y > vertices[0].y)))
                triangles = new int[6] { 0, 1, 2, 0, 2, 3};
            else
                triangles = new int[6] { 0, 3, 2, 0, 2, 1 };
        }

        if (Input.GetMouseButtonUp(0))
        {
            inDrawMode = false;
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public Mesh GetMesh()
    {
        return mesh;
    }
}

