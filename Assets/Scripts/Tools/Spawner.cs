using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    //TODO: Have them as childs to detect how many are on the scene right now.

    [SerializeField] private GameObject toSpawnObject;

    [Header("Limit Options")]
    [SerializeField] private int spawnLimit;
    [SerializeField] private bool limitSpawns;

    [Header("For Other Methods")]
    /*
    [SerializeField] readonly int spawnedObjects
    {
        get
        {
            return totalObjectsSpawned;
        }
    }*/

    private int totalObjectsSpawned;

    /// <summary>
    /// Spawns the object
    /// </summary>
    public void SpawnObject()
    {
        if (limitSpawns && totalObjectsSpawned >= spawnLimit) return;
        Instantiate(gameObject, transform.position, Quaternion.identity);
        totalObjectsSpawned++;
    }

    /// <summary>
    /// Deletes all the instantiated objects
    /// </summary>
    public void DeleteAllSpawnedObjects()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void SpawnObjectAt()
    {

    }
}
