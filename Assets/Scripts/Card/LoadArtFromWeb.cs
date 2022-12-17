using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadArtFromWeb : MonoBehaviour
{
    [SerializeField] private Image _art;

    [System.Obsolete]
    private IEnumerator Start()
    {
        _art = GetComponent<Image>();

        WWW www = new("https://picsum.photos/195/155");
        yield return www;

        Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);

        _art.sprite = sprite;
    }
}
