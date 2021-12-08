using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instanceRef;
    public List<StoryChapter> StoryChapters = new List<StoryChapter>(MAX_CHAPTERS);
    public bool isOnDialogue = false;

    public int currentChapter = 0;
    public int currentDialogue = 0;

    private const int MAX_CHAPTERS = 2;
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
        StoryChapters[0].ChapterDialogues = new Dialogue[10];

        //along in dimly lit room
        //chapter 1 - dialogue 1 - only 1 speaker
        StoryChapters[0].ChapterDialogues[0] = new Dialogue();
        StoryChapters[0].ChapterDialogues[0].name = "MC";
        StoryChapters[0].ChapterDialogues[0].sentences = new string[6];
        StoryChapters[0].ChapterDialogues[0].sentences[0] = "Well then… I guess that’s it for us then.";
        StoryChapters[0].ChapterDialogues[0].sentences[1] = "Five years, gone, just like that.";
        StoryChapters[0].ChapterDialogues[0].sentences[2] = "...";
        StoryChapters[0].ChapterDialogues[0].sentences[3] = "It’s only been a week and...";
        StoryChapters[0].ChapterDialogues[0].sentences[4] = "*sigh*";
        StoryChapters[0].ChapterDialogues[0].sentences[5] = "Time for school I guess.";
        //fade to black i guess...

        //goes to school and meets new friend 
        //chapter 1 - dialogue 2 - 2 speakers
        StoryChapters[0].ChapterDialogues[1] = new Dialogue();
        StoryChapters[0].ChapterDialogues[1].name = "MC";
        StoryChapters[0].ChapterDialogues[1].otherName = "NewFriend";
        StoryChapters[0].ChapterDialogues[1].sentences = new string[32];
        StoryChapters[0].ChapterDialogues[1].decision1 = new string[1];
        StoryChapters[0].ChapterDialogues[1].decision2 = new string[1];

        StoryChapters[0].ChapterDialogues[1].sentences[0] = "Hey there! You don’t look too good. School draining you dead already?";

        StoryChapters[0].ChapterDialogues[1].sentences[1] = "Ah, h-hi.";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "And, no, I just had a rough start to my day, that’s all.";

        StoryChapters[0].ChapterDialogues[1].sentences[3] = "But it’s lunch time though?";
        StoryChapters[0].ChapterDialogues[1].sentences[4] = "Anyways, my name is [INSERT NF NAME HERE], what’s yours?";

        StoryChapters[0].ChapterDialogues[1].sentences[5] = "It’s [INSERT MC NAME HERE]";

        StoryChapters[0].ChapterDialogues[1].sentences[6] = "You into video games?";

        StoryChapters[0].ChapterDialogues[1].sentences[7] = "Yeah. Why do you ask?";

        StoryChapters[0].ChapterDialogues[1].sentences[8] = "I saw you have a very familiar rainbow-colored multiprism keychain, figured you were into that game series";
        StoryChapters[0].ChapterDialogues[1].sentences[9] = "Anyways, what kind of games are you playing?";

        StoryChapters[0].ChapterDialogues[1].sentences[10] = "Well, I was into MMORPGs at a certain period of time. Like Last Legend 14, along with Last Legend 13,LL13-2, and LL13 - Thunder Rebounds. So, generally JRPGs.";
        
        StoryChapters[0].ChapterDialogues[1].sentences[11] = "Interesting mix! Have you ever played gacha games before?";

        StoryChapters[0].ChapterDialogues[1].sentences[12] = "I’ve heard of them, but I haven’t tried any yet. Too busy with MMOs, but… I guess I won’t be playing LL14 anytime soon.";

        StoryChapters[0].ChapterDialogues[1].sentences[13] = "Interesting mix! Have you ever played gacha games before?";

        StoryChapters[0].ChapterDialogues[1].sentences[14] = "Why is that, if you don't mind me asking?";

        StoryChapters[0].ChapterDialogues[1].sentences[15] = "I broke up with my girlfriend.";
        StoryChapters[0].ChapterDialogues[1].sentences[16] = "That’s rough buddy.";
        StoryChapters[0].ChapterDialogues[1].sentences[17] = "...";
        StoryChapters[0].ChapterDialogues[1].sentences[18] = "Anyways, you were asking?";
        StoryChapters[0].ChapterDialogues[1].sentences[19] = "Ah right! The reason why I asked you is because there’s this new game that got released today. It’s called ‘MagiHimeDive’!";

        StoryChapters[0].ChapterDialogues[1].sentences[20] = "Magihimi-what?";

        StoryChapters[0].ChapterDialogues[1].sentences[21] = "MagiHimeDive!, it’s actually localized as Magical Princess Dive, but we call it MagiHimeDive for simplicity’s sake";
        StoryChapters[0].ChapterDialogues[1].sentences[22] = "It’s a gacha game where you save the world as a tactician from another world, and you command princesses to fight for you!";
        StoryChapters[0].ChapterDialogues[1].sentences[23] = "The waifus look amazing!";


        StoryChapters[0].ChapterDialogues[1].sentences[24] = "I-I see…";

        StoryChapters[0].ChapterDialogues[1].sentences[25] = "Well, have a look at the first one they give you!";

        //narration
        StoryChapters[0].ChapterDialogues[1].sentences[26] = "A blonde girl wearing something reminiscent of a school uniform smiles, facing the screen. A quick image of a silhouette of a woman flashes by.";

        StoryChapters[0].ChapterDialogues[1].sentences[27] = "W-Wow. She does look pretty. Is it available for Cyborg devices?";

        StoryChapters[0].ChapterDialogues[1].sentences[28] = "Yeah! So, are you interested in playing?";

        StoryChapters[0].ChapterDialogues[1].decision1[0] = "Yeah! Let me download it right now.";
        StoryChapters[0].ChapterDialogues[1].decision2[0] = "I’m unsure… It seems like a game for degenerates…";
        //1st choice result
        StoryChapters[0].ChapterDialogues[1].sentences[29] = "Alright! Here’s my friend code!";
        //2nd choice result
        StoryChapters[0].ChapterDialogues[1].sentences[30] = "I see… Here’s my code anyways!";

        StoryChapters[0].ChapterDialogues[1].sentences[31] = "Alright…";
        //screen fade to black then back to dimly lit room

        //chapter 1 - dialogue 3
        StoryChapters[0].ChapterDialogues[2] = new Dialogue();
        StoryChapters[0].ChapterDialogues[2].name = "MC";
        StoryChapters[0].ChapterDialogues[2].otherName = "Alice";
        StoryChapters[0].ChapterDialogues[2].sentences = new string[32];
        StoryChapters[0].ChapterDialogues[2].decision1 = new string[1];
        StoryChapters[0].ChapterDialogues[2].decision2 = new string[1];

        StoryChapters[0].ChapterDialogues[2].sentences[5] = "...And it's done.";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Still, the game is relatively light for a gacha game.";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "The princesses do look good though.";
        //narration
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "And the game starts.";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "A blonde woman stands in front of the screen, wearing the same familiar school uniform. She smiles.";

        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Ah, hero, you’re finally here!";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Quickly, take my hand!";

        //spawn phone
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Phone:\nAnd just like that, I was whisked away.The girl dragged me out of the dimly lit room… And it was there that I saw it, a world of color.";
        
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "What are we, colorless?";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Hero, there’s no time to explain, kiss the back of my palm!";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Phone:\n“Huh? Why?” I asked out of surprise. As I asked, however, the area immediately began to feel eerie, and a shiver went down my spine.";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Hurry!";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Phone\nAlice shoves her hand towards my face, and I immediately kiss it.As I pull back, A mark is left on the back of her palm.A crown. A smile of confidence replaces her once nervous face.Light begins to combine around the mark on her hand, and a sword forms from the light";
        StoryChapters[0].ChapterDialogues[2].sentences[5] = "Alright! Let’s do this!";

        //start phone game 
        /*
            [Tutorial begins]

            To start, try connecting the following pieces together!

            Good! See that? I managed to hurt the enemy. The longer the chain, the more damage I deal. Connect the longest one you can find!
            See? Now, try adding this in the middle of a chain!

            Yes! Good! You seem to be a really good tactician, huh? Now, say my name. Don’t worry, I know you know it. *selecting her now glowing icon lets her use her skill*

            [Tutorial Ends]
        */
        /*
            Alice
            “Whew, Thanks Hero. If it wasn’t for you, we would’ve been lost.”

            Phone
            “H-How did I know your name?”

            Alice
            “Ah, right… The moment you kissed my hand, we formed a contract. I’m now your pawn, and you, my king.”

            MC
            “Pawn? King? That explains the chess pieces”

            Alice
            “However, we need to look for more people to join under your army… Come! There’s a camp nearby where others have come to rest. Follow me!”

            [Gacha Tutorial]

            We got lucky earlier. Help is needed for us to handle the upcoming battles.


            Well, you know the drill, kiss the hand of the maiden!

            [After Gacha]

            Alice
            “Okay, we have what we need now. Don’t forget to select the princesses you want in our adventure!”

            MC
            “Well, that’s enough for tonight… I have to go to sleep tomorrow…”
            “Ah, right, I have to add him as a friend”

        */
        /* choice
            If friendship good
                        MC
            “Huh, he already has an SSR unit.”
            “Nice.”
            If friendship bad
                        MC
            “I’ll do it tomorrow…”


            after choice
            “Alright, time to go to bed…”

            The scene fades to black, and thats the end of the alpha

        */

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

    public void Chapter1Dialogue2()
    {

            //move character animation
            //load to classroom
            //then play next dialogue
            FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[1], true);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        setAllChapters();
        setAllDialogues();
        FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[0], true);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<StoryManager>().currentDialogue == 1 &&
            FindObjectOfType<StoryManager>().currentChapter == 0)
        {
            Debug.Log("im called 1");
            if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone)
            {
                Debug.Log("im called 2");
            }
            else
            {
                Debug.Log("im called 3");
                FindObjectOfType<StoryManager>().Chapter1Dialogue2();
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone = true;
            }
        }
    }
}
