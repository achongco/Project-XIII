﻿using UnityEngine;
using System.Collections;

public class AttackRangeDetect : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && col.gameObject.GetComponent<PlayerProperties>().alive)
            transform.parent.GetComponent<Enemy>().SetAttackInRange(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && !transform.parent.FindChild("Exit Attack Range") && col.gameObject.GetComponent<PlayerProperties>().alive)
            transform.parent.GetComponent<Enemy>().SetAttackInRange(false);
    }
}
