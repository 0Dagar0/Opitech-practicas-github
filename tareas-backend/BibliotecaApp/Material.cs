
public class Material
{
    public string Title {  get; set; }
    public string Author { get; set; }
    public string PublicationYear  { get; set; }
    
    public virtual string GetDescription ()
    {
        return $" Título: {Title}, Autor: {Author}, Año: {PublicationYear }";
    }
}
