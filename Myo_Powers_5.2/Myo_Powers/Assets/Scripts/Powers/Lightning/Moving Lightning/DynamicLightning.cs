using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicLightning : MonoBehaviour
{

    [Header ("Lightning Mesh")]
    public GameObject lightningBoltMesh;
    public Transform parent;

    [Header ("Lightning Target")]
    public Transform target;

    [Header ("Lightning Bolt Settings")]
    public int numberOfSplits = 3;
    public float maxSplitOffsetZ = 0.2f;
    public float maxSplitOffsetXY = 0.6f;

    [Header ("Safe Guard For While Loop")]
    public int maxWhileLoops = 1000;

    private int maxLines;

    private List<BoltLine> newLines = new List<BoltLine> ();

    void Start()
    {
        maxLines = (int) Mathf.Pow (2f, numberOfSplits);
        //Debug.Log (maxLines);

        newLines.Add (new BoltLine (transform.position, target.position));

        int loops = 0;

        while(newLines.Count < maxLines)
        {
            //Safe guard, incase something goes wrong and while loop would starts looping endlessly
            loops++;
            if(loops > maxWhileLoops)
            {
                Debug.LogWarning ("Infinite While Loop During Lightning Bolt Creation !!!!");
                break;
            }

            List<BoltLine> tempNewLines = new List<BoltLine> ();

            foreach (BoltLine line in newLines)
            {
                //Get middle point node of the current line start and end nodes
                Vector3 midNode = (line.GetStartNode () + line.GetEndNode ()) / 2;

                //Move the middle point node by a random offset value
                midNode.x += Random.Range (-maxSplitOffsetXY, maxSplitOffsetXY);
                midNode.y += Random.Range (-maxSplitOffsetXY, maxSplitOffsetXY);
                midNode.z += Random.Range (-maxSplitOffsetZ, maxSplitOffsetZ);

                //Create 2 new boltlines out of this single split and add to list of newLines
                tempNewLines.Add (new BoltLine (line.GetStartNode (), midNode));
                tempNewLines.Add (new BoltLine (midNode, line.GetEndNode ()));
            }

            newLines.Clear ();
            newLines = tempNewLines;

            //Debug.Log ("Looping bolt generation");
        }

        foreach(BoltLine line in newLines)
        {
            GameObject newBoltLine = Instantiate (lightningBoltMesh, line.GetStartNode (), Quaternion.identity) as GameObject;
            newBoltLine.transform.parent = parent;

            newBoltLine.GetComponent<BoltLineUpdate> ().SetBoltLine (line.GetStartNode (), line.GetEndNode ());
        }

        //Debug.Log (newLines.Count + "   " + maxLines);

    }
}
