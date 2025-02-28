namespace Abstract_factory;

public interface IReport
{
    public void Generate();
}

public interface IInvoice
{
    public void Create();
}

public class PdfReport : IReport
{
    public void Generate()
    {
        Console.WriteLine("Создан отчет в формате PDF");
    }
}

public class PdfInvoice : IInvoice
{
    public void Create()
    {
        Console.WriteLine("Создана счет-фактура в формате PDF");
    }
}

public class ExcelReport : IReport
{
    public void Generate()
    {
        Console.WriteLine("Создан отчет в формате Excel");
    }
}

public class ExcelInvoice : IInvoice
{
    public void Create()
    {
        Console.WriteLine("Создана счет-фактура в формате Excel");
    }
}

public interface IDocumentFactory
{
    IReport CreateReport();
    IInvoice CreateInvoice();
}

public class PdfDocumentFactory : IDocumentFactory
{
    public IReport CreateReport()
    {
        return new PdfReport();
    }

    public IInvoice CreateInvoice()
    {
        return new PdfInvoice();
    }
}

public class ExcelDocumentFactory : IDocumentFactory
{
    public IReport CreateReport()
    {
        return new ExcelReport();
    }

    public IInvoice CreateInvoice()
    {
        return new ExcelInvoice();
    }
}

public class DocumentGenerator
{
    private readonly IReport _report;
    private readonly IInvoice _invoice;

    public DocumentGenerator(IDocumentFactory documentFactory)
    {
        _report = documentFactory.CreateReport();
        _invoice = documentFactory.CreateInvoice();
    }

    public void GenerateDocument()
    {
        _report.Generate();
        _invoice.Create();
    }
}

class Program
{
    static void Main(string[] args)
    {
        DocumentGenerator documentGenerator = new DocumentGenerator(new PdfDocumentFactory());
        documentGenerator.GenerateDocument();
        documentGenerator = new DocumentGenerator(new ExcelDocumentFactory());
        documentGenerator.GenerateDocument();
    }
}