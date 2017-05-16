using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloidSpawner : MonoBehaviour {
    public GameObject floid;
    ArrayList floids;

    public float awareRadius;
    public float dangerRadius;

	// Use this for initialization
	void Start () {
        floids = new ArrayList();
        for(int i = 0; i < 10; i++)
        {
            Vector2 vic = Random.insideUnitCircle * 10;
            floids.Add(Instantiate(floid, transform.position + new Vector3(vic.x, 2, vic.y), Quaternion.identity));
        }
        foreach(GameObject g in floids)
        {
            g.GetComponent<Floid>().mama = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (var i = 0; i < floids.Count; i++)
        {

            GameObject iGO = (floids[i] as GameObject);
            Vector3 evade = Vector3.zero;
            for (var j = 0; j < floids.Count; j++)
            {
                if (i != j)
                 {
                    GameObject jGO = (floids[j] as GameObject);
                    Vector3 connectingVector = jGO.transform.position - iGO.transform.position;
                    if (connectingVector.magnitude <= dangerRadius)
                    {
                        evade -= connectingVector;
                    }

                }
            }
            iGO.GetComponent<Floid>().avoid = evade;

        }
	}

    public void DestroyFloid(GameObject victim)
    {
        floids.Remove(victim);
        Destroy(victim);
    }
}
