using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SplineBuilder : MonoBehaviour
{

    [SerializeField]
    private HoudiniAsset _splineMesh;
    [SerializeField]
    private string _parameterName = "Coordinates";
    [SerializeField]
    private Transform _pivotsParent;

    private HoudiniApiAssetAccessor _assetAccessor;
    private HoudiniAssetOTL _assetOTL;
    private List<Transform> _pivots;

    public bool DebugNow;

    private void Initialize()
    {
        _assetAccessor = HoudiniApiAssetAccessor.getAssetAccessor(_splineMesh.gameObject);
        _assetOTL = _splineMesh.GetComponent<HoudiniAssetOTL>();

        _pivots.Clear();
        for (int i = 0; i < _pivotsParent.childCount; i++)
        {
            _pivots.Add(_pivotsParent.GetChild(i));
        }
    }

    private string BuildPivotsString()
    {
        string parameters = "";

        for (int i = 0; i < _pivots.Count; i++)
        {
            parameters += _pivots[i].position.ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
            parameters += " ";
        }
        parameters = parameters.Substring(0, parameters.Length - 1);
        return parameters;
    }

    private void UpdateValue()
    {
        _assetAccessor.setParmStringValue(_parameterName, 0, BuildPivotsString());
        _assetOTL.buildAll();
    }

    private void Update()
    {
        if (DebugNow)
        {
            Initialize();
            Debug.Log(BuildPivotsString());
            UpdateValue();
            DebugNow = false;
        }
    }

}
