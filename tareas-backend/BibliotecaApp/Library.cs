using System;
using System.Collections.Generic;
using System.Threading.Channels;

public class Library
{
    private List<Material> materials = new List<Material>();

    public void AddMaterial(Material material)
    {
        materials.Add(material);
    }

    public void ShowAvaileable()
    {
        foreach (var material in materials)
        {
            if (material is ILoanable loanable && loanable.IsAvaileable())
            {
                Console.WriteLine(material.GetDescription());
            }
        }
    }


    public void LendMaterial(string title, User user)
    {
        foreach (var material in materials)
        {
            if (material.Title == title && material is ILoanable loanable)
            {
                loanable.Lend(user);
                return;
            }
        }

        Console.WriteLine("Material no encontrado o no es prestable");
    }

    public void ReturnMaterial(string title)
    {
        foreach (var material in materials)
        {
            if (material.Title == title && material is ILoanable loanable)
            {
                loanable.Reincorporate();
                return;
            }
        }

        Console.WriteLine("material no encontrado / no se puede prestar ");
    }


}

