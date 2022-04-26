using UnityEngine;
using UnityEngine.InputSystem;

public static class Step
{


    public static float ClosestStep(float value, float step)
    {
        float absValue = Mathf.Abs(value);
        step = Mathf.Abs(step);

        // Determing the numbers on either side of value
        float low = absValue - absValue % step;
        float high = low + step;

        // Return the closest one, multiplied by -1 if value < 0
        var result = absValue - low < high - absValue ? low : high;
        return result * Mathf.Sign(value);
    }

    public static float ClosestStepDiff(float value, float step)
    {
        float absValue = Mathf.Abs(value);
        step = Mathf.Abs(step);

        // Determing the numbers on either side of value
        float low = absValue - absValue % step;
        float high = low + step;

        // Return the closest one, multiplied by -1 if value < 0
        var result = absValue - low < high - absValue ? low : high;
        return result;
    }
}
