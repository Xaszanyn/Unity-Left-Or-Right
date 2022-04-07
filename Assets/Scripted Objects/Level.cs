using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public string name;
    
    public GameObject level;

    public List<Log> logs;

    public Vector2 ball;

    public Vector2 success;

    [System.Serializable] public struct Log
    {
        public Vector2 position;
        
        public float length;

        public Node node;

        public bool vertical;

        public enum Node {
            center, left, right, uncontrollable
        }
    }
}