using UnityEngine;

public class MoveKnife : MonoBehaviour
{
    Vector3 startPos, currPos;
    [SerializeField] float speed;
    float[] layerHeights = new float[3];
    private void Start()
    {
        layerHeights[0] = 0.25f;
        layerHeights[1] = 0.211f;
        layerHeights[2] = 0.16f;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;
        else if (Input.GetMouseButton(0))
        {
            currPos = Input.mousePosition;
            float dist = currPos.x - startPos.x;
            transform.Translate(dist * Time.deltaTime * speed, 0, 0);
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            startPos = Vector3.zero;     
            currPos = Vector3.zero;     
        }
        if (transform.localPosition.x <= -1.05f)
        {
            GameManager.gameManager.NextLayer();
            transform.localPosition = new Vector3(0.12f, layerHeights[GameManager.gameManager.currentLayer], transform.localPosition.z);
        }
        else if (transform.localPosition.x > 0)
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }
}
