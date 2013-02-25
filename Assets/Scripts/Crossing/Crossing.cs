using UnityEngine;
using System.Collections;

public enum enumCrossType
{
    ctParallel,    // отрезки лежат на параллельных прямых
    ctSameLine,    // отрезки лежат на одной прямой
    ctOnBounds,    // прямые пересекаются в конечных точках отрезков
    ctInBounds,    // прямые пересекаются в   пределах отрезков
    ctOutBounds    // прямые пересекаются вне пределов отрезков
};

//класс пересечения
public class CrossResultRec 
{
	public enumCrossType  type;  // тип пересечения
    public Vector2  pt;    // точка пересечения
};

public class Crossing {
	public static CrossResultRec GetCrossing(Vector2 p11, Vector2 p12,// координаты первого отрезка
											 Vector2 p21, Vector2 p22)// координаты второго отрезка
	{	
		CrossResultRec result=new CrossResultRec();
		
	    // знаменатель
	    float Z  = (p12.y-p11.y)*(p21.x-p22.x)-(p21.y-p22.y)*(p12.x-p11.x);
	    // числитель 1
	    float Ca = (p12.y-p11.y)*(p21.x-p11.x)-(p21.y-p11.y)*(p12.x-p11.x);
	    // числитель 2
	    float Cb = (p21.y-p11.y)*(p21.x-p22.x)-(p21.y-p22.y)*(p21.x-p11.x);
		
	    // если числители и знаменатель = 0, прямые совпадают
	    if( (Z == 0)&&(Ca == 0)&&(Cb == 0) )
	    {
	        result.type = enumCrossType.ctSameLine;
	        return result;
	    }
		
		
	    // если знаменатель = 0, прямые параллельны
	    if( Z == 0 )
	    {
	        result.type = enumCrossType.ctParallel;
	        return result;
	    }
		
		
	    float Ua = Ca/Z;
	    float Ub = Cb/Z;
		
	    result.pt.x = p11.x + (p12.x - p11.x) * Ub;
	    result.pt.y = p11.y + (p12.y - p11.y) * Ub;
		
	    // если 0<=Ua<=1 и 0<=Ub<=1, точка пересечения в пределах отрезков
	    if( (0 <= Ua)&&(Ua <= 1)&&(0 <= Ub)&&(Ub <= 1) )
	    {
			if((Ua == 0)||(Ua == 1)||(Ub == 0)||(Ub == 1))
				result.type = enumCrossType.ctOnBounds;
			else
				result.type = enumCrossType.ctInBounds;
	    }
	    // иначе точка пересечения за пределами отрезков
	    else
	    {
	        result.type = enumCrossType.ctOutBounds;
	    }
		
	    return result;		
	}
}