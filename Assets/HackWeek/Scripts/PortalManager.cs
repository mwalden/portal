using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject sponza;
    public Text text;
    public Text distance;
    public Material[] sponzaMaterials;
    public bool insideBuilding;
    public bool colliderExited;
    public bool enabledCalled;
    // Start is called before the first frame update
    void Start()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
    }

    private void Update()
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position);
        distance.text = camPositionInPortalSpace.y.ToString() + " :: " + sponzaMaterials[0].GetInt("_StencilComp") + " :: " + enabledCalled;
    }

    private void OnEnable()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
        for (int x = 0; x < sponzaMaterials.Length; x++)
        {
            enabledCalled = true;
            sponzaMaterials[x].SetInt("_StencilComp", 3);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (colliderExited)
        {
            text.text = "Entered";
        }
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position);
        if (camPositionInPortalSpace.y < .5f )
        {
            text.text = camPositionInPortalSpace.y + " :: setting to Always" + sponzaMaterials[0].GetInt("_StencilComp");
            //disable stencil test
            for (int x = 0; x < sponzaMaterials.Length;x++)
                sponzaMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Always);

        }
        else
        {
            text.text = camPositionInPortalSpace.y + " :: setting to equal" + sponzaMaterials[0].GetInt("_StencilComp");
            for (int x = 0; x < sponzaMaterials.Length; x++)
            {
                sponzaMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliderExited = true;
        text.text = "Exited";
    }


}
