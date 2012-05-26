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
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Xml.Schema</Namespace>
</Query>

void Main()
{
	Thread thread = new Thread(Kosaraju, Int16.MaxValue * 1000);
	thread.Start();
	thread.Join();
	Console.ReadLine();
}

public static void Kosaraju()
{
	List<Tuple<int, int>> lstTuple = new List<Tuple<int, int>>();
	foreach (string str in File.ReadLines(@"G:\LINQPad4\SCC.txt"))
	{
		int[] edge = str.Trim().Split(' ').Select(x => Convert.ToInt32(x.Trim())).ToArray();
		lstTuple.Add(Tuple.Create(edge[0], edge[1]));
	}

	Digraph dg = new Digraph(875714);
	foreach (Tuple<int, int> item in lstTuple)
	{
		dg.AddEdge(item.Item1, item.Item2);
	}

	Dictionary<int, int> dict = new Dictionary<int, int>();
	KosarajuSCC kosaraju = new KosarajuSCC(dg);
	int connectedComponents = kosaraju.Count();
	List<int>[] components = new List<int>[connectedComponents + 1];

	for(int i = 0; i <= connectedComponents; i++)
	{
		components[i] = new List<int>();
	}

	for(int i = 1; i <= dg.V; i++)
	{
		components[kosaraju.Id(i)].Add(i);
	}

	var value = components.OrderByDescending(x => x.Count).Take(10);
	foreach(var data in value)
	{
		Console.WriteLine(data.Count);
	}

	Console.WriteLine("Number of connected components : {0}", connectedComponents);
}

public class Digraph
{
	private Dictionary<int,List<int>> adj;
	public int V{get;set;}
	public int E{get;set;}
	
	public Digraph(int V)
	{
		this.V = V;
		this.E = 0;
		adj = new Dictionary<int,List<int>>();
		
		for(int i = 1; i <= V;i++)
		{
			adj.Add(i,new List<int>());
		}
	}
	
	public void AddEdge(int v , int w)
	{
		adj[v].Add(w);
		E++;
	}
	
	public IEnumerable<int> AdjNodes(int v)
	{
		return adj[v];
	}
	
	public Digraph Reverse()
	{
		Digraph dg = new Digraph(V);
		for(int v = 1; v <= V; v++)
		{
			foreach(int i in AdjNodes(v))
			{
				dg.AddEdge(i,v);
			}
		}
		return dg;
	}
}

public class DirectedDFSOrdering
{
	private bool[] marked;
	private Queue<int> pre;
	private Queue<int> post;
	private Stack<int> reversePost;
	
	public DirectedDFSOrdering(Digraph dg)
	{
		pre = new Queue<int>();
		post = new Queue<int>();
		reversePost = new Stack<int>();
		marked = new bool[dg.V + 1];
		for(int i = 1; i <= dg.V ; i++)
		{
			if(!marked[i])
				dfs(dg,i);
		}
	}
	
	public DirectedDFSOrdering(Digraph dg, int s)
	{
		marked = new bool[dg.V + 1];
		dfs(dg,s);
	}
	
	public DirectedDFSOrdering(Digraph dg, IEnumerable<int> sources)
	{
		marked = new bool[dg.V + 1];
		foreach(int v in sources)
		{
			if(!marked[v])
				dfs(dg,v);
		}
	}
	
	private void dfs(Digraph dg,int v)
	{
		pre.Enqueue(v);
		
		marked[v] = true;
		foreach(int vertex in dg.AdjNodes(v))
		{
			if(!marked[vertex])
				dfs(dg,vertex);
		}
		
		post.Enqueue(v);
		reversePost.Push(v);
	}
	
	public bool Marked(int v)
	{
		return marked[v];
	}
	
	public Queue<int> Pre
	{
		get{return pre;}
	}
	
	public Queue<int> Post
	{
		get{return post;}
	}
	
	public Stack<int> ReversePost
	{
		get{return reversePost;}
	}
}

public class KosarajuSCC
{
	private bool[] marked;
	private int[] id;
	private int count;
	
	public KosarajuSCC(Digraph dg)
	{
		marked = new bool[dg.V + 1];
		id = new int[dg.V + 1];
		DirectedDFSOrdering order = new DirectedDFSOrdering(dg.Reverse());
		
		foreach(int i in order.ReversePost)
		{
			if(!marked[i])
			{
				dfs(dg,i);
				count++;
			}
		}
	}
	
	private void dfs(Digraph dg,int v)
	{
		marked[v] = true;
		id[v] = count;
		foreach(int i in dg.AdjNodes(v))
		{
			if(!marked[i])
			{
				dfs(dg,i);
			}
		}
	}
	
	public bool StronglyConnected(int u, int v)
	{
		return marked[u] == marked[v];
	}
	
	public int Id(int v)
	{
		return id[v];
	}
	
	public int Count()
	{
		return count;
	}
}