using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TGraphObject", menuName = "TGraph/TGraphObject", order = 200)]
public class TGraphData : ScriptableObject
{
    public List<TGraphLinkData> LinkData = new List<TGraphLinkData>();
    public List<TGraphNodeData> NodeData = new List<TGraphNodeData>();
}
