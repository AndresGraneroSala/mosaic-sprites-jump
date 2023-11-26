using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( SpriteRenderer))]

public class ChangeSpriteStart : MonoBehaviour
{
    [SerializeField] private string nameFileSprite;

    enum PriorizeScale
    {
        none,
        scaleX,
        scaleY
    }

    [SerializeField] private PriorizeScale priorizeScale;
    
    
    // Start is called before the first frame update
    public void Start()
    {
        ChangeThisSprite();
    }


    public void ChangeThisSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        SelectSprite selectSprite = new SelectSprite();

        StartCoroutine(selectSprite.ChangeSpriteMultiplatform(nameFileSprite, (spriteChanged) =>
        {
            Sprite sprite = spriteChanged;
            ChangeScale(spriteRenderer,sprite.texture);
            spriteRenderer.sprite = sprite;
        }));

    }


    public void ChangeScale(SpriteRenderer spriteRenderer, Texture texture)
    {
        float widthScale = (float)spriteRenderer.sprite.texture.width/ texture.width ;
        float heightScale = (float)spriteRenderer.sprite.texture.height / texture.height;


        if (priorizeScale==PriorizeScale.none)
        {
            transform.localScale =
                new Vector3(widthScale * transform.localScale.x, heightScale * transform.localScale.y, 1);
        }

        if (priorizeScale==PriorizeScale.scaleX)
        {
            transform.localScale =
                new Vector3(widthScale * transform.localScale.x, widthScale * transform.localScale.y, 1);
        }
        
        if (priorizeScale==PriorizeScale.scaleY)
        {
            transform.localScale =
                new Vector3(heightScale * transform.localScale.x, heightScale * transform.localScale.y, 1);
        }
        
    }

}
