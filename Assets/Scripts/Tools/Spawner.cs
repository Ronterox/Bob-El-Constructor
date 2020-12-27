using Plugins.Tools;
using UnityEngine;

namespace Tools
{
    public class Spawner : MonoBehaviour 
    {
        //TODO: Have them as children to detect how many are on the scene right now.

        [SerializeField] private GameObject toSpawnObject;

        [Header("Limit Options")]
        [SerializeField] private int spawnLimit;
        [SerializeField] private bool limitSpawns;

        /*
    [Header("For Other Methods")]
    [SerializeField] readonly int spawnedObjects
    {
        get
        {
            return totalObjectsSpawned;
        }
    }*/

        private int p_totalObjectsSpawned;

        /// <summary>
        /// Spawns the object
        /// </summary>
        public void SpawnObject()
        {
            SoundManager.Instance.Play("Spawn", true);
            if (limitSpawns && p_totalObjectsSpawned >= spawnLimit) return;
            Instantiate(toSpawnObject, transform.position, Quaternion.identity);
            p_totalObjectsSpawned++;
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
}
