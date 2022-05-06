using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    public TextSizer textSizer;
    public TMPro.TMP_Text textContent;
    public CanvasGroup cg;
    [SerializeField] float time;



    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            return;
        }

        cg.alpha -= Time.deltaTime;

        if(cg.alpha <= 0)
        {
            cg.alpha = 0;
            enabled = false;
        }
    }

    public void Show()
    {
        enabled = false;
        cg.alpha = 1;
        time = 3f;
    }

    public void Hide()
    {
        enabled = true;
    }

    public void Create(string name, string content)
    {
        textContent.text = $"{name}\n{content}";
        textSizer.Refresh();
    }

}
