namespace Factory_method;

public abstract class Creator
{
    public abstract Document CreateDocument();
}

public class PdfCreator : Creator
{
    public override Document CreateDocument()
    {
        return new PdfDocument();
    }
}

public class ExcelCreator : Creator
{
    public override Document CreateDocument()
    {
        return new ExcelDocument();
    }
}

public abstract class Document
{
}

public class PdfDocument : Document
{
    public PdfDocument()
    {
        Console.WriteLine("Pdf документ сгенерирован");
    }
}

public class ExcelDocument : Document
{
    public ExcelDocument()
    {
        Console.WriteLine("Excel документ сгенерирован");
    }
}

internal static class Program
{
    static void Main(string[] args)
    {
        Creator creator = new PdfCreator();
        creator.CreateDocument();
        
    }
}