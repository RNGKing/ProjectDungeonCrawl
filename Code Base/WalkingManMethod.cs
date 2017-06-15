using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingManMethod : MonoBehaviour {

    public GameObject tile;
    public Transform spawnPoint;

    public int width = 100;
    public int height = 100;
    public int maxNumTiles = 500;
    int numTiles = 0;
    bool canSpawn = true;

    int currentX;
    int currentY;
    Tile[,] level;
    class Tile
    {
        public bool active = false;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = Active;
            }
        }
        string type;
        public string Type {
            get
            {
                return type;
            }
            set
            {
                type = Type;
            }
        }
    }


	void Start ()
    {
        level = new Tile[width, height];
        for (int i = 0; i < width;i++)
        {
            for(int j = 0;j <height;j++)
            {
                level[i, j] = new Tile();
            }
        }
        GenerateLevel();
        SpawnLevel(level);
	}

    // Returns a Tile Array based on the height and width of the spawn zone

    void GenerateLevel()
    {
        currentX = Random.Range(0, level.GetLength(0) - 1);
        currentY = Random.Range(0, level.GetLength(1) - 1);
       // Debug.Log(level[currentX,currentY]);
        level[currentX, currentY].active = true;
        for(int i = 0; i <= (width*height); i++)
        {
            //Debug.Log(i);
            if(numTiles == maxNumTiles)
            {
                canSpawn = false;
                break;
            }
           Debug.Log("We're walking at " + currentX + " " + currentY);
            Decision(level, currentX, currentY, canSpawn);
            
            //Debug.Log(level[currentX, currentY].active);
        }
    }

    //Builds the Tile[] array based on the input paramenters. 
    void Decision( Tile[,] level, int posX, int posY, bool canSpawn)
    {
        if(numTiles == maxNumTiles)
        {
            return;
        }
        if (canSpawn)
        {
            int randomNum = Random.Range(0, 1000);
            if(randomNum < 250) // up
            {
                if (posY+1 < height)
                {
                    if (level[posX, posY + 1].active == true)
                    {
                        currentY = currentY + 1;
                        Decision(level, currentX, currentY, canSpawn);
                        return;
                    }
                    else
                    {
                        level[posX, posY + 1].active = true;
                        currentY = currentY + 1;
                        numTiles = numTiles + 1;
                        return;
                    }
                }
                else
                {
                    
                    Decision(level, currentX, currentY, canSpawn);
                    return;
                }
            } //UP
            else if(randomNum >= 250 && randomNum < 500) // right
            {
                if (posX + 1 < width)
                {
                    if (level[posX + 1, posY].active == true)
                    {
                        currentX = currentX + 1;
                        Decision(level, currentX, currentY, canSpawn);
                        return;
                    }
                    else
                    {
                        level[posX + 1, posY].active = true;
                        currentX = currentX + 1;
                        numTiles = numTiles + 1;
                        return;
                    }
                }
                else
                {
                   
                    Decision(level, currentX, currentY, canSpawn);
                    return;
                }
            } // RIGHT
            else if(randomNum >=500 && randomNum < 750)
            {
                if (posY - 1 > 0)
                {
                    if (level[posX, posY - 1].active == true)
                    {
                        currentY = currentY - 1;
                        Decision(level, currentX, currentY, canSpawn);
                        return;
                    }
                    else
                    {
                        level[posX, posY - 1].active = true;
                        currentY = currentY - 1;
                        numTiles = numTiles + 1;
                        return;
                    }
                }
                else
                {
                    
                    Decision(level, currentX, currentY, canSpawn);
                    return;
                }
            } // DOWN
            else
            {
                if (posX - 1 > 0)
                {
                    if (level[posX - 1, posY].active == true)
                    {
                        currentX = currentX - 1;
                        Decision(level, currentX, currentY, canSpawn);
                        return;
                    }
                    else
                    {
                        level[posX - 1, posY].active = true;
                        currentX = currentX - 1;
                        numTiles = numTiles + 1;
                        return;
                    }
                }
                else
                {
                    
                    Decision(level, currentX, currentY, canSpawn);
                    return;
                }
            } // LEFT
        }
    }

    void SpawnLevel(Tile[,] level)
    {
        for(int i = 0;i < level.GetLength(0); i++)
        {
            for(int j = 0; j < level.GetLength(1); j++)
            {
                if (level[i, j].Active == true)
                {
                    Instantiate(tile, spawnPoint.position, Quaternion.identity);
                }
                spawnPoint.position = new Vector3(spawnPoint.position.x, 0.0f, spawnPoint.position.z + 1.0f);
            }

            spawnPoint.position = new Vector3(spawnPoint.position.x + 1.0f, 0.0f, 0.0f);
        }
    }
   





}
