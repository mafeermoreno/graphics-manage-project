using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    //Nos dice la velocidad en la cual movernos
    private float moveSpeed;
    //Nos dice en qué sentido movernos
    private bool moveRight;

    // Mantén constante la posición Z deseada
    private float targetZ = -4.13f;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Si la figura en su eje x es mayor que 7, debe dejar de moverse a la derecha
        if (transform.position.x >= 7f)
        {
            moveRight = false;
        }
        // En cambio, si en su eje x es menor a -7, tiene que moverse a la derecha
        else if (transform.position.x <= -7f)
        {
            moveRight = true;
        }

        float newXPosition = transform.position.x;

        // Si el movimiento es hacia la derecha, hay que moverse hasta 7, de acuerdo a la velocidad definida y su eje y
        if (moveRight)
        {
            newXPosition = Mathf.MoveTowards(newXPosition, 7f, moveSpeed * Time.deltaTime);
        }
        // Si el movimiento no es derecha, es entonces izquierda, entonces se mueve al lado contrario
        else
        {
            newXPosition = Mathf.MoveTowards(newXPosition, -7f, moveSpeed * Time.deltaTime);
        }

        // Mantén la posición Z constante
        Vector3 newPosition = new Vector3(newXPosition, transform.position.y, targetZ);

        transform.position = newPosition;
    }
}
