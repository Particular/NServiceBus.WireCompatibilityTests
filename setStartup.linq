<Query Kind="Program">
  <NuGetReference>SetStartupProjects</NuGetReference>
  <Namespace>SetStartupProjects</Namespace>
</Query>

void Main()
{
	var diretory = Path.GetDirectoryName(Util.CurrentQueryPath);
	var src = Path.Combine(diretory, "src");
	SetStartupProjects(src);
}

public static void SetStartupProjects(string codeDirectory)
{
	var startProjectSuoCreator = new StartProjectSuoCreator();
	foreach (var suo in Directory.EnumerateFiles(codeDirectory, "*.suo", SearchOption.AllDirectories))
	{
		File.Delete(suo);
	}
	foreach (var solutionFile in Directory.EnumerateFiles(codeDirectory, "*.sln", SearchOption.AllDirectories))
	{
		var startProjects = GetStartupProjects(solutionFile).ToList();
		if (startProjects.Any())
		{
			startProjectSuoCreator.CreateForSolutionFile(solutionFile, startProjects);
		}
	}
}

public static IEnumerable<string> GetStartupProjects(string solutionFile)
{
	var solutionDirectory = Path.Combine(Path.GetDirectoryName(solutionFile));
	var startProjectFinder = new StartProjectFinder();
	foreach (var startProject in startProjectFinder.GetStartProjects(solutionFile))
	{
		yield return startProject;
	}
	yield break;
}