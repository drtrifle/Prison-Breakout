using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Signboard : MonoBehaviour
{
    public GameObject textBox;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textBox != null)
        {
            textBox.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textBox != null)
        {
            textBox.SetActive(false);
        }
    }
}
