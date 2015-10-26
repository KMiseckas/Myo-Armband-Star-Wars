using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    [Header("Lightning Settings")]
    public GameObject lightningStart;
    public GameObject pointer;
    public GameObject lightningBoltPrefab;
    public GameObject powerPrefabParent;
    public int lightningBoltCount = 3;
    public float boltChangeRate = 0.2f;
    public float positionOffsetRange = 0.3f;

    private bool boltsActive = false;

    private int nextBoltToUpdate = 0;

    private LineRenderer[] lineRenderers;
    private GameObject[] boltPrefabs;

    void Start()
    {
        boltPrefabs = new GameObject[lightningBoltCount];
        lineRenderers = new LineRenderer[lightningBoltCount];

        for (int i = 0; i < lightningBoltCount; i++)
        {
            GameObject newBolt = Instantiate (lightningBoltPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newBolt.transform.parent = lightningStart.transform;
            newBolt.transform.position = lightningStart.transform.position;
            boltPrefabs[i] = newBolt;

            lineRenderers[i] = newBolt.GetComponent<LineRenderer> ();
            lineRenderers[i].enabled = false;
        }

        MyoPoseCheck.onUseLightning += UseLightning;
        MyoPoseCheck.onStopLightning += StopUsingLightning;
    }

    void Update()
    {
        if(boltsActive)
        {
            foreach(GameObject bolt in boltPrefabs)
            {
                bolt.transform.LookAt (pointer.transform.position);
            }
        }
    }

    void UpdateBolts()
    {
        float randomOffsetY = Random.RandomRange (-positionOffsetRange, positionOffsetRange);
        float randomOffsetX = Random.RandomRange (-positionOffsetRange, positionOffsetRange);

        lineRenderers[nextBoltToUpdate].SetPosition (0, new Vector3 (randomOffsetX, randomOffsetY, 0));
        lineRenderers[nextBoltToUpdate].SetPosition (1, new Vector3 (0, 0, 5));

        nextBoltToUpdate++;
        if(nextBoltToUpdate >= lightningBoltCount)
        {
            nextBoltToUpdate = 0;
        }

        Invoke ("UpdateBolts", boltChangeRate);
    }

    void InitialSetUp()
    {
        foreach(LineRenderer bolt in lineRenderers)
        {
            float randomOffsetY = Random.RandomRange (-positionOffsetRange, positionOffsetRange);
            float randomOffsetX = Random.RandomRange (-positionOffsetRange, positionOffsetRange);

            bolt.SetPosition (0, new Vector3 (randomOffsetX, randomOffsetY, 0));
            bolt.SetPosition (1, new Vector3 (0, 0, 5));

            bolt.enabled = true;
        }
    }

    void UseLightning()
    {
        //Debug.Log ("LIGHTNING ACTIVATED -------------------");

        if(!boltsActive)
        {
            boltsActive = true;

            for (int i = 0; i < lightningBoltCount; i++)
            {
                lineRenderers[i].enabled = true;
            }

            InitialSetUp ();

            CancelInvoke ("UpdateBolts");
            Invoke ("UpdateBolts", boltChangeRate);
        }


    }

    void StopUsingLightning()
    {
        //Debug.Log ("LIGHTNING STOPPED ---------------------");

        CancelInvoke ("UpdateBolts");

        for (int i = 0; i < lightningBoltCount; i++)
        {
            lineRenderers[i].enabled = false;
        }

        boltsActive = false;
    }

}
