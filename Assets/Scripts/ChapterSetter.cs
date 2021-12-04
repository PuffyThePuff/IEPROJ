using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSetter
{
    public List<StoryChapter> StoryChapters;
    private const int MAX_CHAPTERS = 2;

    public void setAllChapters()
    {
        for (int i = 0; i < MAX_CHAPTERS; i++)
        {
            StoryChapters[i].ChapterNumber = i + 1;
        }

        StoryChapters[0].ChapterDialogues[0] = new Dialogue();
    }
}
