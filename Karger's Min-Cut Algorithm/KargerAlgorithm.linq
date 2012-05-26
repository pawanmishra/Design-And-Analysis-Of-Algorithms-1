<Query Kind="Program">
  <Connection>
    <ID>7af8e984-b7db-4200-92f8-1a91e289c4e4</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/Northwind/Northwind.svc/</Server>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Transactions.Bridge.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.Selectors.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Messaging.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Namespace>System.ServiceModel.Channels</Namespace>
</Query>

// Adjacency list for representing graph
Dictionary<int,List<int>> adjacencyList;

void Main()
{
	int count = 1000;
	ReadGraphFromFile();
	int numberOfCuts = 100000;
	int highestValueCount;
	for(int i = 0; i < 1000; i++){
		highestValueCount = 0;
		while(adjacencyList.Count > 2){
			Tuple<int,int> tuple = GetRandomVertexs();
			AppendNodes(tuple.Item1,tuple.Item2);
			RemoveSelfLoops();
		}
		foreach(KeyValuePair<int,List<int>> data in adjacencyList)
		{
			if(data.Value.Count > highestValueCount)
				highestValueCount = data.Value.Count;
		}
		
		if(count > highestValueCount)
			count = highestValueCount;
	}
	adjacencyList.Dump();
	count.Dump();
}

public void ReadGraphFromFile()
{
	string[] number = File.ReadAllLines(@"KargerAdj.txt");
	adjacencyList = new Dictionary<int,List<int>>();
	
	for(int i = 0; i < number.Length; i++)
	{
		int[] items = number[i].Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToArray();
		adjacencyList.Add(items[0],items.Skip(1).ToList<int>());
	}
}

public Tuple<int,int> GetRandomVertexs()
{
	Random random = new Random();
	int randomVertex = random.Next(0,adjacencyList.Count());
	var keyArray = adjacencyList.Select(x => x.Key).ToArray();
	int item1 = keyArray[randomVertex]; //Get the main edge
	int randomEdgeVertex = random.Next(0,adjacencyList[item1].Count);
	int item2 = adjacencyList[item1].ToArray()[randomEdgeVertex];
	return new Tuple<int,int>(item1,item2);
}

public void AppendNodes(int parentNode, int toBeMergedNode)
{	
	try{
		var sourceList = adjacencyList[parentNode];
		var listOfDestinatioNodes = adjacencyList[toBeMergedNode];
		
		foreach(var number in listOfDestinatioNodes)
		{
			if(!sourceList.Contains(number) && number != parentNode)
			{
				sourceList.Add(number);
			}
		}
		
		sourceList.Remove(toBeMergedNode);
		
		foreach(KeyValuePair<int,List<int>> source in adjacencyList)
		{
			if(source.Value.Contains(toBeMergedNode)){
				int count = source.Value.RemoveAll(x => x == toBeMergedNode);	
				for(int i = 0; i < count; i++)
					source.Value.Add(parentNode);
			}
		}
		
		adjacencyList[parentNode] = sourceList;	
		adjacencyList.Remove(toBeMergedNode);	
	}
	catch(Exception)
	{
		Console.WriteLine("ParentNode : {0} & ToBeMergedNode : {1}",parentNode,toBeMergedNode);
		adjacencyList[parentNode].Dump();
		adjacencyList[toBeMergedNode].Dump();
		throw;
	}
}

public void RemoveSelfLoops()
{
	foreach(KeyValuePair<int,List<int>> data in adjacencyList)
	{
		int key = data.Key;
		List<int> listOfItems = adjacencyList[key];
		adjacencyList[key].RemoveAll(x => x == key);
	}
}

// Define other methods and classes here