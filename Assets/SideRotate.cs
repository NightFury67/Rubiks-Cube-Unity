using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRotate : MonoBehaviour
{
    [Header("Settings")]
    public Transform cubeParent;      
    public Transform cubeOrientation; 
    public float rotationDuration = 0.3f;
    public float pieceSize = 1f;

    private bool isRotating = false;

    void Update()
    {
        if (isRotating) return;

        if (Input.GetKeyDown(KeyCode.U))
            RotateFace(cubeOrientation.TransformDirection(Vector3.up), 1, 1);
        if (Input.GetKeyDown(KeyCode.Y))
            RotateFace(cubeOrientation.TransformDirection(Vector3.up), 1, -1);

        if (Input.GetKeyDown(KeyCode.R))
            RotateFace(cubeOrientation.TransformDirection(-Vector3.forward), 1, 1);
        if (Input.GetKeyDown(KeyCode.E))
            RotateFace(cubeOrientation.TransformDirection(-Vector3.forward), 1, -1);

        if (Input.GetKeyDown(KeyCode.L))
            RotateFace(cubeOrientation.TransformDirection(Vector3.right), -1, -1);
        if (Input.GetKeyDown(KeyCode.K))
            RotateFace(cubeOrientation.TransformDirection(Vector3.right), -1, 1);
    }

    public void RotateFace(Vector3 axis, int layer, int direction)
    {
        if (isRotating) return;
        StartCoroutine(DoRotation(axis, layer, direction));
    }

    private IEnumerator DoRotation(Vector3 axis, int layer, int direction)
    {
        isRotating = true;

        List<Transform> pieces = GetLayerPieces(axis, layer);

        GameObject pivot = new GameObject("pivot");
        pivot.transform.position = cubeParent.position;

        foreach (Transform p in pieces)
            p.SetParent(pivot.transform);

        float angle = 90f * direction;
        float elapsed = 0f;
        Quaternion startRot = pivot.transform.rotation;
        Quaternion endRot = Quaternion.AngleAxis(angle, axis) * startRot;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / rotationDuration);
            pivot.transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        pivot.transform.rotation = endRot;

        foreach (Transform p in pieces)
        {
            p.SetParent(cubeParent);
            p.localPosition = new Vector3(
                Mathf.Round(p.localPosition.x),
                Mathf.Round(p.localPosition.y),
                Mathf.Round(p.localPosition.z)
            );
        }

        Destroy(pivot);
        isRotating = false;
    }

    private List<Transform> GetLayerPieces(Vector3 axis, int layer)
    {
        List<Transform> result = new List<Transform>();

        foreach (Transform piece in cubeParent)
        {
            float coord = Vector3.Dot(piece.position - cubeParent.position, axis) / pieceSize;

            if (Mathf.RoundToInt(coord) == layer)
                result.Add(piece);
        }

        return result;
    }
}