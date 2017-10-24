using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class dataRetrieve : MonoBehaviour {
	private bool clearBool;
	public string queryname;
	public InputField Field;
	public static string hyperlink;
	private string appid="98d6e3da";
	private string app_key="8a1770d6b846a9e4e3b4983eaec7c992";
	public int offset;
	public int limit;
	private string soffset;
	private string slimit;
	public Scrollbar scroll;
	public static string jsonstring;
	public static List<string> poptitle = new List<string>();
	public static List<string> typemedia=new List<string>();
	public static List<string> availDes=new List<string>();
	public static List<string> duration=new List<string>();
	public static List<string> publisher=new List<string>();
	public static List<string> orgtitle=new List<string>(); 
	public GameObject ListItemBase;

	[SerializeField]
 	private GameObject listContainer = null;
	 //private GameObject Canvas=null;
	 public GameObject DetailSpawn;

	 static List<GameObject> listItemContainer = new List<GameObject>();	

	 public void search()
	 {	 
		 clearBool=false;
		 offset=0;
		 fetch();
	 }
	public void scrollzero()
	{	
		if(clearBool==false && scroll.value==0f)
		{
		offset+=10;
		fetch();}
	}

	public void fetch()
	{	
	queryname=Field.text;	
	soffset=offset.ToString();
	slimit=limit.ToString();
	hyperlink="https://external.api.yle.fi/v1/programs/items.json?app_id="+appid+"&app_key="+app_key+"&offset="+soffset+"&limit="+slimit+"&q="+queryname;

	   if (queryname!="")
	   {
	   StartCoroutine(GetText());}
	}

	IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(hyperlink))
        {
            yield return request.Send();
            if (request.isError) // Error
            {
                Debug.Log(request.error);}
            else // Success
            {	jsonstring=request.downloadHandler.text;
				parser(jsonstring);}                       };
    }

	public void parser(string parsejson)
	{
		var N = JSON.Parse(parsejson);
		var tempcount=N["data"].Count;
		Debug.Log(tempcount);

		try
        {            
            for (int i = 0; i < N["data"].Count; i++)
            {	
				if (N["data"][i]["itemTitle"]["fi"] != null)
                {
                    poptitle.Add(N["data"][i]["itemTitle"]["fi"].Value);
					typemedia.Add(N["data"][i]["typeMedia"].Value);
					publisher.Add(N["data"][i]["publicationEvent"][0][1]["id"].Value);
					orgtitle.Add(N["data"][i]["originalTitle"]["und"].Value);
					duration.Add(N["data"][i]["duration"].Value);
					Debug.Log(publisher[i]);

					populateList(poptitle[i]);
					
					}
                else
                {
                    Debug.Log("Error: Entity with no title");}
                
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught: " + e.Message);}
	}

	public void populateList(string buttonText)
	{
		GameObject listItem = Instantiate(ListItemBase);
		listItem.name=buttonText;
		listItem.GetComponent<Button>().onClick.AddListener(() => onButtonClick(buttonText));
        listItem.GetComponent<SetListItemData>().SetData(buttonText);
        listItemContainer.Add(listItem);
		listItem.transform.SetParent(listContainer.transform, false);
	}

	public void onButtonClick (string buttonText)
	{
		DetailSpawn.GetComponent<DetailPanel>().showDetail(buttonText);}

	public void panelClr()
	{
		DetailSpawn.GetComponent<DetailPanel>().hideDetail();}

	public void clearList()
	{	clearBool=true;
		foreach (GameObject goWp in listItemContainer)
 		{
     		Destroy (goWp);}

 		for (int i = 0; i < listItemContainer.Count; i++)
 		{
    		 listItemContainer.RemoveAt(i);
			 poptitle.RemoveAt(i);
			 typemedia.RemoveAt(i);
			 publisher.RemoveAt(i);
			 orgtitle.RemoveAt(i);
			 duration.RemoveAt(i);	 
		}
	}

}