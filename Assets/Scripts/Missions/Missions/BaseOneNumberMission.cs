using UnityEngine;
using System.Collections;

public class BaseOneNumberMission : Mission {
	public int needNumber;
	private int currentNumber = 0;
	
	public override string GetProgressRepresentation ()
	{
		return string.Format("{0}",needNumber-currentNumber);
	}
	
	public void AddNumber(int addNumber){
		currentNumber+=addNumber;
		if(currentNumber>needNumber){
			currentNumber = needNumber;
		}
		MissionProgressChanged();
		if(currentNumber==needNumber){
			MissionFinished();
		}	
	}
	
	public override string Serialize ()
	{
		if(!oneLife){
			return currentNumber.ToString();
		}
		return "0";
	}
	
	public override void Unserialize (string data)
	{
		if(!oneLife){
			currentNumber = int.Parse(data);
		}
	}
}
