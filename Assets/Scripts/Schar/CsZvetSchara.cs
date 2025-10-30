
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CsZvetSchara : MonoBehaviour
{
    [SerializeField] SpriteRenderer _SpriteRenderer1;
    [SerializeField] SpriteRenderer _SpriteRenderer2;
    [SerializeField] Sprite [] _zvet1 = new Sprite[8];
    [SerializeField] Sprite[] _zvet2 = new Sprite[8];
    int nomer;
    // Start is called before the first frame update
    void Start()
    {
        nomer = Random.Range(0, 8);
        // ѕровер€ем, чтобы массивы не были пустыми
        if (_zvet1.Length > nomer && _zvet2.Length > nomer)
        {
            _SpriteRenderer1.sprite = _zvet1[nomer];
            _SpriteRenderer2.sprite = _zvet2[nomer];
        }
        else
        {
            Debug.LogWarning("Ќедостаточно спрайтов в массивах _zvet1 или _zvet2!");
        }
    }
}

