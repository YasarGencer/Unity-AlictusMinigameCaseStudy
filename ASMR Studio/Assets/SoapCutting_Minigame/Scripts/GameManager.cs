using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager gameManager;
    [SerializeField]
    GameObject[] layers;
    [HideInInspector]
    public int currentLayer = 0;
    [SerializeField] 
    MoveMask mask;
    [SerializeField]
    TextMeshProUGUI text;
    private void Start()
    {
        if (gameManager == null) gameManager = this;
            layers[currentLayer].GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
    public void NextLayer()
    {
        if (currentLayer + 1 == layers.Length)
            StartCoroutine(Complete());
        else
        {
            //makes layers gets mask turn by turn
            layers[currentLayer].SetActive(false);
            layers[currentLayer + 1].GetComponent<MeshRenderer>().material.renderQueue = 3002;
            currentLayer += 1;
            mask.StartPos();
        }
    }
    IEnumerator Complete()
    {
        text.gameObject.GetComponent<Animator>().SetTrigger("Completed");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
