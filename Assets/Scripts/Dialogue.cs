using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public string[] lines;
    public float textSpeed;

    public int index;
    // Update is called once per frame

    private void Start()
    {
        dialogue.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(dialogue.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogue.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine()
    {
        if(index == 1)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }if(index == 4)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }
        if(index == 6)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }
        if(index == 8)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }
        if(index == 11)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }
        if (index == 13)
        {
            Training.instance.UpdateTrainingIndex();
            gameObject.SetActive(false);
            return;
        }

        if (index < lines.Length - 1)
        {
            index++;
            dialogue.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
