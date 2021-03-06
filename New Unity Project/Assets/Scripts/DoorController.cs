﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int keycount;
    public bool unlocked = false;
    PlayerController greg;
    public Dialogue dialogue;

    public void Start()
    {
        greg = FindObjectOfType<PlayerController>();
        dialogue.sentences = new string[2];
        dialogue.name = "Locked Door";
        dialogue.sentences[0] = "[rattle, rattle]";
        dialogue.sentences[1] = "Looks like this door is still locked.";
    }

    public void Update()
    {
        if (greg.keys==keycount)
        {
            unlocked = true;
        } else if (greg.keys > keycount) {
            dialogue.name = "Cleared Room";
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = "You already cleared this room!"; 
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && unlocked)
        {
            FindObjectOfType<GameManager>().LevelUp();
        } else if (collision.gameObject.CompareTag("Player") && !unlocked) {
            TriggerDialogue();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}
