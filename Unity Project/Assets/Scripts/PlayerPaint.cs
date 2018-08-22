using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour {

    public GameObject paint;
    public Camera mainCamera;
    public float paintCooldown;
    public float paintDuration;
    public float paintSize;

    float timer;
    Ray mouseRay;
    RaycastHit rayHit;
    int layerMask;
    GameObject currentPaint;

    private void Start()
    {
        // Ignore collisions on every layer except layer 9 (background layer)
        layerMask = 1 << 9;

        timer = paintCooldown;
    }

    private void Update()
    {
        //Keep timer up to date
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse1) && timer >= paintCooldown)
        {
            // Raycast from mouse to background layer
            mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out rayHit, Mathf.Infinity, layerMask))
            {
                Quaternion objectRotation = Quaternion.identity;
                Vector3 objectPosition = gameObject.transform.position;
                Vector3 transformVector = rayHit.point - objectPosition;

                // Position according to camera where the mouse was clicked
                Vector3 mouseClickPos = rayHit.point;

                // Get changes in positions for next calculation
                float deltaZ = mouseClickPos.z - objectPosition.z;
                float deltaX = mouseClickPos.x - objectPosition.x;

                // Use inverse tangent to figure out angle between mouse click and player
                float angle = Mathf.Atan(deltaX / deltaZ) * 180 / Mathf.PI;

                // Check for arctan(0/0)
                angle = float.IsNaN(angle) ? 270 : angle;
                // Fix orientation
                angle = deltaX > 0 ? angle - 90 : angle + 90;

                objectRotation.eulerAngles = new Vector3(90, angle, 0);
                objectPosition.y = 26.7f;

                //Create and resize paint
                currentPaint = Instantiate(paint, objectPosition, objectRotation);
                currentPaint.transform.localScale += new Vector3(paintSize, 0, 0);

                //Move paint
                transformVector.y = 0;
                transformVector.Normalize();
                transformVector *= (paintSize / 2);
                currentPaint.transform.Translate(transformVector, Space.World);
                
                StartCoroutine(DestroyPaint(currentPaint));
            }

            timer = 0;
        }
    }

    private IEnumerator DestroyPaint(GameObject paintToDestroy)
    {
        for (float destroyTimer = 0; destroyTimer < paintDuration; destroyTimer += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(paintToDestroy);
    }
}
