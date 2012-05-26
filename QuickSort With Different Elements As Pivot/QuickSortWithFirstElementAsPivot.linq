<Query Kind="Program">
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

int totalNumberOfComparions = 0;

void Main()
{
	var items = File.ReadAllLines(@"C:\Users\mishrap\Downloads\Week2-TestCases\TestCase9.txt").ToList().Select(x => Convert.ToInt32(x)).ToArray();
	QuickSort(items,0,items.Count() - 1);
	items.Dump();
	totalNumberOfComparions.Dump();
}

public void QuickSort(int[] array, int low, int high)
{
	if(high <= low) return;
	totalNumberOfComparions = totalNumberOfComparions + (high - low);
	int p = Partition(array,low,high);
	QuickSort(array,low,p-1);
	QuickSort(array,p+1,high);
}

public int Partition(int[] array, int low, int high)
{
	int pivot = array[low];
	int i = low + 1;	
	
	for(int j = low + 1; j <= high; j++)
	{
		if(array[j] < pivot)
		{
			int temp = array[j];
			array[j] = array[i];
			array[i] = temp;
			i++;
		}
	}
	
	// swap the pivot
	int t = array[i - 1];
	array[i - 1] = pivot;
	array[low] = t;
	
	return i - 1; // return the index less than pivot element
}

// Define other methods and classes here