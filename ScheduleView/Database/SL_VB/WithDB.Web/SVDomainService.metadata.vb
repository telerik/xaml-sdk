
Option Compare Binary
Option Infer On
Option Strict On
Option Explicit On

Imports ScheduleView_DB_SL_VB.Web
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Data.Objects.DataClasses
Imports System.Linq
Imports System.ServiceModel.DomainServices.Hosting
Imports System.ServiceModel.DomainServices.Server


'The MetadataTypeAttribute identifies CategoryMetadata as the class
' that carries additional metadata for the Category class.
<MetadataTypeAttribute(GetType(Category.CategoryMetadata))>  _
Partial Public Class Category
    
    'This class allows you to attach custom attributes to properties
    ' of the Category class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class CategoryMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property CategoryBrushName As String
        
        Public Property CategoryID As Integer
        
        Public Property CategoryName As String
        
        Public Property DisplayName As String
        
        Public Property SqlAppointments As EntityCollection(Of SqlAppointment)
        
        Public Property SqlExceptionAppointments As EntityCollection(Of SqlExceptionAppointment)
    End Class
End Class

'The MetadataTypeAttribute identifies SqlAppointmentMetadata as the class
' that carries additional metadata for the SqlAppointment class.
<MetadataTypeAttribute(GetType(SqlAppointment.SqlAppointmentMetadata))>  _
Partial Public Class SqlAppointment
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlAppointment class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlAppointmentMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property Body As String
        
        Public Property Category As Category
        
        Public Property CategoryID As Nullable(Of Integer)
        
        Public Property [End] As DateTime
        
        Public Property Importance As Integer
        
        Public Property IsAllDayEvent As Boolean
        
        Public Property RecurrencePattern As String
        
        Public Property SqlAppointmentId As Integer
        
        Public Property SqlAppointmentResources As EntityCollection(Of SqlAppointmentResource)
        
        Public Property SqlExceptionOccurrences As EntityCollection(Of SqlExceptionOccurrence)
        
        Public Property Start As DateTime
        
        Public Property Subject As String
        
        Public Property TimeMarker As TimeMarker
        
        Public Property TimeMarkerID As Nullable(Of Integer)
        
        Public Property TimeZoneString As String
    End Class
End Class

'The MetadataTypeAttribute identifies SqlAppointmentResourceMetadata as the class
' that carries additional metadata for the SqlAppointmentResource class.
<MetadataTypeAttribute(GetType(SqlAppointmentResource.SqlAppointmentResourceMetadata))>  _
Partial Public Class SqlAppointmentResource
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlAppointmentResource class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlAppointmentResourceMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property ManyToManyWorkaround As Nullable(Of Boolean)
        
        Public Property SqlAppointment As SqlAppointment
        
        Public Property SqlAppointments_SqlAppointmentId As Integer
        
        Public Property SqlResource As SqlResource
        
        Public Property SqlResources_SqlResourceId As Integer
    End Class
End Class

'The MetadataTypeAttribute identifies SqlExceptionAppointmentMetadata as the class
' that carries additional metadata for the SqlExceptionAppointment class.
<MetadataTypeAttribute(GetType(SqlExceptionAppointment.SqlExceptionAppointmentMetadata))>  _
Partial Public Class SqlExceptionAppointment
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlExceptionAppointment class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlExceptionAppointmentMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property Body As String
        
        Public Property Category As Category
        
        Public Property CategoryID As Nullable(Of Integer)
        
        Public Property [End] As DateTime
        
        Public Property ExceptionId As Integer
        
        Public Property Importance As Integer
        
        Public Property IsAllDayEvent As Boolean
        
        Public Property SqlExceptionOccurrence As SqlExceptionOccurrence
        
        Public Property SqlExceptionResources As EntityCollection(Of SqlExceptionResource)
        
        Public Property Start As DateTime
        
        Public Property Subject As String
        
        Public Property TimeMarker As TimeMarker
        
        Public Property TimeMarkerID As Nullable(Of Integer)
        
        Public Property TimeZoneString As String
    End Class
End Class

'The MetadataTypeAttribute identifies SqlExceptionOccurrenceMetadata as the class
' that carries additional metadata for the SqlExceptionOccurrence class.
<MetadataTypeAttribute(GetType(SqlExceptionOccurrence.SqlExceptionOccurrenceMetadata))>  _
Partial Public Class SqlExceptionOccurrence
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlExceptionOccurrence class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlExceptionOccurrenceMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property ExceptionDate As DateTime
        
        Public Property ExceptionId As Integer
        
        Public Property MasterSqlAppointmentId As Integer
        
        Public Property SqlAppointment As SqlAppointment
        
        Public Property SqlExceptionAppointment As SqlExceptionAppointment
    End Class
End Class

'The MetadataTypeAttribute identifies SqlExceptionResourceMetadata as the class
' that carries additional metadata for the SqlExceptionResource class.
<MetadataTypeAttribute(GetType(SqlExceptionResource.SqlExceptionResourceMetadata))>  _
Partial Public Class SqlExceptionResource
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlExceptionResource class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlExceptionResourceMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property ManyToManyWorkaround As Nullable(Of Boolean)
        
        Public Property SqlExceptionAppointment As SqlExceptionAppointment
        
        Public Property SqlExceptionAppointments_ExceptionId As Integer
        
        Public Property SqlResource As SqlResource
        
        Public Property SqlResources_SqlResourceId As Integer
    End Class
End Class

'The MetadataTypeAttribute identifies SqlResourceMetadata as the class
' that carries additional metadata for the SqlResource class.
<MetadataTypeAttribute(GetType(SqlResource.SqlResourceMetadata))>  _
Partial Public Class SqlResource
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlResource class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlResourceMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property DisplayName As String
        
        Public Property ResourceName As String
        
        Public Property SqlAppointmentResources As EntityCollection(Of SqlAppointmentResource)
        
        Public Property SqlExceptionResources As EntityCollection(Of SqlExceptionResource)
        
        Public Property SqlResourceId As Integer
        
        Public Property SqlResourceType As SqlResourceType
        
        Public Property SqlResourceTypeId As Nullable(Of Integer)
    End Class
End Class

'The MetadataTypeAttribute identifies SqlResourceTypeMetadata as the class
' that carries additional metadata for the SqlResourceType class.
<MetadataTypeAttribute(GetType(SqlResourceType.SqlResourceTypeMetadata))>  _
Partial Public Class SqlResourceType
    
    'This class allows you to attach custom attributes to properties
    ' of the SqlResourceType class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class SqlResourceTypeMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property AllowMultipleSelection As Boolean
        
        Public Property DisplayName As String
        
        Public Property Name As String
        
        Public Property SqlResources As EntityCollection(Of SqlResource)
        
        Public Property SqlResourceTypeId As Integer
    End Class
End Class

'The MetadataTypeAttribute identifies TimeMarkerMetadata as the class
' that carries additional metadata for the TimeMarker class.
<MetadataTypeAttribute(GetType(TimeMarker.TimeMarkerMetadata))>  _
Partial Public Class TimeMarker
    
    'This class allows you to attach custom attributes to properties
    ' of the TimeMarker class.
    '
    'For example, the following marks the Xyz property as a
    ' required property and specifies the format for valid values:
    '    <Required()>
    '    <RegularExpression("[A-Z][A-Za-z0-9]*")>
    '    <StringLength(32)>
    '    Public Property Xyz As String
    Friend NotInheritable Class TimeMarkerMetadata
        
        'Metadata classes are not meant to be instantiated.
        Private Sub New()
            MyBase.New
        End Sub
        
        Public Property SqlAppointments As EntityCollection(Of SqlAppointment)
        
        Public Property SqlExceptionAppointments As EntityCollection(Of SqlExceptionAppointment)
        
        Public Property TimeMarkerBrushName As String
        
        Public Property TimeMarkerName As String
        
        Public Property TimeMarkersId As Integer
    End Class
End Class

