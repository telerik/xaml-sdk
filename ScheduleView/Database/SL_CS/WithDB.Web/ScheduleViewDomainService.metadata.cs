
namespace ScheduleViewDB.Web
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Data.Objects.DataClasses;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;


	// The MetadataTypeAttribute identifies CategoryMetadata as the class
	// that carries additional metadata for the Category class.
	[MetadataTypeAttribute(typeof(Category.CategoryMetadata))]
	public partial class Category
	{

		// This class allows you to attach custom attributes to properties
		// of the Category class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class CategoryMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private CategoryMetadata()
			{
			}

			public string CategoryBrushName { get; set; }

			public int CategoryID { get; set; }

			public string CategoryName { get; set; }

			public string DisplayName { get; set; }

			public EntityCollection<SqlAppointment> SqlAppointments { get; set; }

			public EntityCollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlAppointmentMetadata as the class
	// that carries additional metadata for the SqlAppointment class.
	[MetadataTypeAttribute(typeof(SqlAppointment.SqlAppointmentMetadata))]
	public partial class SqlAppointment
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlAppointment class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlAppointmentMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlAppointmentMetadata()
			{
			}

			public string Body { get; set; }

			public Category Category { get; set; }

			public Nullable<int> CategoryID { get; set; }

			public DateTime End { get; set; }

			public int Importance { get; set; }

			public bool IsAllDayEvent { get; set; }

			public string RecurrencePattern { get; set; }

			public int SqlAppointmentId { get; set; }

			public EntityCollection<SqlAppointmentResource> SqlAppointmentResources { get; set; }

			public EntityCollection<SqlExceptionOccurrence> SqlExceptionOccurrences { get; set; }

			public DateTime Start { get; set; }

			public string Subject { get; set; }

			public TimeMarker TimeMarker { get; set; }

			public Nullable<int> TimeMarkerID { get; set; }

			public string TimeZoneString { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlAppointmentResourceMetadata as the class
	// that carries additional metadata for the SqlAppointmentResource class.
	[MetadataTypeAttribute(typeof(SqlAppointmentResource.SqlAppointmentResourceMetadata))]
	public partial class SqlAppointmentResource
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlAppointmentResource class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlAppointmentResourceMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlAppointmentResourceMetadata()
			{
			}

			public Nullable<bool> ManyToManyWorkaround { get; set; }

			public SqlAppointment SqlAppointment { get; set; }

			public int SqlAppointments_SqlAppointmentId { get; set; }

			public SqlResource SqlResource { get; set; }

			public int SqlResources_SqlResourceId { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlExceptionAppointmentMetadata as the class
	// that carries additional metadata for the SqlExceptionAppointment class.
	[MetadataTypeAttribute(typeof(SqlExceptionAppointment.SqlExceptionAppointmentMetadata))]
	public partial class SqlExceptionAppointment
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlExceptionAppointment class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlExceptionAppointmentMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlExceptionAppointmentMetadata()
			{
			}

			public string Body { get; set; }

			public Category Category { get; set; }

			public Nullable<int> CategoryID { get; set; }

			public DateTime End { get; set; }

			public int ExceptionId { get; set; }

			public int Importance { get; set; }

			public bool IsAllDayEvent { get; set; }

			public SqlExceptionOccurrence SqlExceptionOccurrence { get; set; }

			public EntityCollection<SqlExceptionResource> SqlExceptionResources { get; set; }

			public DateTime Start { get; set; }

			public string Subject { get; set; }

			public TimeMarker TimeMarker { get; set; }

			public Nullable<int> TimeMarkerID { get; set; }

			public string TimeZoneString { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlExceptionOccurrenceMetadata as the class
	// that carries additional metadata for the SqlExceptionOccurrence class.
	[MetadataTypeAttribute(typeof(SqlExceptionOccurrence.SqlExceptionOccurrenceMetadata))]
	public partial class SqlExceptionOccurrence
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlExceptionOccurrence class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlExceptionOccurrenceMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlExceptionOccurrenceMetadata()
			{
			}

			public DateTime ExceptionDate { get; set; }

			public int ExceptionId { get; set; }

			public int MasterSqlAppointmentId { get; set; }

			public SqlAppointment SqlAppointment { get; set; }

			public SqlExceptionAppointment SqlExceptionAppointment { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlExceptionResourceMetadata as the class
	// that carries additional metadata for the SqlExceptionResource class.
	[MetadataTypeAttribute(typeof(SqlExceptionResource.SqlExceptionResourceMetadata))]
	public partial class SqlExceptionResource
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlExceptionResource class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlExceptionResourceMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlExceptionResourceMetadata()
			{
			}

			public Nullable<bool> ManyToManyWorkaround { get; set; }

			public SqlExceptionAppointment SqlExceptionAppointment { get; set; }

			public int SqlExceptionAppointments_ExceptionId { get; set; }

			public SqlResource SqlResource { get; set; }

			public int SqlResources_SqlResourceId { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlResourceMetadata as the class
	// that carries additional metadata for the SqlResource class.
	[MetadataTypeAttribute(typeof(SqlResource.SqlResourceMetadata))]
	public partial class SqlResource
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlResource class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlResourceMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlResourceMetadata()
			{
			}

			public string DisplayName { get; set; }

			public string ResourceName { get; set; }

			public EntityCollection<SqlAppointmentResource> SqlAppointmentResources { get; set; }

			public EntityCollection<SqlExceptionResource> SqlExceptionResources { get; set; }

			public int SqlResourceId { get; set; }

			public SqlResourceType SqlResourceType { get; set; }

			public Nullable<int> SqlResourceTypeId { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies SqlResourceTypeMetadata as the class
	// that carries additional metadata for the SqlResourceType class.
	[MetadataTypeAttribute(typeof(SqlResourceType.SqlResourceTypeMetadata))]
	public partial class SqlResourceType
	{

		// This class allows you to attach custom attributes to properties
		// of the SqlResourceType class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class SqlResourceTypeMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private SqlResourceTypeMetadata()
			{
			}

			public bool AllowMultipleSelection { get; set; }

			public string DisplayName { get; set; }

			public string Name { get; set; }

			public EntityCollection<SqlResource> SqlResources { get; set; }

			public int SqlResourceTypeId { get; set; }
		}
	}

	// The MetadataTypeAttribute identifies TimeMarkerMetadata as the class
	// that carries additional metadata for the TimeMarker class.
	[MetadataTypeAttribute(typeof(TimeMarker.TimeMarkerMetadata))]
	public partial class TimeMarker
	{

		// This class allows you to attach custom attributes to properties
		// of the TimeMarker class.
		//
		// For example, the following marks the Xyz property as a
		// required property and specifies the format for valid values:
		//    [Required]
		//    [RegularExpression("[A-Z][A-Za-z0-9]*")]
		//    [StringLength(32)]
		//    public string Xyz { get; set; }
		internal sealed class TimeMarkerMetadata
		{

			// Metadata classes are not meant to be instantiated.
			private TimeMarkerMetadata()
			{
			}

			public EntityCollection<SqlAppointment> SqlAppointments { get; set; }

			public EntityCollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }

			public string TimeMarkerBrushName { get; set; }

			public string TimeMarkerName { get; set; }

			public int TimeMarkersId { get; set; }
		}
	}
}
