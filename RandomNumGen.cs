using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;


public class RandomNumGen : MonoBehaviour {

    float seed;

    int[,] rooms;
    Vector3[] roomLocations;

    public int numRooms = 1000;
    public GameObject tile;
    public GameObject parent;

    List<GameObject> parentList = new List<GameObject>();

	void Start ()
    {
        roomLocations = new Vector3[numRooms];
        rooms = new int[numRooms, 2];
        for(int i  = 0; i < numRooms; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                double temp = Poisson.Sample(new MersenneTwister(), 4.0);
                int tempInt = (int)temp;
                rooms[i, j] = tempInt;
            }
        }
        MakeRooms(rooms,numRooms);
	}

    void MakeRooms(int[,] roomSize, int numRooms)
    {
        for(int i = 0;i < roomSize.GetLength(0);i++)
        {
            Vector3 temp = RandomVector3();
            Generate(roomSize[i, 0], roomSize[i, 1], temp);    
        }
    }

    void Generate(int width, int length, Vector3 rootPos)
    {
        List<GameObject> objList = new List<GameObject>();
        Vector3 temp = rootPos;
        for(int i = 0; i < width;i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject tempObj = Instantiate(tile, new Vector3(temp.x, 0.0f, temp.z),Quaternion.identity) as GameObject;
                objList.Add(tempObj);
                temp.z = temp.z + 1.0f;
            }
            temp.z = rootPos.z;
            temp.x = temp.x + 1.0f;
        }
        GameObject tempPar = Instantiate(parent, rootPos, Quaternion.identity) as GameObject;
        parentList.Add(tempPar);
        for(int i = 0; i < objList.Count; i++)
        {
            objList[i].transform.SetParent(tempPar.transform);
        }
    }

    Vector3 RandomVector3()
    {
        float randX = Random.Range(-100.0f, 100.0f);
        float randZ = Random.Range(-100.0f, 100.0f);
        bool temp = Physics.CheckSphere(new Vector3(randX, 0.0f, randZ), 10.0f);
        if(!temp)
        {
            Vector3 tempVec = new Vector3(randX, 0.0f, randZ);
            return tempVec;
        }
        else
        {
            Vector3 tempVec = RandomVector3();
            return tempVec;
        }
    }

    public List<GameObject> GetParentList()
    {
        return parentList;
    }
}
