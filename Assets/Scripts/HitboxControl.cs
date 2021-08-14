using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxControl : MonoBehaviour
{
    private GameObject knightObject;
    private PlayerController knight;
    // Start is called before the first frame update
    void Start()
    {
        knightObject = transform.parent.gameObject;
        knight = knightObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(knight.faceDirection,1,1);
    }
}
