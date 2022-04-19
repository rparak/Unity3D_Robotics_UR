using System.Collections;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    private static StaticCoroutine m_instance;

    // OnDestroy is called when the MonoBehaviour will be destroyed.
    // Coroutines are not stopped when a MonoBehaviour is disabled, but only when it is definitely destroyed.
    private void OnDestroy()
    { m_instance.StopAllCoroutines(); }

    // OnApplicationQuit is called on all game objects before the application is closed.
    // In the editor it is called when the user stops playmode.
    private void OnApplicationQuit()
    { m_instance.StopAllCoroutines(); }

    // Build will attempt to retrieve the class-wide instance, returning it when available.
    // If no instance exists, attempt to find another StaticCoroutine that exists.
    // If no StaticCoroutines are present, create a dedicated StaticCoroutine object.
    private static StaticCoroutine Build()
    {
        if (m_instance != null)
        { return m_instance; }

        m_instance = (StaticCoroutine)FindObjectOfType(typeof(StaticCoroutine));

        if (m_instance != null)
        { return m_instance; }

        GameObject instanceObject = new GameObject("StaticCoroutine");
        instanceObject.AddComponent<StaticCoroutine>();
        m_instance = instanceObject.GetComponent<StaticCoroutine>();

        if (m_instance != null)
        { return m_instance; }

        Debug.LogError("Build did not generate a replacement instance. Method Failed!");

        return null;
    }

    // Overloaded Static Coroutine Methods which use Unity's default Coroutines.
    // Polymorphism applied for best compatibility with the standard engine.
    public static void Start(string methodName)
    { Build().StartCoroutine(methodName); }
    public static void Start(string methodName, object value)
    { Build().StartCoroutine(methodName, value); }
    public static void Start(IEnumerator routine)
    { Build().StartCoroutine(routine); }

    public static void Stop()
    { Build().StopAllCoroutines(); }
    public static void Stop(IEnumerator routine)
    { Build().StopCoroutine(routine); }
}
