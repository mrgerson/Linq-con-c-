public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();

    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true})!;
        }
    }

    public IEnumerable<Book> TodaLaColeccion()
    {
        return librosCollection;
    }

     public IEnumerable<Book> LibrosDespuesdel2000()
    {
        //extension method
        //return librosCollection.Where(p=> p.PublishedDate.Year > 2000);

        //query expresion

        return from l in librosCollection where l.PublishedDate.Year > 2000 select l;
    }

    public IEnumerable<Book> LibrosConMasde250PagConPalabrasInAction()
    {
        //extension methods
        //return librosCollection.Where(p=> p.PageCount > 250 && p.Title.Contains("in Action"));

        //query expression
        return from l in librosCollection where l.PageCount > 250 && l.Title.Contains("in Action") select l;
    }

    //retorna true o false 
     public bool TodosLosLibrosTienenStatus()
    {
        return librosCollection.All(p=> p.Status!= string.Empty);
    }

    //si alguno cumple la condición  retorna true o false 
    public bool SiAlgunLibroFuePublicado2005()
    {
        return librosCollection.Any(p=> p.PublishedDate.Year == 2005);
    }

     public IEnumerable<Book> LibrosdePython()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Python"));
    }

    public IEnumerable<Book> LibrosdeJavaPorNombreAscendente()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderBy(p=> p.Title);
    }

    public IEnumerable<Book> Librosmasde450pagOrdernadorPorNumPagDescendente()
    {
        return librosCollection.Where(p=> p.PageCount > 450).OrderByDescending(p=> p.PageCount);
    }

     public IEnumerable<Book> TresPrimerosLibrosJavaOrdenadosPorFecha()
    {
        return librosCollection
        .Where(p=> p.Categories.Contains("Java"))
        .OrderBy(p=> p.PublishedDate)
        .TakeLast(3);
    }

    public IEnumerable<Book> TerceryCuartoLibroDeMas400Pag()
    {
        return librosCollection
        .Where(p=> p.PageCount > 400)
        .Take(4)
        .Skip(2);
    }

      public IEnumerable<Book> TresPrimeroLibrosDeLaCollecion()
    {
        return librosCollection.Take(3)
        .Select(p=> new Book() { Title= p.Title, PageCount= p.PageCount  });
    }

    public int CantidadDeLibrosEntre200y600Pag()
    {
        return librosCollection.Where(p=> p.PageCount>=200 && p.PageCount<=600).Count();
    }

    public long CantidadDeLibrosEntre200y500Pag()
    {
        return librosCollection.LongCount(p=> p.PageCount>=200 && p.PageCount<=500);
    }

    public DateTime FechaDePublicacionMenor()
    {
        return librosCollection.Min(p=> p.PublishedDate);
    }

    public int NumeroDePagLibroMayor()
    {
        return librosCollection.Max(p=> p.PageCount);
    }
    public Book? LibroConMenorNumeroDePaginas()
    {
        return librosCollection.Where(p=> p.PageCount>0).MinBy(p=> p.PageCount);
    }

    public Book? LibroConFechaPublicacionMasReciente()
    {
        return librosCollection.MaxBy(p=> p.PublishedDate);
    }
}