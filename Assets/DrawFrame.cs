using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DrawFrame : MonoBehaviour
{
    public static DrawFrame s_Instance;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public bool inDrawMode = false;

    [SerializeField] private Transform frameTransform;
    [SerializeField] private Material frameMat;
    [SerializeField] private float frameWidth;

    private void Awake()
    {
        s_Instance = this;
    }

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float z = Camera.main.nearClipPlane + 10f;  // 比节点更远一点


        if (!inDrawMode)
        {
            if (Input.GetMouseButton(0))
            {
                inDrawMode = true;
                vertices = new Vector3[4];
                vertices[0] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            }
        }

        else
        {
            if (Input.GetMouseButton(0))
            {
                vertices[1] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Camera.main.WorldToScreenPoint(vertices[0]).y, z));
                vertices[3] = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.WorldToScreenPoint(vertices[0]).x, Input.mousePosition.y, z));
                vertices[2] = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
            }

            if (((vertices[2].x > vertices[0].x) && (vertices[2].y < vertices[0].y)) ||
                ((vertices[2].x < vertices[0].x) && (vertices[2].y > vertices[0].y)))
                triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
            else
                triangles = new int[6] { 0, 3, 2, 0, 2, 1 };
        }

        if (Input.GetMouseButtonUp(0))
        {
            inDrawMode = false;
            SelectNodesInFrame();
        }

        UpdateMesh();
    }

    private void LateUpdate()
    {
    }

    public LineRenderer lineRenderer;

    void UpdateMesh()
    {
        mesh.Clear();
        if (vertices == null || vertices.Length < 3) return;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        if (lineRenderer != null && vertices.Length == 4)
        {
            lineRenderer.positionCount = 5;
            lineRenderer.SetPosition(0, vertices[0]);
            lineRenderer.SetPosition(1, vertices[1]);
            lineRenderer.SetPosition(2, vertices[2]);
            lineRenderer.SetPosition(3, vertices[3]);
            lineRenderer.SetPosition(4, vertices[0]);  // 回到起点
        }
    }


    void SelectNodesInFrame()
    {
        if (vertices == null || vertices.Length < 4) return;

        Vector3 bottomLeft = new Vector3(Mathf.Min(vertices[0].x, vertices[2].x), Mathf.Min(vertices[0].y, vertices[2].y), 0);
        Vector3 topRight = new Vector3(Mathf.Max(vertices[0].x, vertices[2].x), Mathf.Max(vertices[0].y, vertices[2].y), 0);

        NodeScript[] allNodes = FindObjectsOfType<NodeScript>();

        foreach (NodeScript node in allNodes)
        {
            Vector3 pos = node.transform.position;
            bool isInside = pos.x >= bottomLeft.x && pos.x <= topRight.x &&
                            pos.y >= bottomLeft.y && pos.y <= topRight.y;

            node.Highlight(isInside);
        }
    }

    public Mesh GetMesh()
    {
        return mesh;
    }
}
