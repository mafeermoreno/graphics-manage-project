using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //Dirección de la bola de fuego
    private Vector2 moveDirection;
    //Velocidad de disparo de la bola de fuego
    private float moveSpeed;

    //Cuando el objeto se activa, tardará 3 segundos en destruirse
    private void OnEnable()
    {
        FireballPool.fireballPoolInstance.BulletActivated();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); 
        if(viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            if (gameObject.activeInHierarchy) 
            {
                gameObject.SetActive(false);
            }
        }
    }

    //Establecer la dirección a la que se moverá la bola de fuego
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    //Cuando el objeto se desactiva, cancela el llamado a Destroy
    private void OnDisable()
    {
        CancelInvoke();
        FireballPool.fireballPoolInstance.BulletDeactivated(); 
    }
}