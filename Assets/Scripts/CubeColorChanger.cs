using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] private float _changeInterval = 0.2f;     // Интервал изменения цвета кубика
    [SerializeField] private float _colorTransitionTime = 0.5f; // Время перехода цвета

    private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _cubeSpawner = FindObjectOfType<CubeSpawner>();
    }

    public void OnChangeColorsButtonClick()
    {
        StartCoroutine(ChangeColors());
    }

    private IEnumerator ChangeColors()
    {
        // Выбираем случайный цвет для всех кубиков
        var targetColor = Random.ColorHSV();

        var cubes = _cubeSpawner.GetCubes();

        for (int i = 0; i < cubes.GetLength(0); i++)
        {
            for (int j = 0; j < cubes.GetLength(1); j++)
            {
                GameObject cube = cubes[j, i];
                if (cube != null)
                {
                    StartCoroutine(ChangeCubeColor(cube, targetColor));
                }
                yield return new WaitForSeconds(_changeInterval);
            }
        }
    }

    private IEnumerator ChangeCubeColor(GameObject cube, Color targetColor)
    {
        var renderer = cube.GetComponent<Renderer>();
        var startColor = renderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < _colorTransitionTime)
        {
            // Плавное изменение цвета
            renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / _colorTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor;
    }
}