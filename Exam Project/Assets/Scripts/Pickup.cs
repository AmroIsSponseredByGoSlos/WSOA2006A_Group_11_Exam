using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public Vector3 CarryPoint;
    public GameObject CarryPointObject;
    public float RayLength;
    public TextMeshProUGUI PickupTxt;
    public GameObject PickupTxtObject;
    public bool Holding = false;
    public GameObject ObjectToDrop;
    public PlayerController playerController;
    public UI UiScript;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Holding)
        {
            ObjectToDrop.transform.forward = gameObject.transform.forward;
            ObjectToDrop.transform.position = CarryPoint;
        }
        PickupTxtObject.SetActive(false);
        if (playerController.BuffaloAbilityActive)
        {
            CarryPoint = CarryPointObject.transform.position;
            RaycastHit Hit;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * RayLength, Color.green);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, RayLength))
            {
                if (Hit.transform.gameObject.CompareTag("Liftable"))
                {
                    PickupTxtObject.SetActive(true);
                    if (Holding)
                    {
                        PickupTxt.text = "Press 'G' to drop the object";
                    }
                    else
                    {
                        PickupTxt.text = "Press 'F' to pick up the object";
                    }
                    if (Input.GetKey("f") && !Holding)
                    {
                        PickupObject(Hit.transform.gameObject);
                    }
                }
            }
        }      

        if (Input.GetKey("g") && Holding)
        {
            playerController.BuffaloAbilityActive = false;
            DropObject(ObjectToDrop);
            anim.SetBool("IsPushing", false);
        }
    }

    void PickupObject(GameObject PickedObject)
    {
        Holding = true;
        ObjectToDrop = PickedObject;
        anim.SetBool("IsPushing", true);
    }

    void DropObject(GameObject PickedObject)
    {
        Holding = false;
        UiScript.Reset();
    }
}
