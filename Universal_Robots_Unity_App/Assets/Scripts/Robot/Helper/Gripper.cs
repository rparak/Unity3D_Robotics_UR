using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robot
{
    public class Gripper : MonoBehaviour
    {
        public static Gripper right;
        public static Gripper left;

        public float openYpos;
        public float closedYpos;



        public static void Open()
        {
            //TODO Tell Robot
            right.Open(true);
            left.Open(true);
        }






        private void Open(bool state)
        {
            if(state) transform.LeanMoveLocalY(openYpos, 1f);
            else transform.LeanMoveLocalY(closedYpos, 1f);
        }
    }
}

