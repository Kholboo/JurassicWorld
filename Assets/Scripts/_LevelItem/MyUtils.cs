using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyUtils : MonoBehaviour
{
    //Delete container objects;
    public static void DeleteChilds(GameObject _container, float _time = 0)
    {
        foreach (Transform child in _container.transform)
        {
            Destroy(child.gameObject, _time);
        }
    }
    //Object instantiate
    public static GameObject CreateUtils(GameObject _obj, Vector3 _pos, GameObject _objContainer, Quaternion _angle)
    {
        GameObject instUtils = Instantiate(_obj, _pos, _angle);
        //instUtils.transform.localPosition = _pos;
        instUtils.transform.parent = _objContainer.transform;
        return instUtils;
    }
    //Random get list
    public static List<int> UniqueRandomInt(int _startIndex, int _length, bool _random = true)
    {
        List<int> usedValues = new List<int>();
        for (int j = _startIndex; j < _length; j++)
        {
            int Rand = j;
            if (_random)
            {
                Rand = Random.Range(_startIndex, _length);
                while (usedValues.Contains(Rand))
                {
                    Rand = Random.Range(_startIndex, _length);
                }
            }
            usedValues.Add(Rand);
        }
        return usedValues;
    }
    //Get string color to prefs 
    public static Color GetColorPrefs(string str_color)
    {
        //Remove the header and brackets
        str_color = str_color.Replace("RGBA(", "");
        str_color = str_color.Replace(")", "");
        //Get the individual values (red green blue and alpha)
        string[] strings = str_color.Split(","[0]);
        Color outputcolor;
        outputcolor = Color.black;
        for (var i = 0; i < 4; i++)
        {
            outputcolor[i] = System.Single.Parse(strings[i]);
        }
        //apply the color to a gameobject
        return outputcolor;
    }
    //Get middle and center point to points 
    public static Vector2 GetMidPoint(List<Vector3> _vert)
    {
        float totalX = 0f;
        float totalY = 0f;
        foreach (Vector3 _point in _vert)
        {
            totalX += _point.x;
            totalY += _point.z;
        }
        Vector2 retPoint = new Vector2(totalX / _vert.Count, totalY / _vert.Count);
        return retPoint;
    }
    public static void printList(List<int> _levels)
    {
        print(_levels.Count + " =>");
        for (int i = 0; i < _levels.Count; i++)
        {
            print(_levels[i]+".");
        }
    }
    //Get inspector angle
    public static float GetInspectorAngle (Transform _transform) {
        Vector3 angle = _transform.eulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot (_transform.up, Vector3.up) >= 0f) {
            if (angle.x >= 0f && angle.x <= 90f) {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f) {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot (_transform.up, Vector3.up) < 0f) {
            if (angle.x >= 0f && angle.x <= 90f) {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f) {
                x = 180 - angle.x;
            }
        }
        return x;
    }
    public static float Clamp0360 (float eulerAngles) {
        float result = eulerAngles - Mathf.CeilToInt (eulerAngles / 360f) * 360f;
        if (result < 0) {
            result += 360f;
        }
        return result;
    }
}