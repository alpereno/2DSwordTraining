using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    // The codes here the solution of the given task

    Camera viewCam;                 // To convert from screen to World point
    SnapSlot snapSlot;
    Transform snapSlotTransform;

    [SerializeField] private float placementTime = .28f;    // placement time(sec) when it enters within a certain distance 
    [SerializeField] private bool snapped;
    bool moving;
    
    float startPosX;
    float startPosY;

    private void Start()
    {
        snapSlot = FindObjectOfType<SnapSlot>();
        snapSlotTransform = snapSlot.transform;                
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
                // if I don't substract the start positions, the middle of the sword always teleports to mouse positions every time
                // the position of the sword you hold, becomes the position of the mouse (you can hold anywhere of the sword)
                transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, transform.position.z);
            }
        }
    }

    private void OnMouseDown()
    {
        // if not snapped, registers the starting position and starts the drag process
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
            // I've used to Square not SquareRoot because square root operation is fairly expensive
            // We don't need the actual distance

            // 0.75 unit = .75 * .75 = .5625
            if (Vector3.SqrMagnitude(transform.position - snapSlotTransform.position) < .5625f)
            {
                StartCoroutine(AnimateSwordPosition());     // To make the action smooth. it comes slowly to hand (duration can be changed)
                transform.parent = snapSlotTransform;       // This game object becomes child
                snapped = true;                             // To disable relocation

                snapSlot.SwordSnap(gameObject); ;
            }
        }
    }

    // the sword slowly comes into the hand and turns
    // placement time can changeable(sec) (high values slow down the process)   if you make 1 to "placementTime" it will take 1 sec
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
    }
}
