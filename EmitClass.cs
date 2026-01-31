namespace StaticWebAssetToolingLibrary;
internal partial class EmitClass(BasicList<FileClass> list)
{    
    public void Emit()
    {
        PopulateImages();
    }



    private void PopulateImages()
    {
        SourceCodeStringBuilder builder = new();
        builder.WriteAssetClass(w =>
        {
            
            w.WriteLine("public static void Register()")
                .WriteCodeBlock(w =>
                {
                    foreach (var item in list)
                    {
                        w.WriteLine($"""
                            CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions.FileContentRegistry.RegisterFile("{item.FullName}", {item.ResolvedPath});
                            """);
                    }
                });
        });
        string text = builder.ToString();
        ff1.WriteAllText(GlobalConstants.FinalName, text);
    }

   
}