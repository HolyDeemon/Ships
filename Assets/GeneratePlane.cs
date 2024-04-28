using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct WaveProp
{
    public float Amplitude;
    public float steepness;
    public Vector2 WaveVector;
    public float Speed;
    public float Lenght;
    public float sequense;
    public float phaseAngle;

    public WaveProp(float A, float Q, Vector2 D, float V, float L) 
    {
        Amplitude = A;
        steepness = Q;
        WaveVector = D;
        Speed = V;
        Lenght = L;
        sequense = 2/L;
        phaseAngle = Speed * 2/ L;
    }
}


public class GeneratePlane : MonoBehaviour
{




    public MeshFilter Mf;
    public MeshCollider Mc;

    [Header("Generate Properties")]
    public int Size = 10;
    public float QuadSize = 0.5f;

    [Header("Wave Properties")]
    public float Amplitude;
    public float steepness;
    public Vector2 WaveVector;
    public float Speed;
    public float Lenght;
    private float sequense;
    private float phaseAngle;

    private List<WaveProp> waveProps;

    private Vector3[] _vertices;
    private Vector3[] _Subvertices;
    private List<int> _triangles = new List<int>();
    private Vector2[] uvs;

    void Start()
    {
        GenerateMesh();

        GenerateWaves();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }


    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        _vertices = new Vector3[Size * Size];
        uvs = new Vector2[_vertices.Length];


        for (int i = 0, y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++, i++)
            {
                _vertices[i] = new Vector3(x - Size/2, 0, y - Size/2) * QuadSize;
                uvs[i] = new Vector2((float)(x / Size), (float)(y / Size));
            }
        }
        _Subvertices = _vertices;

        for (int i = 0, y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++, i++)
            {
                if(x != Size - 1 && y != Size -1 )
                {
                    _triangles.Add(i);
                    _triangles.Add(i + Size);
                    _triangles.Add(i + 1);


                    _triangles.Add(i + 1);
                    _triangles.Add(i + Size);
                    _triangles.Add(i + 1 + Size);

                }
            }
        }

        int maximum = 0;

        for (int i = 0; i < _triangles.Count; i++)
        {
            if (maximum < _triangles[i])
            {
                maximum = _triangles[i];
            }
        }
        Debug.Log(maximum);

        mesh.vertices = _vertices;
        mesh.triangles = _triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        Mf.mesh = mesh;
    }

    public void GenerateWaves()
    {
        WaveVector.Normalize();
        steepness = Mathf.Clamp(steepness, 0, 0.1f / (sequense * Amplitude));

    }


    public void Animate()
    {
        Vector3[] _NewVertices = _vertices;

        sequense = 2 / Lenght;
        phaseAngle = Speed * 2 / Lenght;

        float sequense1 = 2 / (Lenght + 0.1f);
        float sequense2 = 2 / (Lenght - 0.1f);
        float phaseAngle1 = (Speed - 0.1f) * 2 / (Lenght + 0.1f);
        float phaseAngle2 = (Speed + 0.1f) * 2 / (Lenght - 0.1f);

        Mesh mesh = new Mesh();


        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector2 vertex = new Vector2(_vertices[i].x, _vertices[i].z);
            float vertexy = Amplitude * Mathf.Sin(Vector2.Dot(WaveVector, vertex) * sequense + Time.time * phaseAngle);
            float vertexy1 = (Amplitude + 0.1f) * Mathf.Sin(Vector2.Dot( Vector2.up, vertex) * sequense1 + Time.time * phaseAngle1);
            float vertexy2 = (Amplitude - 0.1f) * Mathf.Sin(Vector2.Dot((new Vector2(-1, -1)).normalized , vertex) * sequense2 + Time.time * phaseAngle);

            _vertices[i].y = vertexy + vertexy2 + vertexy1;
        }

        mesh.vertices = _NewVertices;
        mesh.triangles = _triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        Mc.sharedMesh = mesh;

        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i].y += Mathf.PerlinNoise(_vertices[i].x, _vertices[i].z) * steepness;

        }

        mesh.vertices = _NewVertices;
        mesh.triangles = _triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        Mf.mesh = mesh;
    }
}