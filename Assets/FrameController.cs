using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FrameController : MonoBehaviour
{
    public static FrameController s_Instance;

    [SerializeField] private Transform frameTransform;
    [SerializeField] private List<Transform> nodeTransforms;
    [SerializeField] private TMP_Text textUI;

    private bool isSelecting;
    private Vector3 posA;
    private Vector3 posB;
    private LineRenderer lr;
    private List<NodeScript> nodeScripts;
    private float score = 0f;

    private void Awake()
    {
        s_Instance = this;

        frameTransform.localScale = Vector3.zero;

        isSelecting = false;

        posA = Vector3.zero;
        posB = Vector3.zero;

        lr = GetComponent<LineRenderer>();
        lr.positionCount = 5;

        nodeScripts = new List<NodeScript>();
        foreach (Transform node in nodeTransforms)
        {
            nodeScripts.Add(node.GetComponent<NodeScript>());
        }
    }

    void Update()
    {
        if (isSelecting)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 cursorProjection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                cursorProjection.z = -1f;
                posB = cursorProjection;

                frameTransform.position = Vector3.Lerp(posA, posB, 0.5f);

                Vector3 diff = posA - posB;
                diff.x = Mathf.Abs(diff.x);
                diff.y = Mathf.Abs(diff.y);
                diff.z = 0f;

                frameTransform.localScale = diff;

                lr.SetPosition(0, new Vector3(posA.x, posA.y, -1f));
                lr.SetPosition(1, new Vector3(posB.x, posA.y, -1f));
                lr.SetPosition(2, new Vector3(posB.x, posB.y, -1f));
                lr.SetPosition(3, new Vector3(posA.x, posB.y, -1f));
                lr.SetPosition(4, new Vector3(posA.x, posA.y, -1f));

                float bbMinX = Mathf.Min(posA.x, posB.x);
                float bbMaxX = Mathf.Max(posA.x, posB.x);
                float bbMinY = Mathf.Min(posA.y, posB.y);
                float bbMaxY = Mathf.Max(posA.y, posB.y);

                float total = 0;
                int count = 0;
                for (int i = 0; i < nodeTransforms.Count; i++)
                {
                    if (
                        nodeTransforms[i].position.x > bbMinX &&
                        nodeTransforms[i].position.x < bbMaxX &&
                        nodeTransforms[i].position.y > bbMinY &&
                        nodeTransforms[i].position.y < bbMaxY
                    )
                    {
                        nodeScripts[i].Highlight(true);

                        total += nodeScripts[i].CensusValue;
                        count++;
                    }
                    else
                    {
                        nodeScripts[i].Highlight(false);
                    }
                }
                if (count > 0)
                {
                    score = total;
                }
                else
                {
                    score = 0;
                }
                textUI.text = "Value = " + (score).ToString("F2");
            }
            else
            {
                isSelecting = false;
                posA = Vector3.zero;
                posB = Vector3.zero;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;

            Vector3 cursorProjection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorProjection.z = -1f;
            posA = cursorProjection;
            posB = cursorProjection;
        }
    }

    public float GetValue()
    {
        return score;
    }
}
