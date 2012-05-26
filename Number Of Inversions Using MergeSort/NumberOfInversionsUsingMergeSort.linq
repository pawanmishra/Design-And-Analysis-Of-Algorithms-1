<Query Kind="Program">
  <Connection>
    <ID>0f205bc4-2315-4dc0-8efa-d3195b1ebb64</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <SqlSecurity>true</SqlSecurity>
    <NoPluralization>true</NoPluralization>
    <UserName>testdev</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAla1/UEH+IkyJ09PI/MoIjgAAAAACAAAAAAADZgAAwAAAABAAAAATIrNj7nzu5QPgwgsaXYMuAAAAAASAAACgAAAAEAAAAF6YWAWpmQbw5k8i4euE5bAIAAAAkL886rXQtPIUAAAApYDLz957eOPSuFcuWHZGOZ9tA9k=</Password>
    <Database>ClinicalAdvantage</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Output>DataGrids</Output>
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

long inversion = 0;
void Main()
{
	var items = File.ReadAllLines(@"C:\IntegerArray.txt").ToList().Select(x => Convert.ToInt32(x)).ToArray();
	MergeSort(items, 0, items.Count() - 1);
	items.Dump();
	inversion.Dump();
}

public void MergeSort(int[] a, int low, int high, string indent = " ")
{
	if(high <= low){
		//Console.WriteLine(indent + "Return : Low : {0}, High : {1}",low,high);
		return;
	}
	
	int middle = (low + high)/2;
	//Console.WriteLine(indent + "Low : {0}, Middle : {1}, High : {2}",low,middle,high);
	MergeSort(a,low,middle, indent + " ----> ");
	MergeSort(a,middle + 1,high, indent + " ====> ");
	Merge(a,low, middle, high, indent);
}

public void Merge(int[] a, int low, int middle, int high, string indent = " ")
{
	//Console.WriteLine(indent + "Inside Merge " + "Low : {0}[{1}], Middle : {2}[{3}], High : {4}[{5}]",low,a[low],middle,a[middle],high,a[high]);
	int i;
	Queue<int> qLeft = new Queue<int>();
	Queue<int> qRight = new Queue<int>();
	
	for(i = low; i <= middle ; i++) qLeft.Enqueue(a[i]);
	for(i = middle + 1; i <= high ; i++) qRight.Enqueue(a[i]);
	
	i = low;	
	while(!(qLeft.Count == 0 || qRight.Count == 0))
	{
		if(qLeft.Peek() < qRight.Peek())
		{
			a[i++] = qLeft.Dequeue();
		}
		else
		{
			a[i++] = qRight.Dequeue();
			inversion += qLeft.Count;
		}
	}
	
	while(qLeft.Count != 0)a[i++] = qLeft.Dequeue();
	while(qRight.Count != 0)a[i++] = qRight.Dequeue();
}