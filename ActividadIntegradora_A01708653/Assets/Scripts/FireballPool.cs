using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FireballPool : MonoBehaviour
{
    //Variable de instancia que puede acceder a la instancia de la clase FireballPool desde cualquier parte del código
    public static FireballPool fireballPoolInstance;

    [SerializeField]
    //Objeto que será plantilla para las bolas de fuego
    private GameObject pooledFireball;

    //[SerializeField]
    //public Text fireballCountText; // Referencia al componente Text del objeto de texto en la escena

    //Indica cuando no hay suficientes bolas de fuego en el grupo
    private bool notEnoughFireballsInPool = true;

    //Lista que almacena las bolas de fuego
    public List<GameObject> fireballs;

    // Evento que se invoca cuando cambia la cantidad de balas.
    public static Action OnBulletChanged;
    public static int bulletCount = 0;

    public delegate void BulletChangedDelegate();

    //Método que se llama cuando se despierta el objeto
    private void Awake()
    {
        fireballPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fireballs = new List<GameObject>();
    }

    //Método para obtener bolas de fuego. Si hay bolas inactivas, devuelve una de ellas, sino, crea una, la desactiva y la agrega
    public GameObject GetFireball()
    {
        if (fireballs.Count > 0)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                if (!fireballs[i].activeInHierarchy)
                {
                    return fireballs[i];
                }
            }
        }

        if (notEnoughFireballsInPool)
        {
            GameObject fir = Instantiate(pooledFireball);
            fir.SetActive(false);
            fireballs.Add(fir);
            return fir;
        }

        return null;
    }
    // Se llama cuando se activa una bala.
    public void BulletActivated()
    {
        // Incrementa el contador de balas.
        bulletCount++;
        // Invoca el evento para notificar el cambio.
        OnBulletChanged?.Invoke();
    }

    // Se llama cuando se desactiva una bala.
    public void BulletDeactivated()
    {
        // Decrementa el contador de balas.
        bulletCount--;
        // Invoca el evento para notificar el cambio.
        OnBulletChanged?.Invoke();
    }

    // Método para desactivar una bala
    public void Destroy(GameObject fireball)
    {
        fireball.SetActive(false);
        BulletDeactivated();
    }
}
