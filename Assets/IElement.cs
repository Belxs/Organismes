using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElement
{
    // Start is called before the first frame update
    public ElementInfo GetElementInfo();
   
}
public enum TypeElement
{
    ORG,
    FOOD,
    WALL,
    Water
}

public class ElementInfo 
{
    public TypeElement typeElement;
    public IElement element;
    public System.Type typeClass;
    
    public ElementInfo(IElement element)
    {
        this.element = element;
        typeClass = element.GetType();
        switch (typeClass.Name)
        {
            case "Organism":
                IElement org = (Organism)element;
                typeElement = TypeElement.ORG;

                //Debug.Log("Èíôî: " + org + " " + typeElement);
                break;
            case "Food":
                org = (Food)element;
                typeElement = TypeElement.FOOD;

                //Debug.Log("Èíôî: " + org +" "+typeElement);
                break;
            case "Water":
                org = (Water)element;
                typeElement = TypeElement.FOOD;

                //Debug.Log("Èíôî: " + org + " " + typeElement);
                break;
            case "Wall":
                org = (Wall)element;
                typeElement = TypeElement.WALL;

                //Debug.Log("Èíôî: " + org + " " + typeElement);
                break;
        }
    }
    
}
