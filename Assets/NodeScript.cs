using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [SerializeField] float censusValue;
    [SerializeField] bool isCounted;

    private Renderer rend;

    public float CensusValue
    {
        get { return censusValue; }
        set { censusValue = value; }
    }

    public bool IsCounted
    {
        get { return isCounted; }
        set { isCounted = value; }
    }

    private void Awake()
    {
        // 获取本体或子物体上的 Renderer
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            rend = GetComponentInChildren<Renderer>();
        }

        if (rend == null)
        {
            Debug.LogWarning("No Renderer found on " + gameObject.name);
        }
    }

    public void Highlight(bool state)
    {
        if (rend != null)
        {
            rend.material.color = state ? Color.yellow : Color.white;
        }
    }
}
