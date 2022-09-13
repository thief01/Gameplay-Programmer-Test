using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    private const int MAP_SIZE = 160;
    private const float RESPAWN_TIME = 1;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Pool<Astereoid>.Instance.OnObjectDestroyed.AddListener(() => StartCoroutine(OnAstereoidDestroyed()));
    }

    private IEnumerator OnAstereoidDestroyed()
    {
        yield return new WaitForSeconds(RESPAWN_TIME);
        var astereoid = Pool<Astereoid>.Instance.GetObject();
    }
}
