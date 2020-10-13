using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public StartCoOp spScreen;
    bool alreadySplit = false;

    // Start is called before the first frame update
    void Start()
    {
        cam1.rect = new Rect(0, 0, 1f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (spScreen.addedPlayer && !alreadySplit)
        {
            cam2 = spScreen.playerHolder.GetComponentInChildren<Camera>();
            cam1.rect = new Rect(0, 0, .5f, 1);
            cam2.rect = new Rect(.5f, 0, .5f, 1);


            alreadySplit = true;
        }
    }
}
