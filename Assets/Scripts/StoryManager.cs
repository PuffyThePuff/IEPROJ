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
    private const int MAX_CHAPTERS = 6;

    public List<StoryChapter> StoryChapters = new List<StoryChapter>(MAX_CHAPTERS);
    public bool isOnDialogue = false;

    public int currentChapter = 0;
    public int currentDialogue = 0;

    public Sprite[] McSprites;
    public Sprite[] NFSprites;
    public Sprite[] AliceSprites;
    public Sprite[] PhoneSprites;



    private const string mainCharacterName = "Yuuki";
    private const string friendName = "Hal";
    private const string aliceName = "Alice";
    private const string phoneName = "Phone";

    private const string olderMan = "Older Man";
    private const string woman = "Woman";



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

        StoryChapters[0].ChapterDialogues[0].speaker1ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[0].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[0].speaker1ExpressionIndex.Length; k++)
        {
            StoryChapters[0].ChapterDialogues[0].speaker1ExpressionIndex[k] = 0;
        }
        
        //fade to black i guess...
        //Debug.Log("c0d0 success");

        //goes to school and meets new friend 
        //chapter 1 - dialogue 2 - 2 speakers
        StoryChapters[0].ChapterDialogues[1] = new Dialogue();
        StoryChapters[0].ChapterDialogues[1].name = mainCharacterName;
        StoryChapters[0].ChapterDialogues[1].otherName = friendName;
        StoryChapters[0].ChapterDialogues[1].sentences = new string[36];
        StoryChapters[0].ChapterDialogues[1].speaker1Lines = new int[] { 5, 6, 9, 11, 14, 16, 18, 20, 21, 23, 27, 30, 32, 35};
        StoryChapters[0].ChapterDialogues[1].speaker2Lines = new int[] { 4, 7, 8, 10, 12, 13, 15, 17, 19, 22, 24, 25, 26, 28, 31, 33, 34} ;
        StoryChapters[0].ChapterDialogues[1].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[1].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[1].speaker2Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[1].speaker2Sprites = NFSprites;
        StoryChapters[0].ChapterDialogues[1].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[1].dialogueIndex = 1;
        //narration [] = 25

        #region Expressions
        StoryChapters[0].ChapterDialogues[1].speaker1ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[1].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[1].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 6 || k == 18 || k == 19 || k == 20)
            {
                StoryChapters[0].ChapterDialogues[1].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 30)
            {
                StoryChapters[0].ChapterDialogues[1].speaker1ExpressionIndex[k] = 2;
            }
            else
            {
                StoryChapters[0].ChapterDialogues[1].speaker1ExpressionIndex[k] = 0;
            }
        }

        StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[1].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex.Length; k++)
        {
            if (k == 7 || k == 11 || k == 12 || k == 26 || k == 33)
            {
                StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex[k] = 1;
            }
            else if (k == 20)
            {
                StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex[k] = 2;
            }
            else if (k == 19)
            {
                StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex[k] = 3;
            }
            else
            {
                StoryChapters[0].ChapterDialogues[1].speaker2ExpressionIndex[k] = 0;
            }
        }


        #endregion


        StoryChapters[0].ChapterDialogues[1].sentences[0] =
            "School. A place filled with dying hopes and dreams. People just chatting about whatever they fancy. The newest makeup set, the latest sports game, their romantic interests.";
        StoryChapters[0].ChapterDialogues[1].sentences[1] =
            "I take a seat in the far corner of the room. The gray light peered through the window. Slowly, my thoughts my previous relationship start to spring up again, drowning out the noise of the crowd around me.";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "Why?";
        StoryChapters[0].ChapterDialogues[1].sentences[3] = "What did I do wrong?";
        StoryChapters[0].ChapterDialogues[1].sentences[4] = "Hey there! You don’t look too good. School draining you dead already?";
        StoryChapters[0].ChapterDialogues[1].sentences[5] = "Ah, h-hi.";
        StoryChapters[0].ChapterDialogues[1].sentences[6] = "And, no, I just had a rough start to my day, that’s all.";
        StoryChapters[0].ChapterDialogues[1].sentences[7] = "Oof, mood.";
        StoryChapters[0].ChapterDialogues[1].sentences[8] = "Anyways, my name is Halvard. You can just call me Hal, what’s yours?";
        StoryChapters[0].ChapterDialogues[1].sentences[9] = "It’s Yuuki.";
        StoryChapters[0].ChapterDialogues[1].sentences[10] = "You into video games?";
        StoryChapters[0].ChapterDialogues[1].sentences[11] = "Yeah. Why do you ask?";
        StoryChapters[0].ChapterDialogues[1].sentences[12] = "I saw you have a very familiar rainbow-colored multiprism keychain, figured you were into that game series";
        StoryChapters[0].ChapterDialogues[1].sentences[13] = "Anyways, what kind of games are you playing?";
        StoryChapters[0].ChapterDialogues[1].sentences[14] = "Well, I was into MMORPGs at a certain period of time. Like Last Legend 14, along with Last Legend 13,LL13-2, and LL13 - Thunder Rebounds. So, generally JRPGs.";
        StoryChapters[0].ChapterDialogues[1].sentences[15] = "Interesting mix! Have you ever played gacha games before?";
        StoryChapters[0].ChapterDialogues[1].sentences[16] = "I’ve heard of them, but I haven’t tried any yet. Too busy with MMOs, but… I guess I won’t be playing LL14 anytime soon.";

        StoryChapters[0].ChapterDialogues[1].sentences[17] = "Why is that, if you don't mind me asking?";

        StoryChapters[0].ChapterDialogues[1].sentences[18] = "A breakup.";

        StoryChapters[0].ChapterDialogues[1].sentences[19] = "That’s rough buddy.";

        StoryChapters[0].ChapterDialogues[1].sentences[20] = "...";
        StoryChapters[0].ChapterDialogues[1].sentences[21] = "Anyways, you were asking?";

        StoryChapters[0].ChapterDialogues[1].sentences[22] = "Ah right! The reason why I asked you is because there’s this new game that got released today. It’s called ‘MagiHimeDive’!";

        StoryChapters[0].ChapterDialogues[1].sentences[23] = "Magihimi-what?";

        StoryChapters[0].ChapterDialogues[1].sentences[24] = "MagiHimeDive!, it’s actually localized as Magical Princess Dive, but we call it MagiHimeDive for simplicity’s sake";
        StoryChapters[0].ChapterDialogues[1].sentences[25] = "It’s a gacha game where you save the world as a tactician from another world, and you command princesses to fight for you!";
        StoryChapters[0].ChapterDialogues[1].sentences[26] = "The waifus look amazing!";

        StoryChapters[0].ChapterDialogues[1].sentences[27] = "I-I see…";

        StoryChapters[0].ChapterDialogues[1].sentences[28] = "Well, have a look at the first one they give you!";
        StoryChapters[0].ChapterDialogues[1].sentences[29] = "A blonde girl wearing something reminiscent of a school uniform smiles, facing the screen. A quick image of a silhouette of a woman flashes by.";
        StoryChapters[0].ChapterDialogues[1].sentences[30] = "W-Wow. She does look pretty. Is it available for Cyborg devices?";
        StoryChapters[0].ChapterDialogues[1].sentences[31] = "Yeah! So, are you interested in playing?";
        StoryChapters[0].ChapterDialogues[1].sentences[32] = "Sure. Let me download it right now.";
        StoryChapters[0].ChapterDialogues[1].sentences[33] = "Alright! Here’s my friend code!";
        StoryChapters[0].ChapterDialogues[1].sentences[34] = "Oh! My class is in another room. I’ll go ahead.";
        StoryChapters[0].ChapterDialogues[1].sentences[35] = "Alright…";

        //screen fade to black then back to dimly lit room
        //Debug.Log("c0d1 success");

        //chapter 1 - dialogue 3
        StoryChapters[0].ChapterDialogues[2] = new Dialogue();
        StoryChapters[0].ChapterDialogues[2].name = phoneName;
        StoryChapters[0].ChapterDialogues[2].otherName = aliceName;
        StoryChapters[0].ChapterDialogues[2].sentences = new string[18];
        StoryChapters[0].ChapterDialogues[2].speaker1Lines = new int[] { 0, 1, 2, 8, 10, 17};
        StoryChapters[0].ChapterDialogues[2].speaker2Lines = new int[] { 5, 6, 9, 12};
        StoryChapters[0].ChapterDialogues[2].speaker1Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[2].speaker1Sprites = McSprites;
        StoryChapters[0].ChapterDialogues[2].speaker2Sprites = new Sprite[] { };
        StoryChapters[0].ChapterDialogues[2].speaker2Sprites = AliceSprites;
        StoryChapters[0].ChapterDialogues[2].chapterNum = 0;
        StoryChapters[0].ChapterDialogues[2].dialogueIndex = 2;

        #region Expressions
        StoryChapters[0].ChapterDialogues[2].speaker1ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[2].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[2].speaker1ExpressionIndex.Length; k++)
        {
            StoryChapters[0].ChapterDialogues[2].speaker1ExpressionIndex[k] = 0;
            
        }

        StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[2].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex.Length; k++)
        {
            if (k == 5)
            {
                StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex[k] = 1;
            }
            else if (k == 11)
            {
                StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex[k] = 2;
            }
            else if (k == 9 || k == 12 || k == 13 || k == 14 || k == 15 || k == 16)
            {
                StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex[k] = 3;
            }
            else
            {
                StoryChapters[0].ChapterDialogues[2].speaker2ExpressionIndex[k] = 0;
            }
        }
        #endregion

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
        StoryChapters[0].ChapterDialogues[2].sentences[7] = "And just like that, I was whisked away. The girl dragged me out of the dimly lit room… And it was there that I saw it, a world of color.";
        
        StoryChapters[0].ChapterDialogues[2].sentences[8] = "What are we, colorless?";

        StoryChapters[0].ChapterDialogues[2].sentences[9] = "Hero, there’s no time to explain, kiss the back of my palm!";

        //phone
        StoryChapters[0].ChapterDialogues[2].sentences[10] = "Huh? Why? ";
        StoryChapters[0].ChapterDialogues[2].sentences[11] = "As I asked, however, the area immediately began to feel eerie, and a shiver went down my spine.";
        
        StoryChapters[0].ChapterDialogues[2].sentences[12] = "Hurry!";

        //phone
        StoryChapters[0].ChapterDialogues[2].sentences[13] = "Alice shoves her hand towards my face, and I immediately kiss it.";
        StoryChapters[0].ChapterDialogues[2].sentences[14] = "As I pull back, A mark is left on the back of her palm. A crown. ";
        StoryChapters[0].ChapterDialogues[2].sentences[15] = "A smile of confidence replaces her once nervous face.";
        StoryChapters[0].ChapterDialogues[2].sentences[16] = "Light begins to combine around the mark on her hand, and a sword forms from the light";

        StoryChapters[0].ChapterDialogues[2].sentences[17] = "Alright! Let’s do this!";
        //Debug.Log("c0d2 success");


        //start phone game (load phone game scene)
        StoryChapters[0].ChapterDialogues[3] = new Dialogue();
        //Debug.Log("c0d3 success");


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

        #region Expressions
        StoryChapters[0].ChapterDialogues[4].speaker1ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[4].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[4].speaker1ExpressionIndex.Length; k++)
        {
            StoryChapters[0].ChapterDialogues[4].speaker1ExpressionIndex[k] = 0;

        }

        StoryChapters[0].ChapterDialogues[4].speaker2ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[4].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[4].speaker2ExpressionIndex.Length; k++)
        {
            if (k == 5)
            {
                StoryChapters[0].ChapterDialogues[4].speaker2ExpressionIndex[k] = 1;
            }
            else if (k == 2)
            {
                StoryChapters[0].ChapterDialogues[4].speaker2ExpressionIndex[k] = 3;
            }
            else
            {
                StoryChapters[0].ChapterDialogues[4].speaker2ExpressionIndex[k] = 0;
            }
        }
        #endregion

        StoryChapters[0].ChapterDialogues[4].sentences[0] = "Whew, Thanks Hero. If it wasn’t for you, we would’ve been lost.";
        StoryChapters[0].ChapterDialogues[4].sentences[1] = "H-How did I know your name?";
        StoryChapters[0].ChapterDialogues[4].sentences[2] = "Ah, right… The moment you kissed my hand, we formed a contract. I’m now your pawn, and you, my king.";
        StoryChapters[0].ChapterDialogues[4].sentences[3] = "King?";
        StoryChapters[0].ChapterDialogues[4].sentences[4] = "However, we need to look for more people to join under your army… ";
        StoryChapters[0].ChapterDialogues[4].sentences[5] = "Come! There’s a camp nearby where others have come to rest. Follow me!";
        StoryChapters[0].ChapterDialogues[4].sentences[6] = "She takes my hand once more, and I get dragged away to a small camp. There were a few people around, carrying crates, rations, and delivering them around the place.";
        Debug.Log("c0d4 success");

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
        Debug.Log("c0d5 success");


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

        #region Expressions
        StoryChapters[0].ChapterDialogues[6].speaker1ExpressionIndex = new int[StoryChapters[0].ChapterDialogues[6].sentences.Length];
        for (int k = 0; k < StoryChapters[0].ChapterDialogues[6].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 1 || k > 2)
            {
                StoryChapters[0].ChapterDialogues[6].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 2)
            {
                StoryChapters[0].ChapterDialogues[6].speaker1ExpressionIndex[k] = 3;
            }
            else
                StoryChapters[0].ChapterDialogues[6].speaker1ExpressionIndex[k] = 0;

        }
        #endregion

        StoryChapters[0].ChapterDialogues[6].sentences[0] = "As I turned off the screen, my vision was filled with darkness once more. Once again, I am left alone with my thoughts.";
        StoryChapters[0].ChapterDialogues[6].sentences[1] = "This entire morning was just a joke, right?";
        StoryChapters[0].ChapterDialogues[6].sentences[2] = "There’s no way we’re over.";
        StoryChapters[0].ChapterDialogues[6].sentences[3] = "Tomorrow, I’ll wake up with a message from them telling me that it was just a prank.";
        StoryChapters[0].ChapterDialogues[6].sentences[4] = "Yeah, just a prank.";
        StoryChapters[0].ChapterDialogues[6].sentences[5] = "...";
        StoryChapters[0].ChapterDialogues[6].sentences[6] = "Filled with thoughts of them, I fall asleep.";
        Debug.Log("c0d6 success");
        //Denial - End//

        #endregion

        #region Chapter1
        //Anger - Start//
        StoryChapters[1].ChapterTitle = "Anger";
        StoryChapters[1].ChapterDialogues = new Dialogue[7];

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

        #region Expressions
        StoryChapters[1].ChapterDialogues[0].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[0].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[0].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 1 || k == 2)
            {
                StoryChapters[1].ChapterDialogues[0].speaker1ExpressionIndex[k] = 4;
            }
            else
                StoryChapters[1].ChapterDialogues[0].speaker1ExpressionIndex[k] = 0;

        }
        #endregion

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
        Debug.Log("c1d0 success");

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

        #region Expressions
        StoryChapters[1].ChapterDialogues[1].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[1].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[1].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 6 || k == 8 || k == 10 || k == 15 || k == 16)
            {
                StoryChapters[1].ChapterDialogues[1].speaker1ExpressionIndex[k] = 2;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[1].speaker1ExpressionIndex[k] = 0;
            }
                

        }

        StoryChapters[1].ChapterDialogues[1].speaker2ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[1].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[1].speaker2ExpressionIndex.Length; k++)
        {
            if (k == 9 || k == 19 || k == 20)
            {
                StoryChapters[1].ChapterDialogues[1].speaker2ExpressionIndex[k] = 1;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[1].speaker2ExpressionIndex[k] = 0;
            }
        }
        #endregion

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
        Debug.Log("c1d1 success");

        //At Home, 7:00 P.M.
        int i = 2;
        StoryChapters[1].ChapterDialogues[i] = new Dialogue();
        StoryChapters[1].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[i].otherName = "";
        StoryChapters[1].ChapterDialogues[i].sentences = new string[28];
        StoryChapters[1].ChapterDialogues[i].speaker1Lines = new int[] { 0, 2, 7, 15 };
        
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[i].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 4 || k == 5 || k == 6 || k == 15)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k > 15)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }


        }
        
        #endregion

        StoryChapters[1].ChapterDialogues[i].sentences[0] = "Finally. Done with my homework.";
        StoryChapters[1].ChapterDialogues[i].sentences[1] = "I looked over at the corner of my PC. The clock says 7:01 P.M.";
        StoryChapters[1].ChapterDialogues[i].sentences[2] = "I guess I could grind some bo---";
        StoryChapters[1].ChapterDialogues[i].sentences[3] = "I caught my cursor hovering over the icon of the game I once loved.";
        StoryChapters[1].ChapterDialogues[i].sentences[4] = "Right.";
        StoryChapters[1].ChapterDialogues[i].sentences[5] = "There’s no reason to log in to LL14.";
        StoryChapters[1].ChapterDialogues[i].sentences[6] = "…";
        StoryChapters[1].ChapterDialogues[i].sentences[7] = "I’ll check just in case.";
        StoryChapters[1].ChapterDialogues[i].sentences[8] = "And… nothing.";
        StoryChapters[1].ChapterDialogues[i].sentences[9] = "No notification.";
        StoryChapters[1].ChapterDialogues[i].sentences[10] = "I felt hollow.";
        StoryChapters[1].ChapterDialogues[i].sentences[11] = "Empty.";
        StoryChapters[1].ChapterDialogues[i].sentences[12] = "Nothing but silence.";
        StoryChapters[1].ChapterDialogues[i].sentences[13] = "I checked my friends list and…";
        StoryChapters[1].ChapterDialogues[i].sentences[14] = "…The name’s gone.";
        StoryChapters[1].ChapterDialogues[i].sentences[15] = "...Really?";
        StoryChapters[1].ChapterDialogues[i].sentences[16] = "Tch.";
        StoryChapters[1].ChapterDialogues[i].sentences[17] = "I shut off my PC and head to bed.";
        StoryChapters[1].ChapterDialogues[i].sentences[18] = "I’ll play later when I get home.";
        StoryChapters[1].ChapterDialogues[i].sentences[19] = "As I lay in silence, I remember the times we spent together.";
        StoryChapters[1].ChapterDialogues[i].sentences[20] = "All those grinds. We had a million gold bars all up in our mines.";
        StoryChapters[1].ChapterDialogues[i].sentences[21] = "And when we got to the end.";
        StoryChapters[1].ChapterDialogues[i].sentences[22] = "Everything felt right.";
        StoryChapters[1].ChapterDialogues[i].sentences[23] = "I wish you were here with me.";
        StoryChapters[1].ChapterDialogues[i].sentences[24] = "…To grind.";
        StoryChapters[1].ChapterDialogues[i].sentences[25] = "…";
        StoryChapters[1].ChapterDialogues[i].sentences[26] = "I spent about an hour or so just…";
        StoryChapters[1].ChapterDialogues[i].sentences[27] = "…Crying";
        Debug.Log("c1d2 success");


        //===A week later, at School===
        i = 3;
        StoryChapters[1].ChapterDialogues[i] = new Dialogue();
        StoryChapters[1].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[i].otherName = friendName;
        StoryChapters[1].ChapterDialogues[i].sentences = new string[22];
        StoryChapters[1].ChapterDialogues[i].speaker1Lines = new int[] { 3, 8, 12, 15, 19};
        StoryChapters[1].ChapterDialogues[i].speaker2Lines = new int[] { 0, 1, 6, 7, 9, 10 ,11, 13, 14, 16, 17, 18, 20, 21};

        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker2Sprites = NFSprites;
        StoryChapters[1].ChapterDialogues[i].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 5 || k == 6)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }

        }
        StoryChapters[1].ChapterDialogues[i].speaker2ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker2ExpressionIndex.Length; k++)
        {
            if (k == 9 || k == 10 || k == 21)
            {
                StoryChapters[1].ChapterDialogues[i].speaker2ExpressionIndex[k] = 1;
            }
            else if (k >= 2 && k <= 6)
            {
                StoryChapters[1].ChapterDialogues[i].speaker2ExpressionIndex[k] = 3;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker2ExpressionIndex[k] = 0;
            }
        }
        #endregion

        StoryChapters[1].ChapterDialogues[i].sentences[0] = "Hey! I haven’t seen you log on since last week.";
        StoryChapters[1].ChapterDialogues[i].sentences[1] = "You doin’ okay?";
        StoryChapters[1].ChapterDialogues[i].sentences[2] = "My blurry vision slowly comes into focus as I hear a familiar voice.";
        StoryChapters[1].ChapterDialogues[i].sentences[3] = "Ah, it’s you.";
        StoryChapters[1].ChapterDialogues[i].sentences[4] = "My memory is foggy of the last week.";
        StoryChapters[1].ChapterDialogues[i].sentences[5] = "I don’t remember much except…";
        StoryChapters[1].ChapterDialogues[i].sentences[6] = "I hope you’ll get through this. Whatever it is you’re going through right now.";
        StoryChapters[1].ChapterDialogues[i].sentences[7] = "Anyway, on another note, the event is out!";
        StoryChapters[1].ChapterDialogues[i].sentences[8] = "Event?";
        StoryChapters[1].ChapterDialogues[i].sentences[9] = "Yeah! The MagiHimeDive event is out.";
        StoryChapters[1].ChapterDialogues[i].sentences[10] = "There’s also a rate up character banner! She looks pretty good too.";
        StoryChapters[1].ChapterDialogues[i].sentences[11] = "She’s a red unit. So her primary focus is dealing damage.";
        StoryChapters[1].ChapterDialogues[i].sentences[12] = "Ah, but I’d have to spend money on it, right?";
        StoryChapters[1].ChapterDialogues[i].sentences[13] = "If you have free summons, you could spend that first.";
        StoryChapters[1].ChapterDialogues[i].sentences[14] = "See if you get her earlier";
        StoryChapters[1].ChapterDialogues[i].sentences[15] = "Alright. What’s the new mechanic?";
        StoryChapters[1].ChapterDialogues[i].sentences[16] = "Ah, yeah. It’s the Black Hexes.";
        StoryChapters[1].ChapterDialogues[i].sentences[17] = "You got to get rid of them before they stack up.";
        StoryChapters[1].ChapterDialogues[i].sentences[18] = "Once they do, the Boss deals an immense amount of damage";
        StoryChapters[1].ChapterDialogues[i].sentences[19] = "Okay. I’ll check it out later.";
        StoryChapters[1].ChapterDialogues[i].sentences[20] = "Yeah. Oh! I have to go now, I got some assignments to work on.";
        StoryChapters[1].ChapterDialogues[i].sentences[21] = "Till next time fam!";
        Debug.Log("c1d3 success");

        //===At Home, 7:34 P.M.===
        i = 4;
        StoryChapters[1].ChapterDialogues[i] = new Dialogue();
        StoryChapters[1].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[i].otherName = "";
        StoryChapters[1].ChapterDialogues[i].sentences = new string[10];
        StoryChapters[1].ChapterDialogues[i].speaker1Lines = new int[] { 2, 3, 9 };

        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[i].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k > 3)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }

        }
        
        #endregion

        StoryChapters[1].ChapterDialogues[i].sentences[0] = "The sound of knuckles cracking can be heard reverberating in the silent room.";
        StoryChapters[1].ChapterDialogues[i].sentences[1] = "I finish stretching and sit on my bed.";
        StoryChapters[1].ChapterDialogues[i].sentences[2] = "Finally done with my assignment.";
        StoryChapters[1].ChapterDialogues[i].sentences[3] = "As I grab my phone, I subconsciously check my notifications for any messages.";
        StoryChapters[1].ChapterDialogues[i].sentences[4] = "Of course, knowing them, there isn’t one.";
        StoryChapters[1].ChapterDialogues[i].sentences[5] = "Not even a full explanation of why they did what they did.";
        StoryChapters[1].ChapterDialogues[i].sentences[6] = "Not even an apology.";
        StoryChapters[1].ChapterDialogues[i].sentences[7] = "I felt my blood boiling at the thought, or lack thereof.";
        StoryChapters[1].ChapterDialogues[i].sentences[8] = "So I left that with a sigh.";
        StoryChapters[1].ChapterDialogues[i].sentences[9] = "Alright, I guess I’ll start the event.";
        Debug.Log("c1d4 success");

        //World01 Boss, Attempt 01 - Leads to Failure//
        i = 5;
        StoryChapters[1].ChapterDialogues[i] = new Dialogue();
        StoryChapters[1].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[i].otherName = "";
        StoryChapters[1].ChapterDialogues[i].sentences = new string[16];
        StoryChapters[1].ChapterDialogues[i].speaker1Lines = new int[] { 0,1,2,3,4,6, 7, 11, 12,13,14,15};

        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[1].ChapterDialogues[i].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 0 || k == 1)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else if (k == 4)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 2 || k == 3)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else if (k >= 12)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 1;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }

        }

        #endregion

        StoryChapters[1].ChapterDialogues[i].sentences[0] = "Damn.";
        StoryChapters[1].ChapterDialogues[i].sentences[1] = "This new mechanic is getting on my nerves.";
        StoryChapters[1].ChapterDialogues[i].sentences[2] = "Who thought it was a good idea to just have a map filled almost entirely with black hexes?!";
        StoryChapters[1].ChapterDialogues[i].sentences[3] = "Have like, what, only a few turns before the boss nukes my entire team?";
        StoryChapters[1].ChapterDialogues[i].sentences[4] = "Ugh!!!";
        StoryChapters[1].ChapterDialogues[i].sentences[5] = "As I fell to my bed, memories of the previous conversation with Hal rang in my head.";
        StoryChapters[1].ChapterDialogues[i].sentences[6] = "It’s a new month.";
        StoryChapters[1].ChapterDialogues[i].sentences[7] = "I have…";
        StoryChapters[1].ChapterDialogues[i].sentences[8] = "I immediately scrolled through the menu of the game.";
        StoryChapters[1].ChapterDialogues[i].sentences[9] = "Using the resources I got from playing through the tutorial to this point in the game, I tried pulling for the event character.";
        StoryChapters[1].ChapterDialogues[i].sentences[10] = "No luck. But...";
        StoryChapters[1].ChapterDialogues[i].sentences[11] = "With just a few more gems…"; 
        //TODO: (a few minutes later) FADE TO BLACK AND BACK
        StoryChapters[1].ChapterDialogues[i].sentences[12] = "Almost maxed out. But it should be strong enough.";
        StoryChapters[1].ChapterDialogues[i].sentences[13] = "Her stuns should come much more frequently now.";
        StoryChapters[1].ChapterDialogues[i].sentences[14] = "I also should try to chain Blue more often. She has a damage buff that makes her on par, or even stronger than the Red units.";
        StoryChapters[1].ChapterDialogues[i].sentences[15] = "Im back.";
        Debug.Log("c1d5 success");


        //World01 Boss, Attempt 02 - Post-fight//
        i = 6;
        StoryChapters[1].ChapterDialogues[i] = new Dialogue();
        StoryChapters[1].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[1].ChapterDialogues[i].otherName = "";
        StoryChapters[1].ChapterDialogues[i].sentences = new string[19];
        StoryChapters[1].ChapterDialogues[i].speaker1Lines = new int[] { 0, 1, 2, 4, 6, 7, 8, 18 };

        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[1].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[1].ChapterDialogues[i].speaker2Lines = new int[] { 9 };
        StoryChapters[1].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        //StoryChapters[1].ChapterDialogues[i].speaker2Sprites = McSprites;

        StoryChapters[1].ChapterDialogues[i].chapterNum = 1;
        StoryChapters[1].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[1].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 18)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else if (k == 17)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k <= 3 || k == 6)
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 1;
            }
            else
            {
                StoryChapters[1].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }

        }

        #endregion

        StoryChapters[1].ChapterDialogues[i].sentences[0] = "YES!";
        StoryChapters[1].ChapterDialogues[i].sentences[1] = "HELL YEAH!!";
        StoryChapters[1].ChapterDialogues[i].sentences[2] = "SUCK IT!!!";
        StoryChapters[1].ChapterDialogues[i].sentences[3] = "Five knocks from my ceiling echoed throughout my room.";
        StoryChapters[1].ChapterDialogues[i].sentences[4] = "SORRY!";
        StoryChapters[1].ChapterDialogues[i].sentences[5] = "I let out a sigh of relief.";
        StoryChapters[1].ChapterDialogues[i].sentences[6] = "Man, that’s fifteen dollars well spent.";
        StoryChapters[1].ChapterDialogues[i].sentences[7] = "She made that fight so much easier.";
        StoryChapters[1].ChapterDialogues[i].sentences[8] = "Her ability to clear those hexes saved me.";
        StoryChapters[1].ChapterDialogues[i].sentences[9] = "We did it! Let’s go eat some hamburgers! I’ll cook some for ya!";//PHONE - G-UNIT01
        StoryChapters[1].ChapterDialogues[i].sentences[10] = "As my eyes read the words on the screen, I am taken back to a time when I used to wait expectantly at a dinner table.";
        StoryChapters[1].ChapterDialogues[i].sentences[11] = "Their cooking.";
        StoryChapters[1].ChapterDialogues[i].sentences[12] = "Dinner dates with them in their home.";
        StoryChapters[1].ChapterDialogues[i].sentences[13] = "Helping them out in the kitchen.";
        StoryChapters[1].ChapterDialogues[i].sentences[14] = "Cutting onions.";
        StoryChapters[1].ChapterDialogues[i].sentences[15] = "…";
        StoryChapters[1].ChapterDialogues[i].sentences[16] = "As these memories rushed through my head, I closed my phone.";
        StoryChapters[1].ChapterDialogues[i].sentences[17] = "Put my head to my pillow and just…";
        StoryChapters[1].ChapterDialogues[i].sentences[18] = "GAAAAAAAAH!!!";
        Debug.Log("c1d6 success");

        #endregion

        #region Chapter2
        //Bargaining//
        int j = 2;
        StoryChapters[j].ChapterTitle = "Bargaining";
        StoryChapters[j].ChapterDialogues = new Dialogue[5];

        //===At Home, 11:30 A.M.===
        i = 0;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[18];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 4,5,6,7,14,17 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
        }

        #endregion

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "It’s been two months since I started playing this game.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "I’ve made progress with the story, but I’ve reached a roadblock.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "Even with the units I had, I can’t progress past it.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "Hal has stopped playing the game, so his unit is currently underleveled."; //NEED ALARM SOUND EX
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "I won’t be using his support unit at this point.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "Not like I’ve been using it after the first three weeks of playing.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "He introduced me to the game, but he’s the one who stopped.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Whatever.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "I scrolled through forums.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Chatted with other active players, talked about various chokepoints and theorycrafting teams and tested them.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "I was in deep.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "As I continued to read, news about the newest event came up.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "There was a new unit that will help me get past the chokepoint.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "I checked the comments in the forums, and everyone said the same thing.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "This new limited character is going to be the key to pass the gate.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "One more week.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "The banner will be up one more week.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "Guess I’ll work on my assignments for now.";

        Debug.Log("c2d0 success");


        //===At School, One week later, 4:45 P.M.===
        i = 1;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = friendName;
        StoryChapters[j].ChapterDialogues[i].sentences = new string[42];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 5, 6, 7, 8, 9,10,11,20,21,22,23,33,34,36,37,39,40 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[j].ChapterDialogues[i].speaker2Lines = new int[] { 0,1,2,17,18,19,27,28,29,30,31,35,38 };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = NFSprites;

        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if ((k >= 2 && k < 5) || k == 7 || k == 35)
            {
                //annoyed
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 10)
            {
                //happy
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 1;
            }
            else if (k == 8 || k == 9)
            {
                //disgust
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else if (k == 1)
            {
                //embarrassed
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 2;
            }
            else if ((k >= 20 && k < 24) || k >= 33) 
            {
                //angry
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else
            {
                //neutral
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }
        }

        StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex.Length; k++)
        {
            if ((k >= 2 && k < 5) ||(k >= 9 && k < 25))
            {
                //worry
                StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex[k] = 3;
            }
            else if (k == 0)
            {
                //happy
                StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex[k] = 1;
            }
            else if (k >= 25)
            {
                //sad
                StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex[k] = 2;
            }
            else
            {
                StoryChapters[j].ChapterDialogues[i].speaker2ExpressionIndex[k] = 0;
            }
            
        }

        #endregion

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Hey!";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "How have you been? You haven’t been hanging out at the usual spot for the past two weeks.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "Anything new happened during that time?";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "Ah. Right."; 
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "He’s here.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "There’s a new event in game.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "More mechanics are getting added to it.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Still haven’t managed to get the unit that you have.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "And chapter 7 is a huge bottleneck for players.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "I’ve spent around 250$ for this month already.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "My account is already stronger than yours.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "Speaking of which, When are you going to come back to the game?";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "Hal was stunned."; //SHOW HAL stunned face
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "He was just standing there.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Staring at me like I’m some sort of freak.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "He’s the freaky one.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "Who just abandons their friend while they’re the ones who made them go down that route anyway?";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "Wait, let’s rollback a few steps.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "Two hundred and fifty dollars?";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "You spent that much?";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "You’re judging me?";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "You’re the one with the broken unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "You must’ve spent thousands of dollars on the first day.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "You’re also the one who mentioned me spending my money on this game instead of LL14.";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "Silence.";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "He just gave me this disappointed look."; //show hal sad face
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "It was as if I said something horrible.";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "...";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "And that was a mistake.";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "I just got lucky on the first day and managed to pull that unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[30] = "And no, I haven’t spent any money at all in games like these ever since I got in too deep.";
        StoryChapters[j].ChapterDialogues[i].sentences[31] = "...Which seems to be the situation you’re in already.";
        StoryChapters[j].ChapterDialogues[i].sentences[32] = "He gave me this pitied look.";
        StoryChapters[j].ChapterDialogues[i].sentences[33] = "...That’s a lie.";
        StoryChapters[j].ChapterDialogues[i].sentences[34] = "There’s no way that was just luck.";
        StoryChapters[j].ChapterDialogues[i].sentences[35] = "I’m sorry.";
        StoryChapters[j].ChapterDialogues[i].sentences[36] = "No way you haven’t spent money in this game.";
        StoryChapters[j].ChapterDialogues[i].sentences[37] = "There is no way.";
        StoryChapters[j].ChapterDialogues[i].sentences[38] = "I think you should get some-";
        StoryChapters[j].ChapterDialogues[i].sentences[39] = "No. Shut it.";
        StoryChapters[j].ChapterDialogues[i].sentences[40] = "I’m leaving.";
        StoryChapters[j].ChapterDialogues[i].sentences[41] = "That was the last time I saw him.";
        Debug.Log("c2d1 success");


        //===At Home, 5:23 P.M.===
        i = 2;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = phoneName;
        StoryChapters[j].ChapterDialogues[i].sentences = new string[50];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 10,12,13,14,15,18,23,24,42,46,47,48,49 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[j].ChapterDialogues[i].speaker2Lines = new int[] { 28,29,30,31};
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = PhoneSprites;

        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "I’ve been staring at my phone for a few minutes now.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "What was he thinking about?";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "He’s the one who started this.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "It’s his fault.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "Still.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "Enough about him.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "This new unit looks pretty good.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Although with a new unit…";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "…Means a new mechanic.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "An alert goes off."; //NOTIF ALERT sOUND
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "The update just started.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "I start scrolling through the forums reading up on the event.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "And the new mechanic is…"; 
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "...Pain hex.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Any color can be chained with the Pain hex, and upon activation, it deals damage to the units.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "And the new unit heals the team with their skill, huh.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "An alert takes me out of my thoughts for a split second.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "My phone.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "Is the download complete already?";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "It is.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "And…";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "…It’s a text from that one person.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "My ex.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "What…";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "It’s already been months and they suddenly text me out of nowhere?";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "Do they want to get back together?";
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "Is it just an apology?";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "Filled with all these thoughts, I hesitantly open the text.";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "Hey, it’s me.";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "I know it’s been a long time since I last messaged you but I just want to clear things up about before.";
        StoryChapters[j].ChapterDialogues[i].sentences[30] = "Text me when you get this message";
        StoryChapters[j].ChapterDialogues[i].sentences[31] = "And just like that, my hands started to shake.";
        StoryChapters[j].ChapterDialogues[i].sentences[32] = "What do I say?";
        StoryChapters[j].ChapterDialogues[i].sentences[33] = "“Hey, want to get back together?” or “Don’t text me at all.”";
        StoryChapters[j].ChapterDialogues[i].sentences[34] = "Crap, should I play hard to get?";
        StoryChapters[j].ChapterDialogues[i].sentences[35] = "What if they just want to hurt me some more?";
        StoryChapters[j].ChapterDialogues[i].sentences[36] = "How many words should I say?";
        StoryChapters[j].ChapterDialogues[i].sentences[37] = "How long should my reply be?";
        StoryChapters[j].ChapterDialogues[i].sentences[38] = "Filled with various thoughts in my head, I started to panic.";
        StoryChapters[j].ChapterDialogues[i].sentences[39] = "In the end. All I said was: “Got it.”";
        StoryChapters[j].ChapterDialogues[i].sentences[40] = "That’s a good response right?";
        StoryChapters[j].ChapterDialogues[i].sentences[41] = "Not too serious, not too desperate.";
        StoryChapters[j].ChapterDialogues[i].sentences[42] = "Man. I just spent the money for our anniversary in this game.";
        StoryChapters[j].ChapterDialogues[i].sentences[43] = "And another alert rings."; //alert sfx
        StoryChapters[j].ChapterDialogues[i].sentences[44] = "It's the update notification.";
        StoryChapters[j].ChapterDialogues[i].sentences[45] = "It’s finished.";
        StoryChapters[j].ChapterDialogues[i].sentences[46] = "Ah.";
        StoryChapters[j].ChapterDialogues[i].sentences[47] = "I guess I’ll play the event while I wait for a reply";
        StoryChapters[j].ChapterDialogues[i].sentences[48] = "Alright. Got my login rewards, upgraded my unit’s skills.";
        StoryChapters[j].ChapterDialogues[i].sentences[49] = "Let’s see how far I can go without the new unit.";
        Debug.Log("c2d2 success");



        //World02 Boss, Attempt 01 - Failure//
        i = 3;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[30];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0,1,2,8,9,12,13,14,15,16,17,18,24,25,28,29};
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 12)
            {
                //annoyed
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k >= 24)
            {
                //happy
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 1;
            }
            else if (k == 2 || k == 13)
            {
                //disgust
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else if (k == 1 || k == 0 || k == 18)
            {
                //angry
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else
            {
                //neutral
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }
        }
        #endregion

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Damn. Really?";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "I’m not healing enough for this.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "Tsk, I guess I need to get that event unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "I start navigating through the menu of the game. ";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "My fingers hover over the buttons before they even appear.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "They know where to go.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "They know when to press.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "They know what to do.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "Ah.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Right, I still have the free gems from the earlier levels."; 
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "I use them.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "However…";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "I knew it. It’s not enough.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "Tsk. Guess I’ll go for the fifty dollar pack.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Come on.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "Come home.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "Please be the six star.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "Event limited unit?";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "Aaaaagh. I got a dupe of the bad one.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "Summon.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "After summon.";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "After more summons.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "But it wasn’t enough.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "It never is.";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "GOT IT!";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "I can finally clear it!";
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "I ended up spending three hundred and fifty dollars for the key unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "Was it…";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "Alright!";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "Maxed out. Time to use her.";
        Debug.Log("c2d3 success");



        //World02 Boss, Attempt 02 - Post-fight//
        i = 4;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = phoneName;
        StoryChapters[j].ChapterDialogues[i].sentences = new string[46];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 1,2,4,5,6,29,35,44,45 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[j].ChapterDialogues[i].speaker2Lines = new int[] { 11,12,13,14,15,16,17,18,19,20,21,22,23,24,37,38,39,40,41,42 };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = PhoneSprites;

        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "As I laid on my bed, I stared at the ceiling.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "The event’s done.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "I cleared it.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "I took a look at the screen.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "It’s over.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "Haha…";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "...Hahaha…";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "I was tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "Exhausted.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Ah.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "A notification."; //notif sfx
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "The way we ended things wasn’t that great.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "I’m sorry I did things that way.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "It just felt like you weren’t really supporting me in my endeavors.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "I was stressed.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "I was getting tired of the same old routine.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "All that came out of your mouth was just about playing Last Legend 14.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "Don’t get me wrong, it was fun.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "It helped me get my mind off of things.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "Being with you was fun.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "But fun isn’t all I really want our relationship to be.";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "So I was thinking. Maybe we should try again?";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "We’ll do things differently this time.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "I mean, if you’d like to. ";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "You know what? Wait a few minutes.";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "And so I waited.";
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "And waited.";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "And waited…";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "…Then I realized that message was sent an hour ago.  ";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "W-wait…";
        StoryChapters[j].ChapterDialogues[i].sentences[30] = "Twelve missed calls.";
        StoryChapters[j].ChapterDialogues[i].sentences[31] = "Twelve.";
        StoryChapters[j].ChapterDialogues[i].sentences[32] = "I was…";
        StoryChapters[j].ChapterDialogues[i].sentences[33] = "…in game.";
        StoryChapters[j].ChapterDialogues[i].sentences[34] = "I missed it.";
        StoryChapters[j].ChapterDialogues[i].sentences[35] = "Ah! But maybe I can still-";
        StoryChapters[j].ChapterDialogues[i].sentences[36] = "A notification.";
        StoryChapters[j].ChapterDialogues[i].sentences[37] = "You know what?";
        StoryChapters[j].ChapterDialogues[i].sentences[38] = "Nevermind what I said.";
        StoryChapters[j].ChapterDialogues[i].sentences[39] = "I’m sorry I thought you weren’t busy.";
        StoryChapters[j].ChapterDialogues[i].sentences[40] = "But it seems like you’re unable to text me back.";
        StoryChapters[j].ChapterDialogues[i].sentences[41] = "Even after all this time we’ve had to think about ourselves.";
        StoryChapters[j].ChapterDialogues[i].sentences[42] = "Go play your games.";
        StoryChapters[j].ChapterDialogues[i].sentences[43] = "You cannot reply to this conversation.";
        StoryChapters[j].ChapterDialogues[i].sentences[44] = "I…";
        StoryChapters[j].ChapterDialogues[i].sentences[45] = "...Messed up.";
        Debug.Log("c2d4 success");

        //Bargaining - End//


        #endregion

        #region Chapter3
        //Depression//
        j = 3;
        StoryChapters[j].ChapterTitle = "Depression";
        StoryChapters[j].ChapterDialogues = new Dialogue[4];

        //===A few months later. At Home===
        i = 0;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[9];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0, 1, 2, 3, 4, 7, 8 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k == 4)
            {
                //annoyed
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 0 || k == 2)
            {
                //disgust
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else if (k == 1)
            {
                //angry
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else
            {
                //neutral
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }
        }
        #endregion

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Tsk. Wrong team.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "I’ll try aga-- FUCK!!";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "I’m out of stamina.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "I’ll just---";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "Gems too.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "My fingers quickly move to purchase more gems.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "As the transaction finalizes, a notification appears on my phone.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Good. There’s a buyer for my PC.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "I’ll reply to them as soon as I finish this level";


        //World03 Boss, Attempt01 - Failure//
        i = 1;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[17];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0, 1, 2, 3, 16 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        #region Expressions
        StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex = new int[StoryChapters[j].ChapterDialogues[i].sentences.Length];
        for (int k = 0; k < StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex.Length; k++)
        {
            if (k > 10)
            {
                //annoyed
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 4;
            }
            else if (k == 1 || k == 2 || k == 3)
            {
                //disgust
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 3;
            }
            else if (k == 0)
            {
                //angry
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 5;
            }
            else
            {
                //neutral
                StoryChapters[j].ChapterDialogues[i].speaker1ExpressionIndex[k] = 0;
            }
        }
        #endregion

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Damn it.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "Those locks are getting in my way.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "I can’t chain the hexes long enough.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "I’m always short on damage.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "Then I remembered…";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "…There’s an event unit that will fix this problem.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "However, their full strength will require multiple copies.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "As I stare at the failure screen, my mind gets filled with thoughts about all the mistakes I made.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "All the failures.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Every attempt to do the right thing.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "I tried.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "But it wasn’t enough.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "It’s all…";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "…So tiring.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "My phone alarms."; //phone alarm
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "I should reply to that person.";


        //===Five hours later, at Home===
        i = 2;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[16];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0, 8,9,11,15 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;


        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Yep! Alright! Enjoy the PC!";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "Ah.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "That’s it.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "That’s the last thing I can sell.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "I now have the money to spend for the unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "I looked at my phone screen.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "It shows the summon banner for the limited unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "As my lips curled to a faint smile, my voice creeped out.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "You will be mine.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "I will make you mine.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "Maxed out. Levels, overlevels, stats, everything.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "I laid on the floor of my room.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "My strongest unit.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "In this tiny device.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "I’m coming back for another round…";


        //World03 Boss, Attempt02 - Postfight//
        i = 3;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[35];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 15,16,17,18,19 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "And that’s it.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "That’s the event cleared.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "I’ve gotten everything I wanted.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "But why do I feel so hollow?";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "I just cleared the hardest event ever made in the game.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "I am in the leaderboards.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "There’s a huge gap between me and all the other players.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "I’m the strongest.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "I’m at the top.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "I’m at the peak of it all.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "This is what I wanted.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "I asked for this and I got it.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "But why?";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Why does it feel like I’ve accomplished nothing at all?";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "Why do I still feel empty?";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "I put everything I had into this game.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "I sold all that I could sell.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "All my time, my effort.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "All of it.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "I don’t know anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "I'm tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "I’m so tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "I just want to end it all.";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "What else can I do?";
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "I’m a drop out.";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "I lost my lover.";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "I have no friends.";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "I don’t know anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[30] = "I haven’t talked to anyone in a long time.";
        StoryChapters[j].ChapterDialogues[i].sentences[31] = "I haven’t gone outside for more than an hour.";
        StoryChapters[j].ChapterDialogues[i].sentences[32] = "I can’t think straight.";
        StoryChapters[j].ChapterDialogues[i].sentences[33] = "I can’t think anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[34] = "I just want to…";
        //Depression - End//
        //TODO: on end load weak puzzle scene

        #endregion

        #region Chapter4
        //Acceptance//
        j = 4;
        StoryChapters[j].ChapterTitle = "Acceptance";
        StoryChapters[j].ChapterDialogues = new Dialogue[4];

        //TODO: load first dialogue when first weak puzzle is done 
        //After a game
        i = 0;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[2];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0, 1};
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Play...";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "...some more..";

        //After 2nd game
        i = 1;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[2];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Why am I not satified?!";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "I need to forget every...";
        //TODO: load third game

        //during third game - ingame dialogues
        i = 2;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[25];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] {};
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Alone.";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "The curtains are closed.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "There’s just darkness all around.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "I don't know how long it has been since I last left the room.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "I’ve just been playing nonstop.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "My phone’s the only thing that keeps me held.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "My brain’s been in a fog lately.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "All I think about is just connecting hexes of similar colors.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "Hit. Heal. Stun.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Hit. Heal. Stun.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "Same movements.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "A swipe of the finger.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "Swish. Swoosh.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "Attack.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Survive.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "…Tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "…I’m so tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "…I just want to close my eyes.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "I just want to sleep.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "…I’m so tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "I just want it all to fade away.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "I’m so tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "Just…";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "…Let me rest.";


        //during fourth game (final boss) - ingame dialogues
        i = 3;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = mainCharacterName;
        StoryChapters[j].ChapterDialogues[i].otherName = "";
        StoryChapters[j].ChapterDialogues[i].sentences = new string[61];
        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;
        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "What is this?";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "A new event?";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "Oh well.";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "Just keep swiping.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "Hit. Stun. Heal.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "The longest chain.";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Not the shortest.";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "Needs more hexes.";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Chain it together.";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "Stun it.";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "Damage it.";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "Heal.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "Use pain hexes in conjunction with the white hexes to negate damage.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "Reds.";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "Blues.";
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "Work around the locked hexes.";
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "Longer.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "If only there were an extra two to chain it with.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "Tsk.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "Is it dying?";
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "More damage.";
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "I need more hurt.";
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "Can it die?";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "Black hexes are annoying.";
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "Maybe I should try stalling.";
        StoryChapters[j].ChapterDialogues[i].sentences[26] = "Can it just stop?";
        StoryChapters[j].ChapterDialogues[i].sentences[27] = "How long do I have to do this?";
        StoryChapters[j].ChapterDialogues[i].sentences[28] = "Will it end?";
        StoryChapters[j].ChapterDialogues[i].sentences[29] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[30] = "……";
        StoryChapters[j].ChapterDialogues[i].sentences[31] = "………";

        //FROM HERE TEXT START TO GO TO CENTER SLOWELY
        //THE SCREEN WILL ALSO FADE TO BLACK PER INDEX
        //TEXT SHOULD STILL BE VISIBLE
        StoryChapters[j].ChapterDialogues[i].sentences[32] = "I don’t know anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[33] = "I’m tired.";
        StoryChapters[j].ChapterDialogues[i].sentences[34] = "Why?";
        StoryChapters[j].ChapterDialogues[i].sentences[35] = "Why did I do this?";
        StoryChapters[j].ChapterDialogues[i].sentences[36] = "Why do I keep making mistakes?";
        StoryChapters[j].ChapterDialogues[i].sentences[37] = "I just want it to end.";
        StoryChapters[j].ChapterDialogues[i].sentences[38] = "Let me rest.";
        StoryChapters[j].ChapterDialogues[i].sentences[39] = "Let me go.";
        StoryChapters[j].ChapterDialogues[i].sentences[40] = "I can’t do this anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[41] = "I’m just making the same mistakes.";
        StoryChapters[j].ChapterDialogues[i].sentences[42] = "Over and over again.";
        StoryChapters[j].ChapterDialogues[i].sentences[43] = "I failed my partner.";
        StoryChapters[j].ChapterDialogues[i].sentences[44] = "I failed my friends.";
        StoryChapters[j].ChapterDialogues[i].sentences[45] = "I failed my family.";
        StoryChapters[j].ChapterDialogues[i].sentences[46] = "I failed my education.";
        StoryChapters[j].ChapterDialogues[i].sentences[47] = "I’m just so…";
        StoryChapters[j].ChapterDialogues[i].sentences[48] = "…Alone";
        StoryChapters[j].ChapterDialogues[i].sentences[49] = "I can’t anymore.";
        StoryChapters[j].ChapterDialogues[i].sentences[50] = "I..";
        StoryChapters[j].ChapterDialogues[i].sentences[51] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[52] = "I…";
        StoryChapters[j].ChapterDialogues[i].sentences[53] = "I need…";
        StoryChapters[j].ChapterDialogues[i].sentences[54] = "…";
        StoryChapters[j].ChapterDialogues[i].sentences[55] = "I need help.";
        //faded almost completely
        StoryChapters[j].ChapterDialogues[i].sentences[56] = "I need to stop.";
        StoryChapters[j].ChapterDialogues[i].sentences[57] = "I need to change.";
        StoryChapters[j].ChapterDialogues[i].sentences[58] = "Someone.";
        StoryChapters[j].ChapterDialogues[i].sentences[59] = "Please.";
        //black screen at this point
        StoryChapters[j].ChapterDialogues[i].sentences[60] = "Help me.";
        //Acceptance - End//

        #endregion

        #region Epilogue
        //Epilogue//
        j = 5;
        StoryChapters[j].ChapterTitle = "Epilogue";
        StoryChapters[j].ChapterDialogues = new Dialogue[1];

        //after final boss - black screen back to school
        i = 0;
        StoryChapters[j].ChapterDialogues[i] = new Dialogue();
        StoryChapters[j].ChapterDialogues[i].name = olderMan;
        StoryChapters[j].ChapterDialogues[i].otherName = woman;
        StoryChapters[j].ChapterDialogues[i].sentences = new string[26];

        StoryChapters[j].ChapterDialogues[i].speaker1Lines = new int[] { 0, 1,2,3,4,5,6,8,9,11,14,15,16,17,22 };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker1Sprites = McSprites;

        StoryChapters[j].ChapterDialogues[i].speaker2Lines = new int[] { 7,10,12,13,18,19,20,21,23,24,25 };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = new Sprite[] { };
        StoryChapters[j].ChapterDialogues[i].speaker2Sprites = NFSprites;

        StoryChapters[j].ChapterDialogues[i].chapterNum = j;
        StoryChapters[j].ChapterDialogues[i].dialogueIndex = i;

        StoryChapters[j].ChapterDialogues[i].sentences[0] = "Hey! You finished yet?";
        StoryChapters[j].ChapterDialogues[i].sentences[1] = "Nope. The level’s hard.";
        StoryChapters[j].ChapterDialogues[i].sentences[2] = "I can’t get past this boss.";
        StoryChapters[j].ChapterDialogues[i].sentences[3] = "Use my Graydust to clear the level!";
        StoryChapters[j].ChapterDialogues[i].sentences[4] = "His skillset should help sweep the board.";
        StoryChapters[j].ChapterDialogues[i].sentences[5] = "Man, I wish I had OP units like you do.";
        StoryChapters[j].ChapterDialogues[i].sentences[6] = "Maybe I should-";
        StoryChapters[j].ChapterDialogues[i].sentences[7] = "Do you have spare money?";
        StoryChapters[j].ChapterDialogues[i].sentences[8] = "Huh?";
        StoryChapters[j].ChapterDialogues[i].sentences[9] = "Professor? No. I don’t. But I could sell-";
        StoryChapters[j].ChapterDialogues[i].sentences[10] = "Is it something important?";
        StoryChapters[j].ChapterDialogues[i].sentences[11] = "Well, yes but-";
        StoryChapters[j].ChapterDialogues[i].sentences[12] = "Then keep it.";
        StoryChapters[j].ChapterDialogues[i].sentences[13] = "Don’t spend your money on games like these unless you have the luxury of spare cash lying around.";
        StoryChapters[j].ChapterDialogues[i].sentences[14] = "It’s my choice though?";
        StoryChapters[j].ChapterDialogues[i].sentences[15] = "Sir has a point.";

        //some other guy
        StoryChapters[j].ChapterDialogues[i].sentences[16] = "I know rolling for units can be addictive. I had a friend who almost went in debt trying to get his waifu to max.";//other man
        
        
        StoryChapters[j].ChapterDialogues[i].sentences[17] = "It is possible.";
        StoryChapters[j].ChapterDialogues[i].sentences[18] = "You can end up spending too much money, and even money you don’t have just for a moment of temporary satisfaction.";
        StoryChapters[j].ChapterDialogues[i].sentences[19] = "Always remember to keep in mind the truly important things.";
        StoryChapters[j].ChapterDialogues[i].sentences[20] = "A phone rings."; //phone rings sfx

        //???
        StoryChapters[j].ChapterDialogues[i].sentences[21] = "Hey. We have a meeting in five minutes.";

        //man and woman
        StoryChapters[j].ChapterDialogues[i].sentences[22] = "Professor Harvard!";

        //halvard
        StoryChapters[j].ChapterDialogues[i].sentences[23] = "It’s Halvard. An L. Not an R.";
        StoryChapters[j].ChapterDialogues[i].sentences[24] = "Anyway, let’s go, Yuuki. We still have to bring some coffee for the others.";

        //yuuki
        StoryChapters[j].ChapterDialogues[i].sentences[25] = "Right.";

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
        FindObjectOfType<AudioManager>().Play("YouFarAwayBGM", "bgm", true);
    }

    // Update is called once per frame
    void Update()
    {
        //IF PREVIOUS SCENE IS A DIFFERENT SCENE, PUT IT HERE
        //IF DIALOGUE JUST INITIALIZED, THEN DO THESE IF STATEMENTS DURING UPDATE
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {

            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1], true);
                FindObjectOfType<AudioManager>().Stop("YouFarAwayBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("FruitsofLazinessBGM", "bgm", true);
            }

        }
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2], true);
                FindObjectOfType<AudioManager>().Stop("SandCollegeBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("AirConSFX", "sfx", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
                 FindObjectOfType<StoryManager>().currentDialogue == 4)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("SkyLeadingHomeBGM", "bgm", true);
            }

        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1], true);
                FindObjectOfType<AudioManager>().Stop("BirdsSFX", "sfx");
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[2].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[2], true);
                FindObjectOfType<AudioManager>().Stop("SandCollegeBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("AirConSFX", "sfx", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[3].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[3], true);
                FindObjectOfType<AudioManager>().Stop("YouFarAwayBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("FruitsofLazinessBGM", "bgm", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 4)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4], true);
                FindObjectOfType<AudioManager>().Stop("FruitsofLazinessBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("KnucklesCrackingSFX", "sfx", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[5].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[5], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("DefeatSFX", "sfx", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 6)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[6].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[6], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("VictorySFX", "sfx", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 2 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[1], true);
                FindObjectOfType<AudioManager>().Stop("FruitsofLazinessBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("SandCollegeBGM", "bgm", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 2 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[2].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[2], true);
                FindObjectOfType<AudioManager>().Stop("AngryBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("AirConSFX", "sfx", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 2 &&
            FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[3].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[3], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("DefeatSFX", "sfx", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 2 &&
            FindObjectOfType<StoryManager>().currentDialogue == 4)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[4].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[4], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("VictorySFX", "bgm", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[1], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("DefeatSFX", "sfx", false);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3], true);
                FindObjectOfType<AudioManager>().Stop("CuriosityBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("VictorySFX", "sfx", false);
                FindObjectOfType<AudioManager>().Play("AirConSFX", "sfx", true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 4 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[0].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[0], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 4 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[1], true);
            }
        }
        //ONLY FOR 2ndTO LAST LEVEL
        if (FindObjectOfType<StoryManager>().currentChapter == 4 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[2].isDone)
            {
                
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Puzzle.is2ndLastLevel = true;
                Values.Puzzle.isFinalLevel = false;
                Values.Enemy.enemyLevel = 0;
                Values.Enemy.maxHP = 10000;
                Values.Enemy.dmg = 0.01f;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.01f;
                Values.Puzzle.BlackHexBurstDamage = 1.0f;
                Values.Puzzle.hexBlockerCount = 1;//

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[2].hasTriggered = true;
                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[2].isDone = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("YouFarAwayBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("TheWorldIsGrayBGM", "bgm", true);
            }
        }
        //FOR FINAL LEVEL
        if (FindObjectOfType<StoryManager>().currentChapter == 4 &&
            FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[3].isDone)
            {

                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Puzzle.is2ndLastLevel = false;
                Values.Puzzle.isFinalLevel = true;
                Values.Enemy.enemyLevel = 4;
                Values.Enemy.maxHP = 10000000000;
                Values.Enemy.dmg = 0.0f;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.01f;
                Values.Puzzle.BlackHexBurstDamage = 1.0f;
                Values.Puzzle.hexBlockerCount = 3;//

                //set normal values
                Values.Player.setStunAmount = 0;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 0;

                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[3].hasTriggered = true;
                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[3].isDone = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 5 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.ClassroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[5].ChapterDialogues[0].isDone)
            {

                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[5].ChapterDialogues[0], true);

                FindObjectOfType<AudioManager>().Stop("TheWorldIsGrayBGM", "bgm");
                FindObjectOfType<AudioManager>().Play("SandCollegeBGM", "bgm", true);

            }
        }

    }
}
