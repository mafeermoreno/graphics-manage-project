using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCounter : MonoBehaviour
{
    // Referencia al componente TextMeshProUGUI para mostrar el contador de balas.
    public TextMeshProUGUI CountText;

    // Se llama cuando este objeto se activa.
    private void OnEnable()
    {
        // Se suscribe al evento OnBulletChanged para llamar a UpdateBullet cuando cambia el contador de balas.
        FireballPool.OnBulletChanged += UpdateBullet;
    }

    // Se llama cuando este objeto se desactiva.
    private void OnDisable()
    {
        // Se desuscribe del evento OnBulletChanged para evitar llamadas innecesarias a UpdateBullet.
        FireballPool.OnBulletChanged -= UpdateBullet;
    }

    // Método para actualizar el texto del contador de balas.
    private void UpdateBullet()
    {
        // Actualiza el texto para mostrar la cantidad actual de balas.
        CountText.text = "Cantidad de balas: " + FireballPool.bulletCount;
    }
}