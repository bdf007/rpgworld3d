using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCollisions : MonoBehaviour
{
    public GameObject dial;
    public GameObject dialTxt;
    public GameObject sword;
    private void OnTriggerEnter(Collider other)
    {

        // Déclenche le dialogue 1
        if(other.gameObject.name == "triggerDialogue1")
        {
            // si on a deja l'epee
            if(sword.activeSelf)
            {
                // on detruit le trigger
                Destroy(GameObject.Find("obstacle1"));
            } else
            {

            dial.SetActive(true);
            dialTxt.GetComponent<TextMeshProUGUI>().text = "Pour passer, il te faut une épée.";
            }
        }

        if(other.gameObject.name == "Sword_pickup")
        {
            Destroy(other.gameObject);
            sword.SetActive(true);
        }

        // coroutine pour masquer le dialogue
        StartCoroutine(HideDial());
    }

    IEnumerator HideDial()
    {
        yield return new WaitForSeconds(2);
        dial.SetActive(false);
    }
}
