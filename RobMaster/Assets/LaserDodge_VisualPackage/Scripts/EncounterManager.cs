using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    Transform[] encounters;
    public static bool inEncounter = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        foreach (var item in encounters)
        {
            if (Vector3.Distance(player.transform.position, item.transform.position) <= 5 && !item.GetComponent<EncounterAproaching>().GetDone())
            {
                inEncounter = true;
                break;
            }
            inEncounter = false;
        }
        if (inEncounter)
        {
            foreach (var item in encounters)
                item.GetComponent<EncounterAproaching>().SetSpeed(0);
        }
        else
        {
            foreach (var item in encounters)
                item.GetComponent<EncounterAproaching>().SetSpeed(item.GetComponent<EncounterAproaching>().GetSpeedValue());
        }
    }

    public void SetEncounterDone()
    {
        for (int i = 0; i < encounters.Length; i++)
        {
            if(encounters[i].GetComponent<EncounterAproaching>().GetDone() == false)
            {
                encounters[i].GetComponent<EncounterAproaching>().SetDone(true);
                break;
            }
        }
    }
}