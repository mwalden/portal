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
    public List<Material> wallMaterials;
    public GameObject[] walls;
    public GameObject  crowd;
    public bool enabledCalled;
    // Start is called before the first frame update
    void Start()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
        foreach (GameObject go in walls)
        {
            Material[] material = go.GetComponent<Renderer>().sharedMaterials;
            wallMaterials.AddRange(material);
        }

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

        for (int x = 0; x < wallMaterials.Count; x++)
        {
            wallMaterials[x].SetInt("_StencilComp", 3);
        }



    }


    private void OnTriggerEnter(Collider other)
    {
            text.text = "Entered";
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(mainCamera.transform.position);
        if (camPositionInPortalSpace.y < .5f )
        {
            if (!crowd.activeSelf)
                crowd.SetActive(true);
            text.text = camPositionInPortalSpace.y + " :: setting to Always" + sponzaMaterials[0].GetInt("_StencilComp");
            //disable stencil test
            for (int x = 0; x < sponzaMaterials.Length;x++)
                sponzaMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Always);

            for (int x = 0; x < wallMaterials.Count; x++)
            {
                wallMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Always);
            }

        }
        else
        {
            text.text = camPositionInPortalSpace.y + " :: setting to equal" + sponzaMaterials[0].GetInt("_StencilComp");
            for (int x = 0; x < sponzaMaterials.Length; x++)
            {
                sponzaMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
            for (int x = 0; x < wallMaterials.Count; x++)
            {
                wallMaterials[x].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "Exited";
    }


}
