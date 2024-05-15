using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DragDropWithScheduleView
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<Customer> customersSource;

		private ObservableCollection<Appointment> appointmentsSource;

		public ViewModel()
		{
			DateTime today = DateTime.Now;

			this.CustomersSource = new ObservableCollection<Customer> 
			{
				new Customer { ID = 1, Name = "Customer 1" },
				new Customer { ID = 2, Name = "Customer 2" },
				new Customer { ID = 3, Name = "Customer 3" },
				new Customer { ID = 4, Name = "Customer 4" },
				new Customer { ID = 5, Name = "Customer 5" } 
			};
			Appointment app1 = new Appointment { Start = today, End = today.AddHours(1), Subject = "Appointment 1" };
			app1.Resources.Add(new Resource("Mary Baird", "Person"));
			Appointment app2 = new Appointment { Start = today.AddHours(1.5), End = today.AddHours(2.5), Subject = "Appointment 2" };
			app2.Resources.Add(new Resource("Diego Roel", "Person"));
			Appointment app3 = new Appointment { Start = today.AddHours(1.5), End = today.AddHours(2.5), Subject = "Appointment 3" };
			app3.Resources.Add(new Resource("Mary Baird", "Person"));
			this.AppointmentsSource = new ObservableCollection<Appointment> 
			{ 
				app1,
				app2,
				app3
			};
		}

		/// <Summary>Gets or sets AppointmentsSource and notifies for changes</Summary>
		public ObservableCollection<Appointment> AppointmentsSource
		{
			get
			{
				return this.appointmentsSource;
			}

			set
			{
				if (this.appointmentsSource != value)
				{
					this.appointmentsSource = value;
					this.OnPropertyChanged(() => this.AppointmentsSource);
				}
			}
		}

		/// <Summary>Gets or sets CustomersSource and notifies for changes</Summary>
		public ObservableCollection<Customer> CustomersSource
		{
			get
			{
				return this.customersSource;
			}

			set
			{
				if (this.customersSource != value)
				{
					this.customersSource = value;
					this.OnPropertyChanged(() => this.CustomersSource);
				}
			}
		}
	}
}
