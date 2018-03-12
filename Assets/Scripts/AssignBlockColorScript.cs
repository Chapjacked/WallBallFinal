using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignBlockColorScript : MonoBehaviour {

    //Array to hold all blocks
    public GameObject[] BlocksToAssignColor;

    //Array to hold all materials to assign to blocks
    public Material[] MaterialsToAssignBlocks;

    //Number to assign which material to assign to the blocks
    [HideInInspector]
    public int fNumMaterialToAssign;

    //Renderer for the block that's being assigned to set color
    public Renderer BlockRenderer;

	// Use this for initialization
	void Start () {

        StartCoroutine(AssignColorsToBlocksFunction());
        
	}

    public IEnumerator AssignColorsToBlocksFunction()
    {
        //for loop to apply materials to each block in the BlocksToAssignColor Array
        for (int i = 0; i < BlocksToAssignColor.Length; i++)
        {
            //Randomly pick material to assign
            fNumMaterialToAssign = Random.Range(0, MaterialsToAssignBlocks.Length);

            //Assign the new material to the current block in the array
            BlockRenderer = BlocksToAssignColor[i].GetComponent<MeshRenderer>();

            BlockRenderer.sharedMaterial = MaterialsToAssignBlocks[fNumMaterialToAssign];

        }

        yield return true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
