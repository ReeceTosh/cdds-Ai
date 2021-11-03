using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: THIS CODE WAS USED FROM 'CODE MONKEY' ON YOUTUBE TO GENERATE A VISUAL GRID BOX FOR THIS PROJECT
//THIS IS FOR LEARNING AND ALL CREDIT FOR THE CODE GOES TO 'CODE MONKEY'.

public class UtilityClass : MonoBehaviour
{

    public const int sortingOrderDefault = 5000;



    // Creation of the grid
    //Retrieves mouse position within the world space
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    //Creates Grid with TextMesh in Unity.
    public static TextMesh CreateGridText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        if (color == null) color = Color.white;
        return CreateGridText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    public static TextMesh CreateGridText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("Grid_Text", typeof(TextMesh));

        //Sets up the gameObjects transform and position equal to the inputed transform and position.
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();

        //Assignment of variables of type textMesh equal to the parameters passed in.
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Heat Map colour effects
    private static readonly Vector3 Vector3zero = Vector3.zero;
    private static readonly Vector3 Vector3one = Vector3.one;
    private static readonly Vector3 Vector3Down = new Vector3(0, -1);

    private static Quaternion[] cachedQuaternionEulerArr;
    private static void CachedQuaterionEuler()
    {
        if (cachedQuaternionEulerArr != null) return;
        cachedQuaternionEulerArr = new Quaternion[360];
        for (int i = 0; i < 360; i++)
        {
            cachedQuaternionEulerArr[i] = Quaternion.Euler(0, 0, i);
        }
    }

    private static Quaternion GetQuaternionEuler(float rotFloat)
    {
        int rot = Mathf.RoundToInt(rotFloat);
        rot = rot % 360;
        if (rot < 0) rot += 360;

        if (cachedQuaternionEulerArr == null) CachedQuaterionEuler();
        return cachedQuaternionEulerArr[rot];
    }

    public static Mesh CreateEmptyMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[0];
        mesh.uv = new Vector2[0];
        mesh.triangles = new int[0];
        return mesh;
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

    public static Mesh CreateMesh(Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    {
        return AddToMesh(null, pos, rot, baseSize, uv00, uv11);
    }

    public static Mesh AddToMesh(Mesh mesh, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    {
        if (mesh == null)
        {
            mesh = CreateEmptyMesh();
        }

        Vector3[] vertices = new Vector3[4 + mesh.vertices.Length];
        Vector2[] uvs = new Vector2[4 + mesh.uv.Length];
        int[] triangles = new int[6 + mesh.triangles.Length];

        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uvs, 0);
        mesh.triangles.CopyTo(triangles, 0);

        int index = vertices.Length / 4 - 1;
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        baseSize *= .5f;

        bool skewed = baseSize.x != baseSize.y;
        if (skewed)
        {
            vertices[vIndex0] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, baseSize.y);
            vertices[vIndex1] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, -baseSize.y);
            vertices[vIndex2] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, -baseSize.y);
            vertices[vIndex3] = pos + GetQuaternionEuler(rot) * baseSize;
        }
        else
        {
            vertices[vIndex0] = pos + GetQuaternionEuler(rot - 270) * baseSize;
            vertices[vIndex1] = pos + GetQuaternionEuler(rot - 180) * baseSize;
            vertices[vIndex2] = pos + GetQuaternionEuler(rot - 90) * baseSize;
            vertices[vIndex3] = pos + GetQuaternionEuler(rot - 0) * baseSize;
        }

        uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
        uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
        uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
        uvs[vIndex3] = new Vector2(uv11.x, uv11.y);

        //mesh.bounds = bounds;

        return mesh;
    }

    public static void AddToMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 pos, float rot, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    {
        //Relocate vertices
        int vIndex = index * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        baseSize *= .5f;

        bool skewed = baseSize.x != baseSize.y;
        if (skewed)
        {
            vertices[vIndex0] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, baseSize.y);
            vertices[vIndex1] = pos + GetQuaternionEuler(rot) * new Vector3(-baseSize.x, -baseSize.y);
            vertices[vIndex2] = pos + GetQuaternionEuler(rot) * new Vector3(baseSize.x, -baseSize.y);
            vertices[vIndex3] = pos + GetQuaternionEuler(rot) * baseSize;
        }
        else
        {
            vertices[vIndex0] = pos + GetQuaternionEuler(rot - 270) * baseSize;
            vertices[vIndex1] = pos + GetQuaternionEuler(rot - 180) * baseSize;
            vertices[vIndex2] = pos + GetQuaternionEuler(rot - 90) * baseSize;
            vertices[vIndex3] = pos + GetQuaternionEuler(rot - 0) * baseSize;
        }

        //Relocate UVs
        uvs[vIndex0] = new Vector2(uv00.x, uv11.y);
        uvs[vIndex1] = new Vector2(uv00.x, uv00.y);
        uvs[vIndex2] = new Vector2(uv11.x, uv00.y);
        uvs[vIndex3] = new Vector2(uv11.x, uv11.y);

        //Create triangles
        int tIndex = index * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex3;
        triangles[tIndex + 2] = vIndex1;

        triangles[tIndex + 3] = vIndex1;
        triangles[tIndex + 4] = vIndex3;
        triangles[tIndex + 5] = vIndex2;
    }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);

    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        //create a new instance of a gameobject to be generated and used
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));

        //setting up background setting of its transform, parent/child relationship and is new local position (vector3)
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        //Retrieve the textmesh that exists and set the anchor, alignment, text, fontsize, color and its sorting order
        //in a specific way when generated.
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    // Returns 00-FF, value 0->255
    public static string Dec_to_Hex(int value)
    {
        return value.ToString("X2");
    }

    // Returns 0-255
    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }

    // Returns a hex string based on a number between 0->1
    public static string Dec01_to_Hex(float value)
    {
        return Dec_to_Hex((int)Mathf.Round(value * 255f));
    }

    // Returns a float between 0->1
    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }

    // Get Color from Hex string FF00FFAA
    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }
}
