using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailPanel : MonoBehaviour {

	public GameObject Details;
	public int index;

	public void showDetail(string title)
	{	
		
		Details.SetActive(true);
		index=dataRetrieve.poptitle.IndexOf(title);

		Details.transform.FindChild("Title").GetComponent<Text>().text=title;
		Details.transform.FindChild("Duration").GetComponent<Text>().text=dataRetrieve.duration[index].Substring(2);
		Details.transform.FindChild("Publisher").GetComponent<Text>().text=dataRetrieve.publisher[index];
		
		if (dataRetrieve.orgtitle[index]=="")
		{
			dataRetrieve.orgtitle[index]="Unavailable";}
		Details.transform.FindChild("orgtitle").GetComponent<Text>().text=dataRetrieve.orgtitle[index];
		

		if (dataRetrieve.typemedia[index]=="RadioContent")
		{
		Details.transform.FindChild("typemedia").GetComponent<RawImage>().texture=Resources.Load("radio") as Texture;}
		
		else if (dataRetrieve.typemedia[index]=="TVContent")
		{
		Details.transform.FindChild("typemedia").GetComponent<RawImage>().texture=Resources.Load("tv") as Texture;}

	}

	public void hideDetail()
	{	
		//Detail=GameObject.Find("Details");
		Details.SetActive(false);

	}
}
