

public class Magazine : Material
{    
    public int EditionNumber { get; set; }
    public override string GetDescription()
    {
        return $"Revista: {Title}, Autor: {Author}, Año: {PublicationYear}, Edición: {EditionNumber}";
    }
}
