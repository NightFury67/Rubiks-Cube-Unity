using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shuffel : MonoBehaviour
{
    public SideRotate sideRotate;
    public Button shuffleButton;
    public int shuffleMoves = 20;

    void Start()
    {
        shuffleButton.onClick.AddListener(StartShuffle);
    }

    void StartShuffle()
    {
        StartCoroutine(DoShuffle());
    }

    IEnumerator DoShuffle()
    {
        shuffleButton.interactable = false;

        Vector3[] axes = new Vector3[]
        {
            sideRotate.cubeOrientation.TransformDirection(Vector3.up),
            sideRotate.cubeOrientation.TransformDirection(-Vector3.forward),
            sideRotate.cubeOrientation.TransformDirection(Vector3.right)
        };

        int[] layers = { -1, 1 };
        int[] directions = { -1, 1 };

        for (int i = 0; i < shuffleMoves; i++)
        {
            Vector3 axis = axes[Random.Range(0, axes.Length)];
            int layer = layers[Random.Range(0, layers.Length)];
            int direction = directions[Random.Range(0, directions.Length)];

            sideRotate.RotateFace(axis, layer, direction);

            yield return new WaitForSeconds(sideRotate.rotationDuration + 0.05f);
        }

        shuffleButton.interactable = true;
    }
}