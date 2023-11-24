using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private List<Sprite> run;
    
    [SerializeField] public List<Sprite> idle;
    [SerializeField] public List<Sprite> walk;
    [SerializeField] public List<Sprite> jump;
    [SerializeField] public List<Sprite> dead;
    
    [SerializeField] private float delayFramesInSeconds;

    [SerializeField] public bool isSpritesLoadedWeb=false;

    [SerializeField] private PlayerMove playerMove;

    

    public void Start()
    {
#if UNITY_EDITOR
        ChangeAnims();
#elif UNITY_WEBGL
        StartCoroutine(ChangeAnimsWeb());
#else
        ChangeAnims();
#endif


    }

    public void ChangeAnims()
    {
        idle = LoadSprites("Player/Idle", ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesIdle);
        
        float heightScale = (float)spriteRenderer.sprite.texture.height / idle[0].texture.height;
        transform.localScale =
            new Vector3(heightScale * transform.localScale.x, heightScale * transform.localScale.y, 1);
        
        run = LoadSprites("Player/Run", ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesRun);
        walk = LoadSprites("Player/Walk", ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesWalk);
        jump = LoadSprites("Player/Jump", ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesJump);
        dead = LoadSprites("Player/Dead", ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesDead);
        isSpritesLoadedWeb = true;
    }

    public IEnumerator ChangeAnimsWeb()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Color unseen = spriteRenderer.color;
        unseen.a = 0;
        Color seen = spriteRenderer.color;
        
        yield return spriteRenderer.color = unseen;

        
        
        yield return rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        
        yield return LoadSpritesWeb("Player/Idle", idle,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesIdle);
        AnimIdle();
        
        yield return rb.constraints = RigidbodyConstraints2D.None;
        yield return rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return spriteRenderer.color = seen;

        
        
        float heightScale = (float)spriteRenderer.sprite.texture.height / idle[0].texture.height;
        transform.localScale =
            new Vector3(heightScale * transform.localScale.x, heightScale * transform.localScale.y, 1);
        
        
        yield return LoadSpritesWeb("Player/Run", run,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesRun);
        yield return LoadSpritesWeb("Player/Walk", walk,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesWalk);
        yield return LoadSpritesWeb("Player/Jump", jump,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesJump);
        yield return LoadSpritesWeb("Player/Dead", dead,ConfigurationSprites.SharedInstance.ConfigurationSpritesData.framesDead);
        yield return isSpritesLoadedWeb = true;

        

        
    }

    IEnumerator LoadSpritesWeb(string nameFile, List<Sprite> sprites,int numSprites)
    {
        SelectSprite selectSprite = new SelectSprite();
        sprites.Clear();

        for (int i = 1; i < numSprites + 1; i++)
        {
            yield return StartCoroutine(selectSprite.ChangeSpriteWeb($"{nameFile} ({i}).png",
                (spriteChanged) => { sprites.Add(spriteChanged); }));
        }
    }







    public List<Sprite> LoadSprites(string nameFile, int numSprites)
    {
        List<Sprite> sprites = new List<Sprite>();
        SelectSprite selectSprite = new SelectSprite();
        
        for (int i = 1; i < numSprites+1; i++)
        {
            //print(i);

            sprites.Add(selectSprite.ChangeSprite($"{nameFile} ({i}).png"));


        }


        return sprites;
    }

    [ContextMenu("run")]
    public void AnimRun()
    {
        StopAnims();
        StartCoroutine(StartAnim(run));
    }
    
    public void AnimWalk()
    {
        StopAnims();
        StartCoroutine(StartAnim(walk));
    }

    [ContextMenu("idle")]
    public void AnimIdle()
    {
        StopAnims();
        StartCoroutine(StartAnim(idle));
    }
    
    [ContextMenu("jump")]
    public void AnimJump()
    {
        StopAnims();
        StartCoroutine(StartAnim(jump,true));
    }
    public void AnimDead()
    {
        StopAnims();
        StartCoroutine(StartAnim(dead,true));
    }
    

    IEnumerator StartAnim(List<Sprite> frames, bool oneTime=false)
    {
        if (frames.Count == 0)
        {
            yield break;
        }

        bool canPass=true;
        while (true&& canPass)
        {
            for (int i = 0; i < frames.Count; i++)
            {
                spriteRenderer.sprite = frames[i];
                yield return new WaitForSeconds(ConfigurationSprites.SharedInstance.ConfigurationSpritesData.delayFramesInSeconds);
            }

            if (oneTime)
            {
                canPass = false;
            }

        }
        //yield return null;
    }

    public void StopAnims()
    {

        if (isSpritesLoadedWeb)
        {
            StopAllCoroutines();
        }
    }
    public void ChangeScale()
    {

    }


}
