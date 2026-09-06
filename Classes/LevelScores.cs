using Godot;
using System;
using System.Collections.Generic;

public partial class LevelScores : Resource
{
    private const int DEFAULT_SCORE = 9999;
    [Export] private Godot.Collections.Dictionary<int, int> _levelScores = [];
    
    public int GetBestScore(int level)
    {
        return _levelScores.GetValueOrDefault(level, DEFAULT_SCORE);
    }
    public void SetBestScore(int level, int score)
    {
        if (GetBestScore(level) > score)
        {
            _levelScores[level] = score;
        }
    }
}
