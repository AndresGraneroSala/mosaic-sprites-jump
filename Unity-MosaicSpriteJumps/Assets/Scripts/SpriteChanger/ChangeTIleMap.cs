using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[RequireComponent(typeof(Tilemap))]
public class ChangeTIleMap : MonoBehaviour
{
    private List<SpriteLoaded> spritesInTile= new List<SpriteLoaded>();
    
    private void Start()
    {
        StartCoroutine(ChangeMyTile());
    }


    void ChangeInSO()
    {
        
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);

            if (tilemap.HasTile(localPlace))
            {
                Tile localTile = tilemap.GetTile<Tile>(localPlace);
                
                SelectSprite selectSprite = new SelectSprite();



                SpriteLoaded spriteLoadedBefore = null;
                
                foreach (var spriteLoaded in spritesInTile)
                {
                    if (spriteLoaded.nameFile == localTile.name)
                    {
                        spriteLoadedBefore = spriteLoaded;
                    }
                }

                Sprite sprite=null;
                if (spriteLoadedBefore!=null)
                {
                    sprite = spriteLoadedBefore.sprite;
                }
                else
                {
                    sprite =
                        selectSprite.ChangeSprite($"Platforms/{localTile.name}.png");
                }
                
                
                
                
                //sprite.texture.format = TextureFormat.Alpha8;
                
                
                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.colliderType = Tile.ColliderType.Sprite;

                int targetScale = localTile.sprite.texture.width;
                

                float newScale = (float) targetScale /sprite.texture.width;
                
                Vector3 scale = new Vector3(newScale, newScale, 1f);
                Matrix4x4 newMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
                //tilemap.SetTransformMatrix(localPlace,newMatrix);
                
                
                tile.sprite = sprite;
                tilemap.SetTile(localPlace, tile);

            }
        }
    }


    public IEnumerator ChangeMyTile()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);

            if (tilemap.HasTile(localPlace))
            {
                Tile localTile = tilemap.GetTile<Tile>(localPlace);

                SelectSprite selectSprite = new SelectSprite();


                SpriteLoaded spriteLoadedBefore = null;

                foreach (var spriteLoaded in spritesInTile)
                {
                    if (spriteLoaded.nameFile == localTile.name)
                    {
                        spriteLoadedBefore = spriteLoaded;
                    }
                }

                Sprite sprite = null;
                if (spriteLoadedBefore != null)
                {
                    sprite = spriteLoadedBefore.sprite;
                }
                else
                {
                    yield return StartCoroutine(selectSprite.ChangeSpriteMultiplatform($"Platforms/{localTile.name}.png",
                        (spriteChanged) =>
                        {
                            sprite = spriteChanged;
                        }));
                }

                var tile = ScriptableObject.CreateInstance<Tile>();

                int targetScale = localTile.sprite.texture.width;


                float newScale = (float)targetScale / sprite.texture.width;

                Vector3 scale = new Vector3(newScale, newScale, 1f);
                Matrix4x4 newMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);


                tile.sprite = sprite;
                tilemap.SetTile(localPlace, tile);

                tilemap.SetTransformMatrix(localPlace, newMatrix);



            }
        }
    }


    [Serializable]
    class SpriteLoaded
    {
        public string nameFile;
        public Sprite sprite;
        
    }
    
}




