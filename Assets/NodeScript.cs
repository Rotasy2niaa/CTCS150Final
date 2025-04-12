using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [SerializeField] float censusValue;
    [SerializeField] bool isCounted;

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
}
