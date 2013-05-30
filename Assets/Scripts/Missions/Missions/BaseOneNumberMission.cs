using UnityEngine;
using System.Collections;

public class BaseOneNumberMission : Mission {
	public int needNumber;
	private int currentNumber = 0;
	
	public override string GetProgressRepresentation ()
	{
		return string.Format("{0}",needNumber-currentNumber);
	}
	
	public override string GetLongProgressRepresentation ()
	{
		return string.Format("{0}/{1}",currentNumber,needNumber);
	}
	
	public override float GetProgress ()
	{
		return ((float)currentNumber)/needNumber;
	}
	
	public override void Restart ()
	{
		if(oneLife){
			currentNumber = 0;
		}
	}
	
	
	public void ResetNumber(){
		currentNumber = 0;	
		MissionProgressChanged();
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
		string str = "";
		if(!oneLife){
			str+=currentNumber.ToString();
		}else{
			str+="0";
		}
		str+="|";
		str+=GetState()==MissionStates.ACTIVE?"1":"0";
		return str;
	}
	
	public override void Unserialize (string data)
	{
		char[] splitData = new char[] { '|' };
		string[] numbers = data.Split (splitData);
		if(!oneLife){
			currentNumber = int.Parse(numbers[0]);
		}
		int active = int.Parse(numbers[1]);
		if(active==1){
			state = MissionStates.ACTIVE;
		}
	}
	
}
