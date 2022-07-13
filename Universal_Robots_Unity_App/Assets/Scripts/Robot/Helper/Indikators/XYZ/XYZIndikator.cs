using UnityEngine;
using UnityEngine.InputSystem;

namespace Robot
{
    internal class XYZIndikator : MonoBehaviour
    {
        public LineRenderer line;
        public float threshold;
        public Axis usedAxis;

        private Vector2 mouseStartPos;


        public void Enable()
        {
            mouseStartPos = Mouse.current.position.ReadValue();
            gameObject.SetActive(true);
        }

        private void Start() //To test stuff
        {
            //mouseStartPos = Mouse.current.position.ReadValue();
            mouseStartPos = new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2);
        }

        public Vector3 Disable()
        {
            gameObject.SetActive(false);
            return Difference();
        }

        public void Update()
        {
            //Get Diff and return in percentage
            Vector2 diff = Difference();
            diff /= new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2);
            Debug.Log(diff);

            if (diff.x > Screen.currentResolution.width * .2 && diff.x < Screen.currentResolution.width * .2) usedAxis = Axis.x;
            else if (diff.y > Screen.currentResolution.height * .2 && diff.y < Screen.currentResolution.height * .2) usedAxis = Axis.y;





            Line(diff);

            
        }

        private void Line(Vector2 distance)
        {
            Vector3 position = RobotPos.Current.position;
            line.SetPosition(0, position);

            switch (usedAxis)
            {
                case Axis.x: position.x += distance.magnitude; break;
                case Axis.y: position.y += distance.magnitude; break;
                case Axis.z: position.z += distance.magnitude; break;
            }

            line.SetPosition(1, position);
        }

        private Vector3 Difference()
        {
            Vector2 newPos = Mouse.current.position.ReadValue();
            return new Vector3(newPos.x - mouseStartPos.x, newPos.y - mouseStartPos.y);
        }

        public enum Axis { x,y,z }

    }
}

