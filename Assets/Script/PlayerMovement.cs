using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("===Component===")]
    Rigidbody2D _playerRb;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        F_MovePlayer();
    }

    private void F_MovePlayer() 
    {
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        //this.transform.position += new Vector3(hori, verti) * Time.deltaTime * 3f;

        _playerRb.velocity = new Vector2 (hori, verti) * 3f;
    }
}
