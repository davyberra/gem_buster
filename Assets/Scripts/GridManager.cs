using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public int rows, columns;
    private GameObject[,] gems;

    public List<Sprite> gemSprites = new List<Sprite>();

    public GameObject gem;

    void Start()
    {
        instance = GetComponent<GridManager>();
        Vector2 gemDimensions = gem.GetComponent<SpriteRenderer>().bounds.size;
        GenerateGrid(gemDimensions.x, gemDimensions.y);
    }

    private void GenerateGrid(float gemWidth, float gemHeight)
    {
        gems = new GameObject[columns, rows];

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                float xPosition = transform.position.x + (gemWidth * x);
                float yPosition = transform.position.y + (gemHeight * y);

                GameObject newGem = Instantiate(
                  gem,
                  new Vector3(xPosition, yPosition, 0),
                  gem.transform.rotation
                );

                gems[x, y] = newGem;

                newGem.transform.parent = transform;
                newGem.GetComponent<SpriteRenderer>().sprite = gemSprites[Random.Range(0, gemSprites.Count)];
            }
        }
    }

    void Update()
    {
    }
}