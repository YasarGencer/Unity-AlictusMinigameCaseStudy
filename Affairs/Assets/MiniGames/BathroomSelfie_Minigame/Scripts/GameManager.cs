using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Animator ui, woman;
    [SerializeField]
    GameObject game;
    [SerializeField]
    Transform photoPos;
    [SerializeField]
    GameObject[] photos;

    Vector3 startPos, currPos, dist;

    [HideInInspector]
    public static bool swipeable = false;
    [HideInInspector]
    public static int swipeDir = -1;
    [HideInInspector]
    public static GameObject arrow;

    int swipeCount = 0;

    private void Start() { Invoke("StartAnims", .75f); }
    private void Update()
    {
        if(swipeCount >= 5)
        {
            ui.SetTrigger("Completed");
            Invoke("Restart", 1f);
        }
        else
        {
            if (swipeable == true)
            {
                if (Input.GetMouseButtonDown(0))
                    startPos = Input.mousePosition;
                else if (Input.GetMouseButton(0))
                {
                    currPos = Input.mousePosition;
                    dist.x = (currPos.x) - (startPos.x);
                    dist.y = (currPos.y) - (startPos.y);
                }
                else if (Input.GetMouseButtonUp(0))
                    dist = Vector3.zero;
                Debug.Log("Swipe Direction = " + swipeDir + "  /  Dis Vector = " + dist);
                switch (swipeDir)
                {
                    case 0:
                        if (dist.x < 0)
                            Swipe();
                        break;
                    case 1:
                        if (dist.x > 0)
                            Swipe();
                        break;
                    case 2:
                        if (dist.y > 0)
                            Swipe();
                        break;
                    case 3:
                        if (dist.y < 0)
                            Swipe();
                        break;
                    default:
                        break;
                }
            }
            else
                dist = Vector3.zero;
        }
    }
    public void StartAnims() { ui.SetTrigger("Start"); Invoke("StartGame", 3f); }
    public void StartGame() { game.SetActive(true); }
    public void Swipe() 
    {
        Destroy(arrow,1); 
        arrow.GetComponent<Animator>().SetTrigger("Correct");
        int pose = Random.Range(0,photos.Length);

        woman.SetTrigger("Pose_" + pose);
        Instantiate(photos[pose], photoPos);
        ui.SetTrigger("Flash");

        SetSwipeable(false);

        swipeCount++;
    }
    public static void SetSwipeable(bool value) { swipeable = value; }
    public static void DirectionToSwipe(int value) { swipeDir = value; }
    public static void GetSwipableObject(GameObject value) { arrow = value; }
    public void Restart() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }


}
