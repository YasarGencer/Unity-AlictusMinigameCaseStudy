using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager gamemanager;
    [SerializeField]
    GameObject[] cheese;
    [SerializeField]
    TextMeshProUGUI text;
    int destroyedCheese = 0, startCheeseCount;
    private void Start()
    {
        if (gamemanager == null) gamemanager = this;
        startCheeseCount = cheese.Length;
    }
    public void DestroyCheese(GameObject gameObject) => StartCoroutine(DestroyCheeseIenum(gameObject));
    IEnumerator DestroyCheeseIenum(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Destroy");
        yield return new WaitForSeconds(1F);
        Destroy(gameObject);
        destroyedCheese++;

        if (destroyedCheese == startCheeseCount)
            End();
    }
    void End()
    {
        text.gameObject.GetComponent<Animator>().SetTrigger("Completed");
        Invoke("Restart", 1f);
    }
    void Restart() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
