using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instanceRef;
    public List<StoryChapter> StoryChapters = new List<StoryChapter>(MAX_CHAPTERS);
    public bool isOnDialogue = false;

    public int currentChapter = 0;
    public int currentDialogue = 0;

    public Sprite[] McSprites;
    public Sprite[] NFSprites;
    public Sprite[] AliceSprites;

    private const int MAX_CHAPTERS = 2;


    private const string mainCharacterName = "Yuuki";
    private const string friendName = "Hal";
    private const string aliceName = "Alice";
    private const string phoneName = "Phone";

    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);
    }
    public void setAllDialogues()
    {
        //TODO: CHANGE TO PHONE BG IN Ch0 DIALOGUE 2 sentence no.?
        #region Chapter0

        //Denial - Start//
        StoryChapters[0].ChapterTitle = "Denial";
        StoryChapters[0].ChapterDialogues = new Dialogue[7];

        //along in dimly lit room
        //chapter 1 - dialogue 1 - only 1 speaker
        StoryChapters[0].ChapterDialogues[0] = new Dialogue();
        StoryChapters[0].ChapterDialogues[0].name = mainCharacterName;
        StoryChapters[0].ChapterDialogues[0].otherName = "";
        StoryChapters[0].ChapterDialogues[0].sentences = new string[6];
        StoryChapters[0].ChapterDialogues[0].speaker1Lines = new int[] {0, 1, 2, 3,4,5 };
        StoryChapters[0].ChapterDialogues[0].sentences[0] = "Well then… I guess that’s it for us then.";
        StoryChapters[0].ChapterDialogues[0].sentences[1] = "Five years, gone, just like that.";
        StoryChapters[0].ChapterDialogues[0].sentences[2] = "...";
        StoryChapters[0].ChapterDialogues[0].sentences[3] = "It’s only been a week and...";
        StoryChapters[0].ChapterDialogues[0].sentences[4] = "*sigh*";
        StoryChapters[0].ChapterDialogues[0].sentences[5] = "Time for school I guess.";
        StoryChapters[0].ChapterDialogues[0].speaker1Sprites = new Sprite[] {};
        StoryChapters[0].ChapterDialogues[0].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[0].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[0].dialogueIndex = 0;
        //fade to black i guess...

        //goes to school and meets new friend 
        //chapter 1 - dialogue 2 - 2 speakers
        StoryChapters[0].ChapterDialogues[1] = new Dialogue();
        StoryChapters[0].ChapterDialogues[1].name = mainCharacterName;
        StoryChapters[0].ChapterDialogues[1].otherName = friendName;
        StoryChapters[0].ChapterDialogues[1].sentences = new string[32];
        StoryChapters[0].ChapterDialogues[1].speaker1Lines = new int[] { 1, 2, 5, 7, 10, 12, 14, 16, 17, 19, 23, 26, 28, 31};
        StoryChapters[0].ChapterDialogues[1].speaker2Lines = new int[] { 0, 3, 4, 6, 8, 9, 11, 13, 15, 18, 20, 21, 22, 24, 27, 29, 30} ;
        StoryChapters[0].ChapterDialogues[1].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[1].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[1].speaker2Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[1].speaker2Sprites = NFSprites;
        StoryChapters[0].ChapterDialogues[1].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[1].dialogueIndex = 1;
        //narration [] = 25


        StoryChapters[0].ChapterDialogues[1].sentences[0] = "Hey there! You don’t look too good. School draining you dead already?";

        StoryChapters[0].ChapterDialogues[1].sentences[1] = "Ah, h-hi.";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "And, no, I just had a rough start to my day, that’s all.";

        StoryChapters[0].ChapterDialogues[1].sentences[3] = "But it’s lunch time though?";
        StoryChapters[0].ChapterDialogues[1].sentences[4] = "Anyways, my name is Hal, what’s yours?";

        StoryChapters[0].ChapterDialogues[1].sentences[5] = "It’s Yuuki.";

        StoryChapters[0].ChapterDialogues[1].sentences[6] = "You into video games?";

        StoryChapters[0].ChapterDialogues[1].sentences[7] = "Yeah. Why do you ask?";

        StoryChapters[0].ChapterDialogues[1].sentences[8] = "I saw you have a very familiar rainbow-colored multiprism keychain, figured you were into that game series";
        StoryChapters[0].ChapterDialogues[1].sentences[9] = "Anyways, what kind of games are you playing?";

        StoryChapters[0].ChapterDialogues[1].sentences[10] = "Well, I was into MMORPGs at a certain period of time. Like Last Legend 14, along with Last Legend 13,LL13-2, and LL13 - Thunder Rebounds. So, generally JRPGs.";
        
        StoryChapters[0].ChapterDialogues[1].sentences[11] = "Interesting mix! Have you ever played gacha games before?";

        StoryChapters[0].ChapterDialogues[1].sentences[12] = "I’ve heard of them, but I haven’t tried any yet. Too busy with MMOs, but… I guess I won’t be playing LL14 anytime soon.";

        StoryChapters[0].ChapterDialogues[1].sentences[13] = "Why is that, if you don't mind me asking?";

        StoryChapters[0].ChapterDialogues[1].sentences[14] = "I broke up with my girlfriend.";

        StoryChapters[0].ChapterDialogues[1].sentences[15] = "That’s rough buddy.";

        StoryChapters[0].ChapterDialogues[1].sentences[16] = "...";
        StoryChapters[0].ChapterDialogues[1].sentences[17] = "Anyways, you were asking?";

        StoryChapters[0].ChapterDialogues[1].sentences[18] = "Ah right! The reason why I asked you is because there’s this new game that got released today. It’s called ‘MagiHimeDive’!";

        StoryChapters[0].ChapterDialogues[1].sentences[19] = "Magihimi-what?";

        StoryChapters[0].ChapterDialogues[1].sentences[20] = "MagiHimeDive!, it’s actually localized as Magical Princess Dive, but we call it MagiHimeDive for simplicity’s sake";
        StoryChapters[0].ChapterDialogues[1].sentences[21] = "It’s a gacha game where you save the world as a tactician from another world, and you command princesses to fight for you!";
        StoryChapters[0].ChapterDialogues[1].sentences[22] = "The waifus look amazing!";


        StoryChapters[0].ChapterDialogues[1].sentences[23] = "I-I see…";

        StoryChapters[0].ChapterDialogues[1].sentences[24] = "Well, have a look at the first one they give you!";

        //narration
        StoryChapters[0].ChapterDialogues[1].sentences[25] = "A blonde girl wearing something reminiscent of a school uniform smiles, facing the screen. A quick image of a silhouette of a woman flashes by.";

        StoryChapters[0].ChapterDialogues[1].sentences[26] = "W-Wow. She does look pretty. Is it available for Cyborg devices?";

        StoryChapters[0].ChapterDialogues[1].sentences[27] = "Yeah! So, are you interested in playing?";

        StoryChapters[0].ChapterDialogues[1].sentences[28] = "Sure. Let me download it right now.";
        
        StoryChapters[0].ChapterDialogues[1].sentences[29] = "Alright! Here’s my friend code!";

        StoryChapters[0].ChapterDialogues[1].sentences[30] = "Oh! My class is in another room. I’ll go ahead.";

        StoryChapters[0].ChapterDialogues[1].sentences[31] = "Alright…";
        //screen fade to black then back to dimly lit room

        //chapter 1 - dialogue 3
        StoryChapters[0].ChapterDialogues[2] = new Dialogue();
        StoryChapters[0].ChapterDialogues[2].name = phoneName;
        StoryChapters[0].ChapterDialogues[2].otherName = aliceName;
        StoryChapters[0].ChapterDialogues[2].sentences = new string[14];
        StoryChapters[0].ChapterDialogues[2].speaker1Lines = new int[] { 0, 1, 2, 8};
        StoryChapters[0].ChapterDialogues[2].speaker2Lines = new int[] { 5, 6, 9, 11, 13};
        StoryChapters[0].ChapterDialogues[2].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[2].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[2].speaker2Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[2].speaker2Sprites = AliceSprites;
        StoryChapters[0].ChapterDialogues[2].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[2].dialogueIndex = 2;

        StoryChapters[0].ChapterDialogues[2].sentences[0] = "...And it's done.";
        StoryChapters[0].ChapterDialogues[2].sentences[1] = "Still, the game is relatively light for a gacha game.";
        StoryChapters[0].ChapterDialogues[2].sentences[2] = "The princesses do look good though.";

        //narration
        StoryChapters[0].ChapterDialogues[2].sentences[3] = "And the game starts.";
        StoryChapters[0].ChapterDialogues[2].sentences[4] = "A blonde woman stands in front of the screen, wearing the same familiar school uniform. She smiles.";

        //alice
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Ah, hero, you’re finally here!";
        StoryChapters[0].ChapterDialogues[2].sentences[6] = "Quickly, take my hand!";

        //spawn phone image
        StoryChapters[0].ChapterDialogues[2].sentences[7] = "And just like that, I was whisked away.The girl dragged me out of the dimly lit room… And it was there that I saw it, a world of color.";
        
        StoryChapters[0].ChapterDialogues[2].sentences[8] = "What are we, colorless?";

        StoryChapters[0].ChapterDialogues[2].sentences[9] = "Hero, there’s no time to explain, kiss the back of my palm!";

        //phone
        StoryChapters[0].ChapterDialogues[2].sentences[10] = "[Phone]“Huh? Why?” I asked out of surprise. As I asked, however, the area immediately began to feel eerie, and a shiver went down my spine.";
        
        StoryChapters[0].ChapterDialogues[2].sentences[11] = "Hurry!";

        //phone
        StoryChapters[0].ChapterDialogues[2].sentences[12] = "[Phone]Alice shoves her hand towards my face, and I immediately kiss it.As I pull back, A mark is left on the back of her palm.A crown. A smile of confidence replaces her once nervous face.Light begins to combine around the mark on her hand, and a sword forms from the light";
        
        StoryChapters[0].ChapterDialogues[2].sentences[13] = "Alright! Let’s do this!";

        //start phone game (load phone game scene)
        StoryChapters[0].ChapterDialogues[3] = new Dialogue();

        //dia4
        StoryChapters[0].ChapterDialogues[4] = new Dialogue();
        StoryChapters[0].ChapterDialogues[4].name = phoneName;
        StoryChapters[0].ChapterDialogues[4].otherName = aliceName;
        StoryChapters[0].ChapterDialogues[4].sentences = new string[7];
        StoryChapters[0].ChapterDialogues[4].speaker1Lines = new int[] { 1, 3, 6 };
        StoryChapters[0].ChapterDialogues[4].speaker2Lines = new int[] { 0,2,4, 5};
        StoryChapters[0].ChapterDialogues[4].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[4].speaker1Sprites = McSprites; //phone
        StoryChapters[0].ChapterDialogues[4].speaker2Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[4].speaker2Sprites = AliceSprites;
        StoryChapters[0].ChapterDialogues[4].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[4].dialogueIndex = 4;

        StoryChapters[0].ChapterDialogues[4].sentences[0] = "Whew, Thanks Hero. If it wasn’t for you, we would’ve been lost.";
        StoryChapters[0].ChapterDialogues[4].sentences[1] = "H-How did I know your name?";
        StoryChapters[0].ChapterDialogues[4].sentences[2] = "Ah, right… The moment you kissed my hand, we formed a contract. I’m now your pawn, and you, my king.";
        StoryChapters[0].ChapterDialogues[4].sentences[3] = "King?";
        StoryChapters[0].ChapterDialogues[4].sentences[4] = "However, we need to look for more people to join under your army… ";
        StoryChapters[0].ChapterDialogues[4].sentences[5] = "Come! There’s a camp nearby where others have come to rest. Follow me!";
        StoryChapters[0].ChapterDialogues[4].sentences[6] = "She takes my hand once more, and I get dragged away to a small camp. There were a few people around, carrying crates, rations, and delivering them around the place.";


        //
        //dialogue 5 - gacha screen
        StoryChapters[0].ChapterDialogues[5] = new Dialogue();
        StoryChapters[0].ChapterDialogues[5].name = mainCharacterName;
        StoryChapters[0].ChapterDialogues[5].otherName = "";
        StoryChapters[0].ChapterDialogues[5].sentences = new string[8];
        StoryChapters[0].ChapterDialogues[5].speaker1Lines = new int[] { 0,1,2,3,4,5,6,7,8 };
        StoryChapters[0].ChapterDialogues[5].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[5].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[5].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[5].dialogueIndex = 5;

        StoryChapters[0].ChapterDialogues[5].sentences[0] = "The gacha mechanic huh?";
        StoryChapters[0].ChapterDialogues[5].sentences[1] = "Okay, we have what we need now. Don’t forget to select the princesses you want in our adventure!";
        StoryChapters[0].ChapterDialogues[5].sentences[2] = "Well, that’s enough for tonight… I have to go to sleep tomorrow…";
        StoryChapters[0].ChapterDialogues[5].sentences[3] = "Ah, right, I have to add him as a friend";
        StoryChapters[0].ChapterDialogues[5].sentences[4] = "Huh, he already has an SSR unit.";
        StoryChapters[0].ChapterDialogues[5].sentences[5] = "Nice.";
        StoryChapters[0].ChapterDialogues[5].sentences[6] = "That was...";
        StoryChapters[0].ChapterDialogues[5].sentences[7] = "...Surprisingly fun.";

        //dialogue 6
        StoryChapters[0].ChapterDialogues[6] = new Dialogue();
        StoryChapters[0].ChapterDialogues[6].name = mainCharacterName;
        StoryChapters[0].ChapterDialogues[6].otherName = "";
        StoryChapters[0].ChapterDialogues[6].sentences = new string[7];
        StoryChapters[0].ChapterDialogues[6].speaker1Lines = new int[] { 1, 2, 3, 4, 5 };
        StoryChapters[0].ChapterDialogues[6].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[6].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[6].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[6].dialogueIndex = 6;

        StoryChapters[0].ChapterDialogues[6].sentences[0] = "As I turned off the screen, my vision was filled with darkness once more. Once again, I am left alone with my thoughts.";
        StoryChapters[0].ChapterDialogues[6].sentences[1] = "This entire morning was just a joke, right?";
        StoryChapters[0].ChapterDialogues[6].sentences[2] = "There’s no way we’re over.";
        StoryChapters[0].ChapterDialogues[6].sentences[3] = "Tomorrow, I’ll wake up with a message from them telling me that it was just a prank.";
        StoryChapters[0].ChapterDialogues[6].sentences[4] = "Yeah, just a prank.";
        StoryChapters[0].ChapterDialogues[6].sentences[5] = "...";
        StoryChapters[0].ChapterDialogues[6].sentences[6] = "Filled with thoughts of them, I fall asleep.";
        
        //Denial - End//
        
        #endregion

        #region Chapter1
        //Anger - Start//
        StoryChapters[1].ChapterTitle = "Anger";
        StoryChapters[1].ChapterDialogues = new Dialogue[10];

        //along in dimly lit room
        //chapter 1 - dialogue 1 - only 1 speaker
        StoryChapters[1].ChapterDialogues[0] = new Dialogue();
        StoryChapters[1].ChapterDialogues[0].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[0].otherName = "";
        StoryChapters[1].ChapterDialogues[0].sentences = new string[12];
        StoryChapters[1].ChapterDialogues[0].speaker1Lines = new int[] { 2};
        StoryChapters[1].ChapterDialogues[0].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[0].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[0].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[0].dialogueIndex = 0;

        StoryChapters[1].ChapterDialogues[0].sentences[0] = "My eyes slowly open to the sound of an alarm.";
        StoryChapters[1].ChapterDialogues[0].sentences[1] = "My hands immediately shoot up to grab my phone.";
        StoryChapters[1].ChapterDialogues[0].sentences[2] = "Is it---?";
        StoryChapters[1].ChapterDialogues[0].sentences[3] = "My morning alarm."; //NEED ALARM SOUND EX
        StoryChapters[1].ChapterDialogues[0].sentences[4] = "I open my phone and check for any alerts or messages.";
        StoryChapters[1].ChapterDialogues[0].sentences[5] = "None.";
        StoryChapters[1].ChapterDialogues[0].sentences[6] = "I check my missed calls.";
        StoryChapters[1].ChapterDialogues[0].sentences[7] = "Nothing.";
        StoryChapters[1].ChapterDialogues[0].sentences[8] = "I then quickly went to turn on my PC.";
        StoryChapters[1].ChapterDialogues[0].sentences[9] = "Logged in LL14 to check for any Whispers.";
        StoryChapters[1].ChapterDialogues[0].sentences[10] = "Still nothing.";
        StoryChapters[1].ChapterDialogues[0].sentences[11] = "And with a disappointed sigh, I prepare for school.";


        StoryChapters[1].ChapterDialogues[1] = new Dialogue();
        StoryChapters[1].ChapterDialogues[1].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[1].otherName = friendName;
        StoryChapters[1].ChapterDialogues[1].sentences = new string[22];
        StoryChapters[1].ChapterDialogues[1].speaker1Lines = new int[] { 5,6,8,10,12,15,17,18 };
        StoryChapters[1].ChapterDialogues[1].speaker2Lines = new int[] { 4,7,9,11,13,14,16,19,20 };

        StoryChapters[1].ChapterDialogues[1].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[1].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[1].speaker2Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[1].speaker2Sprites = NFSprites;
        StoryChapters[1].ChapterDialogues[1].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[1].dialogueIndex = 1;

        StoryChapters[1].ChapterDialogues[1].sentences[0] = "As the classes continued, I found myself checking my phone often.";
        StoryChapters[1].ChapterDialogues[1].sentences[1] = "Hoping that there will be at least a message for me.";
        StoryChapters[1].ChapterDialogues[1].sentences[2] = "And as I wait…";
        StoryChapters[1].ChapterDialogues[1].sentences[3] = "…the bell rings."; //BELL ring SFX
        StoryChapters[1].ChapterDialogues[1].sentences[4] = "What's up! Have you played the game yet?";
        StoryChapters[1].ChapterDialogues[1].sentences[5] = "Yeah. It’s interesting.";
        StoryChapters[1].ChapterDialogues[1].sentences[6] = "The girls look nice but…";
        StoryChapters[1].ChapterDialogues[1].sentences[7] = "Not your type?";
        StoryChapters[1].ChapterDialogues[1].sentences[8] = "It’s not that, there’s just something that feels off.";
        StoryChapters[1].ChapterDialogues[1].sentences[9] = "Well, I did say that the game had cute girls in it, not great characters.";
        StoryChapters[1].ChapterDialogues[1].sentences[10] = "That’s… True.";
        StoryChapters[1].ChapterDialogues[1].sentences[11] = "There’s an event next week! A Limited Gacha banner with an OP unit! She’s also pretty too to boot! I’m rolling for her, what about you?";
        StoryChapters[1].ChapterDialogues[1].sentences[12] = "I don’t have enough currency to get the guarantee off the banner yet.";
        StoryChapters[1].ChapterDialogues[1].sentences[13] = "Well, you can always spend money.";
        StoryChapters[1].ChapterDialogues[1].sentences[14] = "I mean, if I remember correctly, Last Legend 14 was a subscription based MMO, right?";
        StoryChapters[1].ChapterDialogues[1].sentences[15] = "Yes. But…";
        StoryChapters[1].ChapterDialogues[1].sentences[16] = "So what’s the difference between the subscription and the gacha? Just spend the same amount of money every month!";
        StoryChapters[1].ChapterDialogues[1].sentences[17] = "I guess I could try.";
        StoryChapters[1].ChapterDialogues[1].sentences[18] = "I’ll play later when I get home.";
        StoryChapters[1].ChapterDialogues[1].sentences[19] = "Oh yeah! I accepted your friend request!";
        StoryChapters[1].ChapterDialogues[1].sentences[20] = "I hope you’ll use my support unit when you’re in trouble!";
        StoryChapters[1].ChapterDialogues[1].sentences[21] = "We then continued to hang out at lunchtime and eat.";
        

        #endregion


    }

    public void setAllChapters()
    {
        
        for (int i = 0; i < MAX_CHAPTERS; i++)
        {
            StoryChapters[i] = new StoryChapter();
            StoryChapters[i].ChapterNumber = i + 1;
        }
    }

    public void NextChapter()
    {
        currentChapter++;
        currentDialogue = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        setAllChapters();
        setAllDialogues();
        FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[0], true);
        FindObjectOfType<AudioManager>().Play("Birds",true);
    }

    // Update is called once per frame
    void Update()
    {

        //IF DIALOGUE JUST INITIALIZED, THEN DO THESE IF STATEMENTS DURING UPDATE
        if (FindObjectOfType<StoryManager>().currentDialogue == 1 &&
            FindObjectOfType<StoryManager>().currentChapter == 0)
        {

            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[1], true);
                FindObjectOfType<AudioManager>().Stop("Birds");
                FindObjectOfType<AudioManager>().Play("ClassroomBGM", true);
            }

        }
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[2], true);
                FindObjectOfType<AudioManager>().Stop("ClassroomBGM");
                FindObjectOfType<AudioManager>().Play("RoomBGM", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5].isDone 
                && FindObjectOfType<BackgroundManager>().GachaBackground.activeInHierarchy)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[5], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 6)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[6], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].isDone)
            {
                FindObjectOfType<StoryManager>().currentChapter = 1;
                FindObjectOfType<StoryManager>().currentDialogue = 0;
                
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[1].ChapterDialogues[0], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[1].ChapterDialogues[1], true);
            }
        }
    }
}
