using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    public Transform snapSlotTransform;
    public float placementTime = 1f;
    private bool moving;
    public bool snapped;
    Camera viewCam;
    private float startPosX;
    private float startPosY;

    private void Start()
    {
        snapSlotTransform = FindObjectOfType<SnapSlot>().transform;
        viewCam = Camera.main;
    }

    private void Update()
    {
        if (!snapped)
        {
            if (moving)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = viewCam.ScreenToWorldPoint(mousePos);
                // dragging
                // baslangic vectorlerini cikarmazsan her tikladiginda kilicin ortasi hep mouse pos'a isinlaniyor
                // koddan anlasilacagi uzere kilicin transform.position'u mousenin oldugu yer oluyor
                transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, transform.position.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!snapped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = viewCam.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - transform.position.x;
                startPosY = mousePos.y - transform.position.y;
                moving = true;

            }
        }
    }

    private void OnMouseUp()
    {
        if (!snapped)
        {
            moving = false;
            // makin square 0.6 unit = .6 * .6 = .36
            if (Vector3.SqrMagnitude(transform.position - snapSlotTransform.position) < .5625f)
            {
                print(Vector3.SqrMagnitude(transform.position - snapSlotTransform.position));
                // make these with coroutines (transform and rotation)
                //transform.position = snapSlotTransform.position;
                //transform.rotation = snapSlotTransform.rotation;
                StartCoroutine(AnimateSwordPosition());
                transform.parent = snapSlotTransform;
                snapped = true;
            }
        }
    }

    IEnumerator AnimateSwordPosition()
    {
        float placementSpeed = 1f / placementTime;
        float percent = 0;
        Vector3 initialPos = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (percent <= 1)
        {
            percent += Time.deltaTime * placementSpeed;
            transform.position = Vector3.Lerp(initialPos, snapSlotTransform.position, percent);
            transform.rotation = Quaternion.Lerp(initialRotation, snapSlotTransform.rotation, percent);
            yield return null;
        }
        //transform.position = snapSlotTransform.position;
        //transform.rotation = snapSlotTransform.rotation;
    }
}
