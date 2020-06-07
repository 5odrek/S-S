using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zorder : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer mySprite;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mySprite.sortingOrder = -(int)Camera.main.WorldToScreenPoint(this.transform.position).y;
    }
}
