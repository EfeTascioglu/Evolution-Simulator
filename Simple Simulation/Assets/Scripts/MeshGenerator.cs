using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class MeshGenerator 
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail == 0)?1: levelOfDetail * 2;
        //if(levelOfDetail == 0)
        //    meshSimplificationIncrement = 1;
        int verticesPerLine = (width - 1)/meshSimplificationIncrement + 1;


        MeshData meshData = new MeshData (verticesPerLine, verticesPerLine); 
        int vertexIndex = 0;


        for(int y = 0; y < height; y+= meshSimplificationIncrement)
        {
            for(int x = 0; x < width; x+= meshSimplificationIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x/(float)width,y/(float)height);

                if (x < width -1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    int triangleIndex;
    public Vector2[] uvs;
    public int[] slopes;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        slopes = new int[(meshWidth - 1) * (meshHeight - 1)];
    }
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        slopes[triangleIndex/3] = Math.Max(Math.Abs(c - a), Math.Max(Math.Abs(c - b), Math.Abs(b - a))); // Stores the absolute maximum heightDifference
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

/*
[RequireComponent(typeof(MeshFilter))] // Make sure there is always a mesh filter
[RequireComponent(typeof(MeshCollider))] // Make sure there is always a mesh Collider
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float length;
    public float width;
    public float height;

    public int xSize;
    public int zSize;
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // You can do this in a coroutine to see the squares getting generated real time
    public void GenerateMap()
    {

        // Set Mesh object = to calculations
        mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        float[,] heightMap = Noise.GenerateNoiseMap(xSize + 1, zSize + 1, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float bottomLeftX = (xSize) /-2f;
        float bottomLeftZ = (zSize) /-2f;

        if (height == 0)
        {
            height = 0.001f;
        }
        if (length <= 0)
        {
            length = 0.001f;
        }
        if (width <= 0)
        {
            width = 0.001f;
        }

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for(int i = 0, z = 0; z < zSize + 1; z++)
        {
            for(int x = 0; x < xSize + 1; x++)
            {
                //float y = Mathf.PerlinNoise(x * 0.3f,z * 0.3f) * 2f;
                
                float y = heightMap[x,z] * height;
                //Debug.Log("Coordinates: " + x + " " + y + " " + z);
                //Center the grid
                vertices[i] = new Vector3((bottomLeftX + x) / xSize * length,y,(bottomLeftZ + z) / zSize * width);
                //vertices[i] = new Vector3(x,y,z);
                i++;
            }
        }
        // I put the contents of the array in the curly braces

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }     
            vert++;
        }
        UpdateMesh();

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        //return mesh;
    }
    
    // See Points
    /*
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
*/