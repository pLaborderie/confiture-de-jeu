using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class texteStory : MonoBehaviour
{

    private Text uiText;

    private float showSpeed = 0.025f;

    private string showText, uiTextCopy;

    private bool coroutineProtect, loadText;


    private void Start()
    {
        uiText = GetComponent<Text>();

        TextInformations();
    }

    private void OnEnable() { uiTextCopy = null; }

    private void Update()
    {
        if (loadText && !coroutineProtect)
        {
            StartCoroutine(LoadLetters(uiTextCopy));
            coroutineProtect = true;
        }

        else if (loadText && coroutineProtect) { uiText.text = showText; }

        else if (!loadText && !coroutineProtect)
        {
            if (uiText.text != uiTextCopy) { TextInformations(); }
        }
    }

    private void TextInformations()
    {
        uiTextCopy = uiText.text;
        showText = null;
        uiText.text = null;

        loadText = true;
        coroutineProtect = false;
    }

    private IEnumerator LoadLetters(string completeText)
    {
        int textSize = 0;

        while (textSize < completeText.Length)
        {
            showText += completeText[textSize++];
            yield return new WaitForSeconds(showSpeed);
        }

        coroutineProtect = false;
        loadText = false;
    }

}