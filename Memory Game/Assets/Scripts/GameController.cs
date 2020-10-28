using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] images;

    public List<Sprite> gameImages = new List<Sprite>();

    public List<Button> btns = new List<Button>();
    
    private bool firstGuess, secondGuess;

    private int firstGuessIndex, secondGuessIndex;

    private int countGuesses, countCorrectGuesses, gameGuesses;

    private string firstGuessImage, secondGuessImage;

    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/Candy");
    }

    void Start()
    {
        GetButtons();
        AddGameImages();
        AddListeners();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GameButton");
        
        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGameImages()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }
            gameImages.Add(images[index]);
            index++;
        }
        /* adding images to half of the buttons, then setting the index to 0 
           so that we can add their pair images for the other half */
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => ClickButton());
            // adding the click function to the buttons (after generating them)
        }
    }

    public void ClickButton()
    {        
        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessImage = gameImages[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gameImages[firstGuessIndex];

        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessImage = gameImages[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gameImages[secondGuessIndex];
            
            if(firstGuessImage == secondGuessImage)
            {
                Debug.Log("The images match");
            }
            else
            {
                Debug.Log("The images don't match");
            }
        }
    } 
}
