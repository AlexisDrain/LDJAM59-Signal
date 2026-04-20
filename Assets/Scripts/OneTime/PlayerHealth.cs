using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    /* Alexis Clay Drain */
    public AudioClip loseHealthSFX;
    public AudioClip loseGoalSFX;
    public List<Image> sprites = new List<Image>();
    public Sprite heartYes;
    public Sprite heartNo;
    void Start()
    {
        
    }
    public void UpdateHealthValue(int newHealth) {
        if (newHealth == 0) {
            sprites[0].GetComponent<Image>().sprite = heartNo;
            sprites[1].GetComponent<Image>().sprite = heartNo;
            sprites[2].GetComponent<Image>().sprite = heartNo;
            sprites[3].GetComponent<Image>().sprite = heartNo;
            sprites[4].GetComponent<Image>().sprite = heartNo;
        } else if (newHealth == 1) {
            sprites[0].GetComponent<Image>().sprite = heartYes;
            sprites[1].GetComponent<Image>().sprite = heartNo;
            sprites[2].GetComponent<Image>().sprite = heartNo;
            sprites[3].GetComponent<Image>().sprite = heartNo;
            sprites[4].GetComponent<Image>().sprite = heartNo;
        } else if (newHealth == 2) {
            sprites[0].GetComponent<Image>().sprite = heartYes;
            sprites[1].GetComponent<Image>().sprite = heartYes;
            sprites[2].GetComponent<Image>().sprite = heartNo;
            sprites[3].GetComponent<Image>().sprite = heartNo;
            sprites[4].GetComponent<Image>().sprite = heartNo;
        } else if (newHealth == 3) {
            sprites[0].GetComponent<Image>().sprite = heartYes;
            sprites[1].GetComponent<Image>().sprite = heartYes;
            sprites[2].GetComponent<Image>().sprite = heartYes;
            sprites[3].GetComponent<Image>().sprite = heartNo;
            sprites[4].GetComponent<Image>().sprite = heartNo;
        } else if (newHealth == 4) {
            sprites[0].GetComponent<Image>().sprite = heartYes;
            sprites[1].GetComponent<Image>().sprite = heartYes;
            sprites[2].GetComponent<Image>().sprite = heartYes;
            sprites[3].GetComponent<Image>().sprite = heartYes;
            sprites[4].GetComponent<Image>().sprite = heartNo;
        } else if (newHealth == 5) {
            sprites[0].GetComponent<Image>().sprite = heartYes;
            sprites[1].GetComponent<Image>().sprite = heartYes;
            sprites[2].GetComponent<Image>().sprite = heartYes;
            sprites[3].GetComponent<Image>().sprite = heartYes;
            sprites[4].GetComponent<Image>().sprite = heartYes;
        } else {
            print("Warning: player health is not between 5 and 1!");
        }
    }
    public void PlayerLoseGoal() {
        GameManager.currentHealth -= 1;
        UpdateHealthValue(GameManager.currentHealth);
    }
    public void PlayerLoseHealth() {
        GameManager.currentHealth -= 1;
        UpdateHealthValue(GameManager.currentHealth);
        GameManager.SpawnLoudAudio(loseHealthSFX);
    }
    public void PlayerRestoreAllHealth() {
        GameManager.currentHealth -= 5;
        UpdateHealthValue(GameManager.currentHealth);
    }
}
