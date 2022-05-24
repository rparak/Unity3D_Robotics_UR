using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Robot/Data")]
public class Data : ScriptableObject
{
    public static Data Current;

    public Vector3 position;
    public Vector3 rotation;

    public double[] jointRot = new double[6];


    public Pose ToPose(bool useJointData = true) => new Pose(this, useJointData);


    public float[] EulerJointAngles { get
        {
            float[] angles = new float[6];

            //1Deg = 0.0174532925199;
            
            for (int i = 0; i < jointRot.Length; i++)
            {
                angles[i] = (float)(jointRot[i] / 0.0174532925199);
            }

            return angles;
        } 
    }



    //Static Preconfigured

    public static Data Home
    {
        get
        {
            Data data = ScriptableObject.CreateInstance<Data>();
            data.jointRot[0] = 3.14159250259399;
            data.jointRot[1] = -1.5707963267949;
            data.jointRot[2] = 1.57079632679489;
            data.jointRot[3] = -1.57079632679489;
            data.jointRot[4] = -1.5707963267949;
            data.jointRot[5] = -3.14158422151674;
            return data;
        }
    }

    public static Data Zero
    {
        get
        {
            Data data = ScriptableObject.CreateInstance<Data>();
            data.jointRot[0] = 0;
            data.jointRot[1] = -1.5707963267949;
            data.jointRot[2] = 0;
            data.jointRot[3] = -1.5707963267949;
            data.jointRot[4] = 4.44089209850063e-16;
            data.jointRot[5] = 0;
            return data;
        }
    }
}
