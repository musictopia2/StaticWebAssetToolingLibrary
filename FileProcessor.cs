namespace StaticWebAssetToolingLibrary;
public class FileProcessor(IStaticWebAssetSpec resolver, params BasicList<string> args)
{

    

    public async Task ProcessAsync()
    {
        Console.WriteLine("Start Using File Tooling Library");
        if (args.Count != 3)
        {
            Console.WriteLine($"Expected 3 arguments but only received {args.Count}");
            return;
        }
        GlobalConstants.ProjectName = args[0];
        GlobalConstants.GlobalName = resolver.GetGlobalName;
        string projectDirectory = args[1];
        string projectFile = args[2];
        string wwwrootPath = Path.Combine(projectDirectory, "wwwroot");


        string NormalizeFullPath(string candidate)
        {
            // If candidate already looks like a rooted path, trust it.
            if (Path.IsPathRooted(candidate))
                return candidate;

            // Otherwise, treat it as relative to wwwrootPath
            return Path.Combine(wwwrootPath, candidate);
        }

        if (ff1.DirectoryExists(wwwrootPath) == false)
        {
            Console.WriteLine("There was no wwwroot folder");
            return;
        }
        GlobalConstants.CsProjPath = Path.Combine(projectDirectory, projectFile);
        if (ff1.FileExists(GlobalConstants.CsProjPath) == false)
        {
            Console.WriteLine("There was no csproj file located");
            return;
        }
        BasicList<string> list = await ff1.FileListAsync(wwwrootPath, SearchOption.AllDirectories);
        BasicList<FileClass> files = [];
        foreach (string item in list)
        {
            if (Path.GetExtension(item).Equals(".cs", StringComparison.OrdinalIgnoreCase))
                continue;

            var ext = Path.GetExtension(item).ToLowerInvariant();
            if (resolver.ExtensionsAllowed.Contains(ext) == false)
            {
                continue;
            }

            // Full path to file (your wrapper’s behavior matters, but this matches what you were doing)
            string fullPath = NormalizeFullPath(item);

            // Relative path under wwwroot (this is the key to correct _content URLs)
            string relativeUnderWwwroot = Path.GetRelativePath(wwwrootPath, fullPath)
                .Replace('\\', '/');

            

            // Key used in registry / const name (your old behavior)
            // If you want key == filename without extension:
            string key = Path.GetFileNameWithoutExtension(relativeUnderWwwroot);

            // Static web asset URL (RCL)
            string url = $"/_content/{GlobalConstants.ProjectName}/{relativeUnderWwwroot}";

            FileClass file = new();
            file.Name = ff1.FileName(item);
            file.ResolvedPath = $"""
                "{url}"
                """;

            files.Add(file);
        }
        if (files.Count == 0)
        {
            Console.WriteLine($"There was no files found at {wwwrootPath}");
            return;
        }
        string assetFolder = Path.Combine(projectDirectory, "Assets");
        if (ff1.DirectoryExists(assetFolder) == false)
        {
            ff1.CreateFolder(assetFolder);
        }
        GlobalConstants.FinalName = Path.Combine(assetFolder, $"{GlobalConstants.GlobalName}.cs");
        EmitClass emits = new(files);
        emits.Emit();
        Console.WriteLine($"Created c# files for {GlobalConstants.GlobalName}.  Check out");
    }
}