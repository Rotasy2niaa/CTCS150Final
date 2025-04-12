using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeCalculate : MonoBehaviour
{
    [SerializeField] bool pictureTaken = false;
    [SerializeField] DrawFrame drawFrame;
    [SerializeField] List<NodeScript> nodeScript;
    [SerializeField] float censusValue, tempValue;
    [SerializeField] TextMeshProUGUI censusText;

    MeshCollider meshCollider;

    private void Start()
    {
        nodeScript = new List<NodeScript>();
        StartCoroutine(Calculate());
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = drawFrame.GetComponent<MeshFilter>().mesh;
    }

    IEnumerator Calculate()
    {
        while (true)
        {

            if (pictureTaken)
            {
                tempValue = 0;
                censusValue = 0;
                foreach (NodeScript node in nodeScript)
                {
                    tempValue += node.CensusValue;
                    node.IsCounted = false;
                }

                censusValue = tempValue / nodeScript.Count;
                Debug.Log(censusValue);
                nodeScript.Clear();
            }
            pictureTaken = false;
            censusText.text = "Value = " + censusValue.ToString();
            yield return null;
        }
    }

    private void Update()
    {
        meshCollider.sharedMesh = drawFrame.GetComponent<MeshFilter>().mesh;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pictureTaken = true;
        }
    }

    public void SetPictureTaken(bool taken)
    {
        pictureTaken = taken;
    }

    public bool GetPictureTaken()
    {
        return pictureTaken;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Node") && !other.GetComponent<NodeScript>().IsCounted)
        {
            NodeScript node = other.GetComponent<NodeScript>();
            nodeScript.Add(node);
            node.IsCounted = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Node") && collision.GetComponent<NodeScript>().IsCounted)
        {
            NodeScript node = collision.GetComponent<NodeScript>();
            nodeScript.Remove(node);
            node.IsCounted = false;
        }
    }
}

