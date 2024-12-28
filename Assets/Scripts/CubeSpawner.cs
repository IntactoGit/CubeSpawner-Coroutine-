using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;        // Префаб кубика
    [SerializeField] private int _gridSize = 20;            // Размер сетки (20x20)
    [SerializeField] private float _spawnInterval = 0.04f; // Интервал спавна кубиков

    private GameObject[,] cubes;         // Хранение всех кубиков

    private void Start()
    {
        cubes = new GameObject[_gridSize, _gridSize];
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        for (int i = 0; i < _gridSize; i++)
        {
            for (int j = 0; j < _gridSize; j++)
            {
                // Создаем кубик
                var position = new Vector3(j, 0, -i);
                var cube = Instantiate(_cubePrefab, position, Quaternion.identity);
                cubes[j, i] = cube;

                // Ждем перед спавном следующего кубика
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    public GameObject[,] GetCubes()
    {
        return cubes;
    }
}