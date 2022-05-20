using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Converter
{
    

    public static Vector3 ToRobotPos(this Vector3 unityPos)
    {
        return new Vector3(
            unityToRobotX.Evaluate(unityPos.x),
            (unityPos.y - 0.882f) / 1.36f,
            unityToRobotZ.Evaluate(unityPos.z));
    }

    public static Vector3 ToUnityPos(this Vector3 robotPos)
    {
        //return new Vector3(-robotPos.x, -robotPos.z, -robotPos.y);

        return new Vector3(
            robotToUnityX.Evaluate(robotPos.x),
            0.882f + (1.36f * robotPos.y),
            robotToUnityZ.Evaluate(robotPos.z));
    }


    public static AnimationCurve robotToUnityZ = new AnimationCurve(
        new Keyframe(-.6f, -.54f),
        new Keyframe(-.5f, -0.39f),
        new Keyframe(-.4f, -0.238f),
        new Keyframe(-.3f, -0.084f),
        new Keyframe(-.2f, 0.073f),
        new Keyframe(-.1f, 0.234f),
        new Keyframe(0f, 0.395f),
        new Keyframe(.1f, 0.553f),
        new Keyframe(.2f, 0.706f),
        new Keyframe(.3f, 0.853f),
        new Keyframe(.4f, 0.996f),
        new Keyframe(.5f, 1.136f),
        new Keyframe(.6f, 1.277f));


    public static AnimationCurve unityToRobotZ = new AnimationCurve(
    new Keyframe(-.54f, -.6f),
    new Keyframe(-0.39f, -.5f),
    new Keyframe(-0.238f, -.4f),
    new Keyframe(-0.084f, -.3f),
    new Keyframe(0.073f, -.2f),
    new Keyframe(0.234f, -.1f),
    new Keyframe(0.395f, 0f),
    new Keyframe(0.553f, .1f),
    new Keyframe(0.706f, .2f),
    new Keyframe(0.853f, .3f),
    new Keyframe(0.996f, .4f),
    new Keyframe(1.136f, .5f),
    new Keyframe(1.277f, .6f));


    public static AnimationCurve robotToUnityX = new AnimationCurve(
        new Keyframe(-.7f, -4.708f),
        new Keyframe(-.6f, -4.562f),
        new Keyframe(-.5f, -4.417f),
        new Keyframe(-.4f, -4.271f),
        new Keyframe(-.3f, -4.123f),
        new Keyframe(-.2f, -3.972f),
        new Keyframe(-.1f, -3.812f),
        new Keyframe(0f, -3.633f),
        new Keyframe(.1f, -3.447f),
        new Keyframe(.2f, -3.286f),
        new Keyframe(.3f, -3.143f),
        new Keyframe(.4f, -3.005f),
        new Keyframe(.5f, -2.868f),
        new Keyframe(.6f, -2.73f),
        new Keyframe(.7f, -2.59f));


    public static AnimationCurve unityToRobotX = new AnimationCurve(
        new Keyframe(-4.708f, -.7f),
        new Keyframe(-4.562f, -.6f),
        new Keyframe(-4.417f, -.5f),
        new Keyframe(-4.271f, -.4f),
        new Keyframe(-4.123f, -.3f),
        new Keyframe(-3.972f, -.2f),
        new Keyframe(-3.812f, -.1f),
        new Keyframe(-3.633f, 0f),
        new Keyframe(-3.447f, .1f),
        new Keyframe(-3.286f, .2f),
        new Keyframe(-3.143f, .3f),
        new Keyframe(-3.005f, .4f),
        new Keyframe(-2.868f, .5f),
        new Keyframe(-2.73f, .6f),
        new Keyframe(-2.59f, .7f));
}
