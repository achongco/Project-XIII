﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDetectionScript : MonoBehaviour {

    public int radiusLevel = 0;

    HashSet<GameObject> playersInRange;

    private void Start()
    {
        playersInRange = new HashSet<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            if (!playersInRange.Contains(collision.gameObject))
            {
                playersInRange.Add(collision.gameObject);
                transform.parent.parent.GetComponent<FloatingMineScript>().RadiusReport(radiusLevel, true);
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if (playersInRange.Contains(collision.gameObject))
            {
                playersInRange.Remove(collision.gameObject);
                transform.parent.parent.GetComponent<FloatingMineScript>().RadiusReport(radiusLevel, false);
            }
    }

    public bool detectsPlayer()
    {
        return playersInRange.Count != 0;
    }
}
