using Godot;

public partial class ScoreManager : Node
{
	private const string SCORES_PATH = "user://animals.res";
	public static ScoreManager Instance { get; private set; }
	public LevelScores LevelScores { get; private set; } = new();
	public static int LevelSelected { get; set; } = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		LoadScoresFromFile();
	}

	public static void SetScore(int score)
	{
		Instance.LevelScores.SetBestScore(LevelSelected, score);
		Instance.SaveScoresToFile();
	}

	public static int GetBestScoreForLevel(int level)
	{
		return Instance.LevelScores.GetBestScore(level);
	}

	private void LoadScoresFromFile()
	{
		LevelScores = new LevelScores();

		if(ResourceLoader.Exists(SCORES_PATH))
		{
			var data = ResourceLoader.Load<LevelScores>(SCORES_PATH);
			if(data != null) LevelScores = data;
		}
	}

	private void SaveScoresToFile()
	{
		Error err = ResourceSaver.Save(LevelScores, SCORES_PATH);
		if (err != Error.Ok)
		{
			GD.PrintErr($"Failed to save scores to file: {err}");
		}
	}
	
}
