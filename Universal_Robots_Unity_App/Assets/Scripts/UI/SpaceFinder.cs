using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFinder : MonoBehaviour
{
    public Transform root;


    public Obj cube, robot;
    [Space]
    public bool displayLogs;


    void Update()
    {
        cube.ConvertFromRoot(root.position);
        cube.ApplyOffset();
        cube.ApplyMultiplier();

        robot.ConvertFromRoot(root.position);
        robot.ApplyOffset();
        robot.ApplyMultiplier();

        

        if (displayLogs)
        {
            displayLogs = false;
            Debug.Log($"Cube: {cube.position:0.00} | Robot: {robot.position:0.00}");
        }
    }

    [System.Serializable]
    public class Obj
    {
        public Transform transform;
        public Vector3 position, offset, multiplier;


        public void ConvertFromRoot(Vector3 root) => position = transform.position - root;
        public void ApplyOffset() => position -= offset;
        public void ApplyMultiplier() => position = new Vector3(position.x * multiplier.x, position.y * multiplier.y, position.z * multiplier.z);
    }
}
