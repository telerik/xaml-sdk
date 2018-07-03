using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace AmazonDynamoRB
{
	public class MainWindowViewModel : ViewModelBase
	{
		private AmazonDynamoDBClient client;


		private ObservableCollection<Customer> customers;

		public ObservableCollection<Customer> Customers
		{
			get { return this.customers; }
			set
			{
				if (this.customers != value)
				{
					this.customers = value;
					this.OnPropertyChanged("Customers");
				}
			}
		}

        public ICommand LoadCustomersCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        public MainWindowViewModel()
		{
			try
			{
				this.client = new AmazonDynamoDBClient();
				this.Customers = new ObservableCollection<Customer>();
				this.CreateCustomersTable();
				this.AddCustomers();
				this.LoadCustomersCommand = new DelegateCommand(LoadData);
				this.SaveChangesCommand = new DelegateCommand(SaveChanges);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Error: failed to create a DynamoDB client; " + ex.Message);
			}
		}

		private void CreateCustomersTable()
		{
			List<string> currentTables = client.ListTables().TableNames;

			if (!currentTables.Contains("Customers"))
			{
				CreateTableRequest createRequest = new CreateTableRequest
				{
					TableName = "Customers",
					AttributeDefinitions = new List<AttributeDefinition>()
					{
						new AttributeDefinition
						{
							AttributeName = "Id",
							AttributeType = "N"
						},
						new AttributeDefinition
						{
							AttributeName = "Name",
							AttributeType = "S"
						}
					},
					KeySchema = new List<KeySchemaElement>()
					{
						new KeySchemaElement
						{
							AttributeName = "Id",
							KeyType = "HASH"
						},
						new KeySchemaElement
						{
							AttributeName = "Name",
							KeyType = "RANGE"
						}
					},
				};

				createRequest.ProvisionedThroughput = new ProvisionedThroughput(1, 1);

				CreateTableResponse createResponse;
				try
				{
					createResponse = client.CreateTable(createRequest);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error: failed to create the new table; " + ex.Message);

					return;
				}
			}
		}

		private void AddCustomers()
		{
			var table = Table.LoadTable(client, "Customers");
			var search = table.Scan(new Expression());
			if (search.Count == 0)
			{
				Document dataObj1 = new Document();
				dataObj1["Name"] = "Telerik";
				dataObj1["Id"] = 2;
				dataObj1["Employees"] = 46;
				dataObj1["State"] = "NY";
				table.PutItem(dataObj1);

				Document dataObj2 = new Document();
				dataObj2["Name"] = "Progress";
				dataObj2["Id"] = 13;
				dataObj2["Employees"] = 54;
				dataObj2["State"] = "IL";
				table.PutItem(dataObj2);
			}
		}

		private void LoadData(object obj)
		{
			var table = Table.LoadTable(client, "Customers");
			var search = table.Scan(new Expression());

			var documentList = new List<Document>();
			do
			{
				documentList.AddRange(search.GetNextSet());

			} while (!search.IsDone);

			var customers = new ObservableCollection<Customer>();
			foreach (var doc in documentList)
			{
				var customer = new Customer();
				foreach (var attribute in doc.GetAttributeNames())
				{
					var value = doc[attribute];
					if (attribute == "Id")
					{
						customer.Id = Convert.ToInt32(value.AsPrimitive().Value);
					}
					else if (attribute == "Name")
					{
						customer.Name = value.AsPrimitive().Value.ToString();
					}
					else if (attribute == "Employees")
					{
						customer.Employees = Convert.ToInt32(value.AsPrimitive().Value);
					}
					else if (attribute == "State")
					{
						customer.State = value.AsPrimitive().Value.ToString();
					}
				}

				customers.Add(customer);
			}

			this.Customers = customers;
		}

		private void SaveChanges(object obj)
		{
			foreach (var item in this.Customers)
			{
				this.UpdateCustomer(item as Customer);
			}
		}

		private void UpdateCustomer(Customer customer)
		{
			var table = Table.LoadTable(client, "Customers");
			var entry = new Document();
			entry["Id"] = customer.Id;
			entry["Name"] = customer.Name;
			entry["Employees"] = customer.Employees;
			entry["State"] = customer.State;
			table.UpdateItem(entry);
		}
	}
}
