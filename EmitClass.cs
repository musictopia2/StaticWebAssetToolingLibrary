
namespace StaticWebAssetToolingLibrary;
internal partial class EmitClass(BasicList<FileClass> list)
{    
    public void Emit()
    {
        PopulateImages();
    }

    static bool IsValidIdentifier(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (!(char.IsLetter(name[0]) || name[0] == '_'))
        {
            return false;
        }

        for (int i = 1; i < name.Length; i++)
        {
            char c = name[i];
            if (!(char.IsLetterOrDigit(c) || c == '_'))
            {
                return false;
            }
        }
        return true;
    }

    private void PopulateImages()
    {
        SourceCodeStringBuilder builder = new();
        builder.WriteAssetClass(w =>
        {
            foreach (var item in list)
            {
                if (!IsValidIdentifier(item.Name))
                {
                    throw new CustomBasicException($"Invalid PNG name '{item.Name}'. Use PascalCase (letters/digits/underscore only).");
                }

                w.WriteLine($"""
                    public const string {item.Name} = "{item.Name}";
                    """);
            }
            w.WriteLine("public static void Register()")
                .WriteCodeBlock(w =>
                {
                    foreach (var item in list)
                    {
                        w.WriteLine($"""
                            CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry.RegisterFile("{item.Name}", {item.ResolvedPath});
                            """);
                    }
                });
        });
        string text = builder.ToString();
        ff1.WriteAllText(GlobalConstants.FinalName, text);
    }

   
}