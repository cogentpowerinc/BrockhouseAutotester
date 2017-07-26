Imports System.ComponentModel

Namespace DataElements
    Public MustInherit Class WorkOrder
        <BrowsableAttribute(False)>
        Public Property WorkOrderNo As String

        <BrowsableAttribute(False)>
        Public Property OperatorName As String

        <DisplayName("Test setting"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property TestSetting As String

        <DisplayName("Catalogue #"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property CPI_CatalogueNo As String

        <DisplayName("Customer name"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property CustomerName As String

        <DisplayName("Customer #"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property CustomerID As String

        <DisplayName("Order quantity"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property OrderQuantity As UInteger

        <DisplayName("Core code"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property CoreCode As String

        <DisplayName("Frequency [Hz]"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property Frequency As Double

        <DisplayName("Nominal weight [Lbs]"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property NominalWeight As Double

        <DisplayName("Nominal induction [T]"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property NominalInduction As Double

        <DisplayName("Nominal V/T"), BrowsableAttribute(True), ReadOnlyAttribute(True)>
        Public Property NominalV_T As Double

        <BrowsableAttribute(False)>
        Public Property HarnessTurnsRequired As Boolean

        <BrowsableAttribute(False)>
        Public Property Sample() As Sample

        <BrowsableAttribute(False)>
        Public Property PurchaseOrderNo As String

        <BrowsableAttribute(False)>
        Public Property PartDescription As String

        <BrowsableAttribute(False)>
        Public Property Grade As String

        <BrowsableAttribute(False)>
        Public Property QuantityDone As UInteger
    End Class
End Namespace
