using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject doorPrefab;

    [Header("Materials")]
    public Material rayCastHitIndicatorMaterial;
    public Material defaultMaterial;

    [Header("LayerMask")]
    public LayerMask rayCastLayerFilter;

    [Header("Settings")]
    [Tooltip("Controls which directions the ray shoots to")]
    public Vector3 rayDirection;

    [Tooltip("Toggles showing the ray")]
    public bool DrawRayEnabled = false;

    [Tooltip("Stores the room from which to start the ray from")]
    public Transform room;

    [Tooltip("Stores the last object hit")]
    public MeshRenderer lastHitObjectMesh;

    private void Awake()
    {
        room = GameObject.Find("Room").transform;
        rayDirection = room.forward * 2.5f;
    }

    //Send a ray from the middle of the GameObject named "Room" to a direction to hit at a specefied LayerMask
    public void TestRayCast()
    {
        RaycastHit hit;

        if(Physics.Raycast(room.position + Vector3.up * 0.725f, rayDirection, out hit, rayCastLayerFilter))
        {
            //Prints in the console what object the ray has hit on the layermask
            Debug.Log($"You Have Hit an object named {hit.collider.name}");

            //Spawns a door
            GameObject newDoor = Instantiate(doorPrefab,hit.point, hit.collider.transform.rotation, hit.collider.transform.parent);
            newDoor.transform.position += newDoor.transform.forward / 20;

            //Changes the color of the hit object
            MeshRenderer hitObjectMesh = hit.collider.transform.GetComponent<MeshRenderer>();
            hitObjectMesh.material = rayCastHitIndicatorMaterial;

            //makes sure to only have 1 hit object shown at a time
            if (lastHitObjectMesh != null)
            {
                lastHitObjectMesh.material = defaultMaterial;
            }

            lastHitObjectMesh = hitObjectMesh;
        }
        
    }

    //Toggles showing the ray
    public void ToggleDrawRayCastTest()
    {
        DrawRayEnabled = !DrawRayEnabled; 
    }

    //draws the ray
    void DrawRayCastTest()
    {
        Debug.DrawRay(room.position + Vector3.up * 0.725f, rayDirection, Color.black);
    }

    void Update()
    {
        if(DrawRayEnabled)
        {
            DrawRayCastTest();
        }
    }

    
}
