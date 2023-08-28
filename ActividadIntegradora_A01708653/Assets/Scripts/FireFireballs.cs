using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireballs : MonoBehaviour
{
    [SerializeField]
    private int fireballsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 fireballMoveDirection;

    private float patternChangeInterval = 10f; // Cambia de patrón cada 10 segundos
    private float patternSwitchTime;

    private int currentPattern = 0; // 0: FireCirclePattern, 1: FireLotusPattern, 2: FireStarPattern

    private void Start()
    {
        // Cancela la invocación anterior si existe
        CancelInvoke("ExecutePattern");

        patternSwitchTime = Time.time + patternChangeInterval;
        InvokeRepeating("ExecutePattern", 0f, 2f);
    }
    private void OnDisable()
    {
        CancelInvoke("ExecutePattern");
    }

    private void ExecutePattern()
    {
        if (Time.time >= patternSwitchTime)
        {
            currentPattern = (currentPattern + 1) % 3;
            patternSwitchTime = Time.time + patternChangeInterval;
        }

        switch (currentPattern)
        {
            case 0:
                FireCirclePattern();
                break;
            case 1:
                FireLotusPattern();
                break;
            case 2:
                FireStarPattern();
                break;
            default:
                break;
        }
    }


    // Patrón de círculo de fuego
    private void FireCirclePattern()
    {
        float angleStep = (endAngle - startAngle) / fireballsAmount;
        float angle = startAngle;

        for (int i = 0; i < fireballsAmount; i++)
        {
            // Cálculo de la dirección de la bola de fuego
            float firDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float firDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 firMoveVector = new Vector3(firDirX, firDirY, 0f);
            Vector2 firDir = (firMoveVector - transform.position).normalized;

            GameObject fir = FireballPool.fireballPoolInstance.GetFireball();
            fir.transform.position = transform.position;
            fir.transform.rotation = transform.rotation;
            fir.SetActive(true);
            fir.GetComponent<Fireball>().SetMoveDirection(firDir);

            angle += angleStep;
        }
    }

    // Patrón de loto de fuego
    private void FireLotusPattern()
    {
        int petalsCount = 12; // Cambia el número de pétalos a 12
        float angleIncrement = 360f / petalsCount;
        Vector2 forward = transform.up;

        for (int i = 0; i < petalsCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0f, 0f, angleIncrement * i) * forward;
            FireBullet(direction);

            // Agrega balas adicionales en la misma dirección para hacerlo más denso
            for (int j = 1; j <= 3; j++)
            {
                Vector2 directionOffset = Quaternion.Euler(0f, 0f, angleIncrement * i) * forward * (j * 0.15f); // Ajusta el factor de multiplicación para separar las balas
                FireBullet(direction + directionOffset);
            }
        }
    }

    // Patrón de estrella de fuego
    private void FireStarPattern()
    {
        int spikesCount = 5;
        int verticesPerSpike = 2; // Cambio el número de vértices por pico a 2
        float angleIncrement = 360f / (spikesCount * verticesPerSpike); // Ajusto el incremento angular
        Vector2 forward = transform.up;

        for (int i = 0; i < spikesCount * verticesPerSpike; i++)
        {
            Vector2 direction = Quaternion.Euler(0f, 0f, angleIncrement * i) * forward;
            FireBullet(direction);

            // Añadir balas adicionales entre los vértices para formar los picos
            if (i % verticesPerSpike == 0)
            {
                for (int j = 1; j <= verticesPerSpike - 1; j++)
                {
                    Vector2 directionOffset = Quaternion.Euler(0f, 0f, angleIncrement * i) * forward * (j * 0.15f); // Ajusta el factor de multiplicación
                    FireBullet(direction + directionOffset);
                }
            }
        }
    }

    // Método para disparar una bala de fuego
    private void FireBullet(Vector2 direction)
    {
        GameObject fir = FireballPool.fireballPoolInstance.GetFireball();

        if (fir != null)
        {
            fir.transform.position = transform.position;
            fir.transform.rotation = transform.rotation;
            fir.SetActive(true);
            fir.GetComponent<Fireball>().SetMoveDirection(direction);
        }
    }
}

