using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    Vector2[] positions;
    float[] velocities;
    float[] accelerations;
    LineRenderer body;
    GameObject[] meshObjects;
    Mesh[] meshes;

    [SerializeField]
    float springConstant = 0.02f;
    [SerializeField]
    float damping = 0.04f;
    [SerializeField]
    float spread = 0.05f;
    const float z = -1f;

    float baseHeight;
    float left;
    float bottom;

    public Material mat;
    public GameObject mesh;

    public float waterLeft = -7;
    public float waterWidth = 14;
    public float waterTop = 0;
    public float waterBottom = -6;
    public int numNodes = 50;

    public Vector2[] Positions
    {
        get
        {
            return positions;
        }
    }

    public int NumNodes
    {
        get
        {
            return numNodes;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        SpawnWater(waterLeft, waterWidth, waterTop, waterBottom);
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            Vector3[] verts = { new Vector3(positions[i].x, positions[i].y, z),
                                new Vector3(positions[i + 1].x, positions[i + 1].y, z),
                                new Vector3(positions[i].x, bottom, z),
                                new Vector3(positions[i + 1].x, bottom, z)};

            meshes[i].vertices = verts;
        }
	}

    void FixedUpdate()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            float force = springConstant * (positions[i].y - baseHeight) + velocities[i] * damping;
            //accelerations[i] = Mathf.Clamp(accelerations[i], -force, 1);
            positions[i].y += velocities[i];
            velocities[i] += accelerations[i];
            accelerations[i] = -force;
            body.SetPosition(i, new Vector3(positions[i].x, positions[i].y, z));
        }

        float[] leftDeltas = new float[positions.Length];
        float[] rightDeltas = new float[positions.Length];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < positions.Length; j++)
            {
                if (j > 0)
                {
                    leftDeltas[j] = spread * (positions[j].y - positions[j - 1].y);
                    velocities[j - 1] += leftDeltas[j];
                }
                if (j < positions.Length - 1)
                {
                    rightDeltas[j] = spread * (positions[j].y - positions[j + 1].y);
                    velocities[j + 1] += rightDeltas[j];
                }
            }

            for (int j = 0; j < positions.Length; j++)
            {
                if (j > 0)
                {
                    positions[j - 1].y += leftDeltas[j];
                }
                if (j < positions.Length - 1)
                {
                    positions[j + 1].y += rightDeltas[j];
                }
            }
        }

    }

    public void SpawnWater(float left, float width, float top, float bottom)
    {
        int edgeCount = numNodes;
        int nodecount = edgeCount + 1;

        body = gameObject.AddComponent<LineRenderer>();
        body.material = mat;
        body.material.renderQueue = 1000;
        body.numPositions = nodecount;
        body.startWidth = .1f;
        body.endWidth = .1f;

        positions = new Vector2[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];

        meshObjects = new GameObject[edgeCount];
        meshes = new Mesh[edgeCount];

        baseHeight = top;
        this.bottom = bottom;
        this.left = left;

        for (int i = 0; i < nodecount; i++)
        {
            positions[i].y = top;
            positions[i].x = left + width * i / edgeCount;
            //Debug.Log("position "+i+" at "+positions)
            accelerations[i] = 0;
            velocities[i] = 0;
            body.SetPosition(i, new Vector3(positions[i].x, positions[i].y, z));
        }

        for (int i = 0; i < edgeCount; i++)
        {
            meshes[i] = new Mesh();
            Vector3[] verts = { new Vector3(positions[i].x, positions[i].y, z),
                                new Vector3(positions[i + 1].x, positions[i + 1].y, z),
                                new Vector3(positions[i].x, bottom, z),
                                new Vector3(positions[i + 1].x, bottom, z)};

            Vector2[] UVs = { new Vector2(0,1),
                                new Vector2(1,1),
                                new Vector2(0,0),
                                new Vector2(1,0) };

            int[] tris = { 0, 1, 3, 3, 2, 0 };

            meshes[i].vertices = verts;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            meshObjects[i] = Instantiate(mesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshObjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshObjects[i].transform.parent = transform;
        }
    }

    public void AddForce(float force, int node)
    {
        //Debug.Log("Force " + force + " on node " + node);
        positions[node].y = force;
    }
}
