using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private static Color SELECTED_COLOR = new Color(.5f, .5f, .5f, 1.0f);
    private static Color UNSELECTED_COLOR = Color.white;

    private bool isSelected;
    private static Gem previousSelected = null;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isSelected = false;
    }

    private void Select()
    {
        isSelected = true;
        spriteRenderer.color = SELECTED_COLOR;
        previousSelected = gameObject.GetComponent<Gem>();
    }

    private void Unselect()
    {
        isSelected = false;
        spriteRenderer.color = UNSELECTED_COLOR;
        previousSelected = null;
    }

    private void OnMouseDown()
    {
        if (spriteRenderer.sprite == null)
        {
            return;
        }
        if (isSelected)
        {
            Unselect();
        }
        else
        {
            if (previousSelected == null)
            {
                Select();
            }
            else
            {
                if (IsSelectedGemAdjacent())
                {
                    SwapGem();
                    Unselect();
                }
                else
                {
                    previousSelected.GetComponent<Gem>().Unselect();
                    Select();
                }
            }
        }
    }

    private bool IsSelectedGemAdjacent()
    {
        Vector2[] adjacentDirections = new Vector2[] {
      Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

        List<GameObject> adjacentGems = new List<GameObject>();

        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            RaycastHit2D collidedObject = Physics2D.Raycast(transform.position, adjacentDirections[i]);
            if (collidedObject.collider != null)
            {
                adjacentGems.Add(collidedObject.collider.gameObject);
            }
        }

        if (adjacentGems.Contains(previousSelected.gameObject))
        {
            return true;
        }
        return false;
    }

    public void SwapGem()
    {
        Sprite tempSprite = previousSelected.spriteRenderer.sprite;
        previousSelected.spriteRenderer.sprite = spriteRenderer.sprite;
        spriteRenderer.sprite = tempSprite;
    }
}